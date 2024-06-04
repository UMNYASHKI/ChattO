using Application.Abstractions;
using Application.Helpers;
using Application.Helpers.Mappings;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.Groups.Commands;

public class Create
{
    public class Command : IRequest<Result<bool>>, IMapWith<Group>
    {
        public string Name { get; set; }

        public Guid OrganizationId { get; set; }

        public List<Guid> UsersId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Command, Group>()
                .ForMember(org => org.Name, opt => opt.MapFrom(c => c.Name))
                .ForMember(org => org.OrganizationId, opt => opt.MapFrom(c => c.OrganizationId));
        }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.OrganizationId).NotEmpty();
            RuleFor(x => x.UsersId).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly IRepository<Group> _groupRepository;

        private readonly IRepository<AppUserGroup> _userGroupsRepository;

        private readonly IValidator<Command> _validator;

        private readonly IMapper _mapper;

        public Handler(IRepository<Group> groupRepository, IRepository<AppUserGroup> userGroupRepository, IValidator<Command> validator, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _userGroupsRepository = userGroupRepository;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Result.Failure<bool>(validationResult.ToString(" "));
            }

            var nameUnique = await _groupRepository.IsUnique(group => group.Name == request.Name && group.OrganizationId == request.OrganizationId);
            if (!nameUnique.Data) 
            {
                return Result.Failure<bool>("Invalid group name");
            }

            var groupId = Guid.NewGuid();
            var group = _mapper.Map<Group>(request);
            group.Id = groupId;
            var createGroupResult = await _groupRepository.AddItemAsync(group);
            if (!createGroupResult.IsSuccessful)
            {
                return createGroupResult;
            }

            var userGroups = request.UsersId.Select(userId => new AppUserGroup() { AppUserId = userId, GroupId = groupId });
            return await _userGroupsRepository.AddItemsAsync(userGroups);
        }
    }
}

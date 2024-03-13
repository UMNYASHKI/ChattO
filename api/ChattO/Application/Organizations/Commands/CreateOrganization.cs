using FluentValidation;
using Domain.Models;
using MediatR;
using Application.Helpers;
using Application.Helpers.Mappings;
using AutoMapper;
using Application.Abstractions;

namespace Application.Organizations.Commands;

public class CreateOrganization
{
    public class  Command : IRequest<Result<Guid>>, IMapWith<Organization>
    {
        public string Name { get; set; }
        public string Domain { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Command, Organization>()
                .ForMember(org => org.Name,
                    opt => opt.MapFrom(c => c.Name))
                .ForMember(org => org.Domain,
                    opt => opt.MapFrom(c => c.Domain));
        }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Domain).NotEmpty();
        }
    }
    public class Handler : IRequestHandler<Command, Result<Guid>>
    {
        private readonly IRepository<Organization> _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<Command> _validator;
        public Handler(IRepository<Organization> repository, IMapper mapper, IValidator<Command> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return Result.Failure<Guid>(validationResult.ToString(" "));

            var organization = _mapper.Map<Organization>(request);

            var result = await _repository.AddItemAsync(organization);

            if (!result.IsSuccessful)
                return Result.Failure<Guid>($"Failed to create {nameof(Organization)}");

            return Result.Success(organization.Id);
        }
    }
}

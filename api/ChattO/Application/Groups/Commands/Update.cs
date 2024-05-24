using Application.Abstractions;
using Application.Helpers;
using Application.Helpers.Mappings;
using AutoMapper;
using Domain.Models;
using MediatR;

namespace Application.Groups.Commands;

public class Update
{
    public class Command : IRequest<Result<bool>>, IMapWith<Group>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Command, Group>()
                .ForMember(org => org.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(org => org.Name, opt => opt.MapFrom(c => c.Name));
        }
    }

    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly IRepository<Group> _groupRepository;

        private readonly IMapper _mapper;

        public Handler(IRepository<Group> groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }
        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var original = await _groupRepository.GetByIdAsync(request.Id);
            if (!original.IsSuccessful)
            {
                return Result.Failure<bool>(original.Message);
            }

            var data = _mapper.Map<Group>(request);

            return await _groupRepository.UpdateItemAsync(_mapper.Map<Group>(request));
        }
    }
}

using Application.Abstractions;
using Application.Helpers;
using Application.Helpers.Mappings;
using AutoMapper;
using Domain.Enums;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.Feeds.Commands;

public class CreateFeed
{
    public class Command : IRequest<Result<Feed>>, IMapWith<Feed>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public FeedType Type { get; set; }
        public Guid CreatorId { get; set; }
        public Guid? GroupId { get; set; }
        public List<Guid> AppUsersId { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Command, Feed>()
                .ForMember(feed => feed.Name, opt => opt.MapFrom(cmd => cmd.Name))
                .ForMember(feed => feed.Description, opt => opt.MapFrom(cmd => cmd.Description))
                .ForMember(feed => feed.Type, opt => opt.MapFrom(cmd => cmd.Type))
                .ForMember(feed => feed.GroupId, opt => opt.MapFrom(cmd => cmd.GroupId))
                .ForMember(feed => feed.AppUserFeeds, opt => opt.MapFrom(cmd => cmd.AppUsersId
                    .Select(id => new AppUserFeed { AppUserId = id, IsCreator = cmd.CreatorId == id }).ToList()));
        }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Type).IsInEnum();
            RuleFor(x => x.CreatorId).NotEmpty();
            When(c => c.GroupId == null, () =>
            {
                RuleFor(c => c.Type).Equal(FeedType.Chat);
                RuleFor(x => x.AppUsersId).NotEmpty();
            });
        }
    }

    public class Handler : IRequestHandler<Command, Result<Feed>>
    {
        private readonly IRepository<Feed> _repository;
        private readonly IValidator<Command> _validator;
        private readonly IMapper _mapper;
        private readonly IRepository<AppUserGroup> _userGroupsRepository;
        public Handler(IRepository<Feed> repository, IValidator<Command> validator, 
            IMapper mapper, IRepository<AppUserGroup> userGroupsRepository)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
            _userGroupsRepository = userGroupsRepository;
        }

        public async Task<Result<Feed>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Result.Failure<Feed>(validationResult.ToString(" "));

            var feed = _mapper.Map<Feed>(request);
            feed.AppUserFeeds.Add(new AppUserFeed { AppUserId = request.CreatorId, IsCreator = true });

            if (request.GroupId is not null)
            {
                var addUsersResult = await AddUsersFromGroup(feed, request.CreatorId);
                if (!addUsersResult.IsSuccessful)
                    return Result.Failure<Feed>(addUsersResult.Message);
            }

            var createResult = await _repository.AddItemAsync(feed);
            if(!createResult.IsSuccessful)
                return Result.Failure<Feed>(createResult.Message);

            return Result.Success(feed);
        }

        public async Task<Result<bool>> AddUsersFromGroup(Feed feed, Guid creatorId)
        {
            var userGroupsResult = await _userGroupsRepository.GetAllAsync(x => x.GroupId == feed.GroupId && x.AppUserId != creatorId);
            if (!userGroupsResult.IsSuccessful)
                return Result.Failure<bool>(userGroupsResult.Message);

            var appUserFeeds = userGroupsResult.Data.Select(userGroup => 
                new AppUserFeed { AppUserId = userGroup.AppUserId, IsCreator = false }).ToList();

            feed.AppUserFeeds = feed.AppUserFeeds.Concat(appUserFeeds).ToList();

            return Result.Success(true);
        }
    }
}

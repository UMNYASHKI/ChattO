using Application.Abstractions;
using Application.Helpers;
using Application.Helpers.Mappings;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.Messages.Commands;

public class CreateMessage
{
    public class Command : IRequest<Result<bool>>, IMapWith<Message>
    {
        public Guid SenderId { get; set; }
        public Guid FeedId { get; set; }
        public string Content { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Command, Message>()
                .ForMember(message => message.Text, opt => opt.MapFrom(c => c.Content))
                .AfterMap((src, dest) => dest.CreatedAt = DateTime.UtcNow);
        }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.SenderId).NotEmpty();
            RuleFor(x => x.FeedId).NotEmpty();
            RuleFor(x => x.Content).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly IRepository<Message> _repository;
        private readonly IRepository<AppUserFeed> _feedRepository;
        private readonly IValidator<Command> _validator;
        private readonly IMapper _mapper;
        public Handler(IRepository<Message> repository, IRepository<AppUserFeed> feedRepository,
            IValidator<Command> validator, IMapper mapper)
        {
            _repository = repository;
            _feedRepository = feedRepository;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Result.Failure<bool>(validationResult.ToString(" "));

            var message = _mapper.Map<Message>(request);
            var appUserFeedsResult = await _feedRepository
                .GetAllAsync(x => x.FeedId == request.FeedId && x.AppUserId == request.SenderId);
            if (!appUserFeedsResult.IsSuccessful)
                return Result.Failure<bool>(appUserFeedsResult.Message);

            var appUserFeed = appUserFeedsResult.Data.FirstOrDefault();
            if (appUserFeed is null)
                return Result.Failure<bool>("User is not allowed to post in this feed");

            message.AppUserFeedId = appUserFeed.Id;
            var result = await _repository.AddItemAsync(message);
            if (!result.IsSuccessful)
                return Result.Failure<bool>(result.Message);

            return Result.Success(true);
        }
    }
}

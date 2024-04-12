using Application.Abstractions;
using Application.Helpers;
using Domain.Models;
using MediatR;
using FluentValidation;

namespace Application.Files.Queries;

public class DownloadFeedImage
{
    public class Command : IRequest<Result<string>>
    {
        public Guid FeedId { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.FeedId).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly ICloudRepository _cloudRepository;

        private readonly IRepository<Feed> _feedRepository;

        public Handler(ICloudRepository cloudRepository, IRepository<Feed> feedRepository)
        {
            _cloudRepository = cloudRepository;
            _feedRepository = feedRepository;
        }

        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var getFeedResult = await _feedRepository.GetByIdAsync(request.FeedId);
            if (!getFeedResult.IsSuccessful)
            {
                return Result.Failure<string>(getFeedResult.Message);
            }

            var feed = getFeedResult.Data;

            if (feed.FeedImageId != null && await _cloudRepository.FileExists(feed.FeedImage.Name))
            {
                return Result.Success(feed.FeedImage.PublicUrl);
            }

            return Result.Success<string>(null);
        }
    }
}

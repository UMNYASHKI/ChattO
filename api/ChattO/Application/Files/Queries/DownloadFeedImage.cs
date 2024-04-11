using Application.Abstractions;
using Application.Extensions;
using Application.Helpers;
using Domain.Models.Files;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Files.Queries;

public class DownloadFeedImage
{
    public class Command : IRequest<Result<string>>
    {
        public string Domain { get; set; }

        public Guid FeedId { get; set; }
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

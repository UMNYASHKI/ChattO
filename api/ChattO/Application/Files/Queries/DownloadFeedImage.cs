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
    public class Command : IRequest<Result<byte[]>>
    {
        public string Domain { get; set; }

        public Guid FeedId { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<byte[]>>
    {
        private readonly ICloudRepository _cloudRepository;

        private readonly IRepository<Feed> _feedRepository;

        public Handler(ICloudRepository cloudRepository, IRepository<Feed> feedRepository)
        {
            _cloudRepository = cloudRepository;
            _feedRepository = feedRepository;
        }

        public async Task<Result<byte[]>> Handle(Command request, CancellationToken cancellationToken)
        {
            var getFeedResult = await _feedRepository.GetByIdAsync(request.FeedId);
            if (!getFeedResult.IsSuccessful) 
            {
                return Result.Failure<byte[]>(getFeedResult.Message);
            }

            if (getFeedResult.Data.FeedImageId == null)
            {
                return Result.Success<byte[]>(null);
            }

            var path = PathExtension.GetPath<ProfileImage>(request.Domain, getFeedResult.Data.FeedImageId.ToString(), request.FeedId.ToString());

            return await _cloudRepository.DownloadFile(path);
        }
    }
}

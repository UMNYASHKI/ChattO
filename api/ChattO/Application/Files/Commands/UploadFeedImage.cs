using Application.Abstractions;
using Application.Extensions;
using Application.Helpers;
using Domain.Models;
using Domain.Models.Files;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Files.Commands;

public class UploadFeedImage
{
    public class Command : IRequest<Result<bool>>
    {
        public IFormFile File { get; set; }

        public string Domain { get; set; }

        public Guid FeedId { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly ICloudRepository _cloudRepository;

        private readonly IRepository<FeedImage> _fileRepository;

        private readonly IRepository<Feed> _feedRepository;

        public Handler(ICloudRepository cloudRepository, IRepository<FeedImage> repository, IRepository<Feed> feedRepository)
        {
            _cloudRepository = cloudRepository;
            _fileRepository = repository;
            _feedRepository = feedRepository;
        }

        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var fileName = Guid.NewGuid();
            var extension = request.File.GetFileExtension();
            var path = PathExtension.GetPath<FeedImage>(request.Domain, fileName.ToString() + '.' + extension, request.FeedId.ToString());

            var uploadResult = await _cloudRepository.UploadFile(request.File, path);
            if (!uploadResult.IsSuccessful)
            {
                return uploadResult;
            }

            var createResult = await _fileRepository.AddItemAsync(new FeedImage() { Id = fileName, FeedId = request.FeedId, Name = path, PublicUrl = _cloudRepository.GetFileUrl(path) });
            if (!createResult.IsSuccessful)
            {
                return createResult;
            }

            await SetFeedImageId(request.FeedId, fileName);

            return Result.Success<bool>();
        }

        private async Task SetFeedImageId(Guid feedId, Guid feedImageId) 
        {
            var feed = (await _feedRepository.GetByIdAsync(feedId)).Data;
            feed.FeedImageId = feedImageId;
            await _feedRepository.UpdateItemAsync(feed);
        }
    }
}

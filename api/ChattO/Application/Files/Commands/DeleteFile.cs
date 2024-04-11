using Application.Abstractions;
using Application.Helpers;
using Domain.Models.Files;
using MediatR;

namespace Application.Files.Commands;

public class DeleteFile
{
    public class Command<T> : IRequest<Result<bool>> where T : BaseFile
    {
        public Guid FileId { get; set; }
    }

    public class ProfileImageHandler : IRequestHandler<Command<ProfileImage>, Result<bool>>
    {
        private readonly ICloudRepository _cloudRepository;

        private readonly IRepository<ProfileImage> _fileRepository;

        public ProfileImageHandler(ICloudRepository cloudRepository, IRepository<ProfileImage> fileRepository)
        {
            _cloudRepository = cloudRepository;
            _fileRepository = fileRepository;
        }

        public async Task<Result<bool>> Handle(Command<ProfileImage> request, CancellationToken cancellationToken)
        {
            var getFileResult = await _fileRepository.GetByIdAsync(request.FileId);
            if (!getFileResult.IsSuccessful)
            {
                return Result.Failure<bool>(getFileResult.Message);
            }

            var deleteFileFromCloudResult = await _cloudRepository.DeleteFile(getFileResult.Data.Name);

            if (!deleteFileFromCloudResult.IsSuccessful)
            {
                return Result.Failure<bool>("Cannot delete file from cloud" + deleteFileFromCloudResult.Message);
            }

            var deleteFileFromDbResult = await _fileRepository.DeleteItemAsync(request.FileId);

            if (!deleteFileFromDbResult.IsSuccessful)
            {
                return Result.Failure<bool>(deleteFileFromDbResult.Message);
            }

            return Result.Success<bool>();
        }
    }

    public class FeedImageHandler : IRequestHandler<Command<FeedImage>, Result<bool>>
    {
        private readonly ICloudRepository _cloudRepository;

        private readonly IRepository<FeedImage> _fileRepository;

        public FeedImageHandler(ICloudRepository cloudRepository, IRepository<FeedImage> fileRepository)
        {
            _cloudRepository = cloudRepository;
            _fileRepository = fileRepository;
        }

        public async Task<Result<bool>> Handle(Command<FeedImage> request, CancellationToken cancellationToken)
        {
            var getFileResult = await _fileRepository.GetByIdAsync(request.FileId);
            if (!getFileResult.IsSuccessful)
            {
                return Result.Failure<bool>(getFileResult.Message);
            }

            var deleteFileFromCloudResult = await _cloudRepository.DeleteFile(getFileResult.Data.Name);

            if (!deleteFileFromCloudResult.IsSuccessful)
            {
                return Result.Failure<bool>("Cannot delete file from cloud" + deleteFileFromCloudResult.Message);
            }

            var deleteFileFromDbResult = await _fileRepository.DeleteItemAsync(request.FileId);

            if (!deleteFileFromDbResult.IsSuccessful)
            {
                return Result.Failure<bool>(deleteFileFromDbResult.Message);
            }

            return Result.Success<bool>();
        }
    }

    public class MessageFileHandler : IRequestHandler<Command<MessageFile>, Result<bool>>
    {
        private readonly ICloudRepository _cloudRepository;

        private readonly IRepository<MessageFile> _fileRepository;

        public MessageFileHandler(ICloudRepository cloudRepository, IRepository<MessageFile> fileRepository)
        {
            _cloudRepository = cloudRepository;
            _fileRepository = fileRepository;
        }

        public async Task<Result<bool>> Handle(Command<MessageFile> request, CancellationToken cancellationToken)
        {
            var getFileResult = await _fileRepository.GetByIdAsync(request.FileId);
            if (!getFileResult.IsSuccessful)
            {
                return Result.Failure<bool>(getFileResult.Message);
            }

            var deleteFileFromCloudResult = await _cloudRepository.DeleteFile(getFileResult.Data.Name);

            if (!deleteFileFromCloudResult.IsSuccessful)
            {
                return Result.Failure<bool>("Cannot delete file from cloud" + deleteFileFromCloudResult.Message);
            }

            var deleteFileFromDbResult = await _fileRepository.DeleteItemAsync(request.FileId);

            if (!deleteFileFromDbResult.IsSuccessful)
            {
                return Result.Failure<bool>(deleteFileFromDbResult.Message);
            }

            return Result.Success<bool>();
        }
    }
}

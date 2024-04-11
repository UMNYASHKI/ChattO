using Application.Abstractions;
using Application.Helpers;
using Domain.Models.Files;
using MediatR;

namespace Application.Files.Commands;

public class DeleteFile
{
    public class Command : IRequest<Result<bool>>
    {
        public Guid FileId { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly ICloudRepository _cloudRepository;

        private readonly IRepository<BaseFile> _fileRepository;

        public Handler(ICloudRepository cloudRepository, IRepository<BaseFile> fileRepository)
        {
            _cloudRepository = cloudRepository;
            _fileRepository = fileRepository;
        }

        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
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

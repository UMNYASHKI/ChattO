using Application.Abstractions;
using Application.Extensions;
using Application.Helpers;
using Domain.Models.Files;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Files.Commands;

public class UploadProfileImage
{
    public class Command : IRequest<Result<bool>>
    {
        public IFormFile File { get; set; }

        public string Domain { get; set; }

        public Guid AccountId { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly ICloudRepository _cloudRepository;

        private readonly IRepository<ProfileImage> _fileRepository;

        public Handler(ICloudRepository cloudRepository, IRepository<ProfileImage> repository)
        {
            _cloudRepository = cloudRepository;
            _fileRepository = repository;
        }

        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var fileName = Guid.NewGuid();

            var path = PathExtension.GetPath<ProfileImage>(request.Domain, fileName.ToString());
            
            var uploadResult = await _cloudRepository.UploadFile(request.File, path);
            if (!uploadResult.IsSuccessful)
            {
                return uploadResult;
            }

            var createResult = await _fileRepository.AddItemAsync(new ProfileImage() { Id = fileName, AppUserId = request.AccountId });
            if (!createResult.IsSuccessful)
            {
                return createResult;
            }

            return Result.Success<bool>();
        }
    }
}

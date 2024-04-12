using Application.Abstractions;
using Application.Helpers;
using Domain.Models.Files;
using FluentValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Files.Commands;

public class DeleteFile
{
    public class Command : IRequest<Result<bool>>
    {
        public Guid FileId { get; set; }

        public Type FileType { get; set; }
    }

    public class CommanValidator : AbstractValidator<Command>
    {
        public CommanValidator()
        {
            RuleFor(x => x.FileId).NotEmpty();
            RuleFor(x => x.FileType).NotNull().Must(type => type.BaseType == typeof(BaseFile));
        }
    }

    public class ProfileImageHandler : IRequestHandler<Command, Result<bool>>
    {
        private readonly ICloudRepository _cloudRepository;

        private readonly IValidator<Command> _validator;

        private readonly IRepository<BaseFile> _fileRepository;

        public ProfileImageHandler(ICloudRepository cloudRepository, IRepository<BaseFile> fileRepository, IValidator<Command> validator)
        {
            _cloudRepository = cloudRepository;
            _fileRepository = fileRepository;
            _validator = validator;
        }

        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Failure<bool>(string.Join(". ", validationResult.Errors));
            }

            var wrapper = new DeleteFile();
            return request.FileType switch
            {
                Type t when t == typeof(ProfileImage) => await wrapper.Handle<ProfileImage>(_cloudRepository, _fileRepository, request),
                Type t when t == typeof(FeedImage) => await wrapper.Handle<FeedImage>(_cloudRepository, _fileRepository, request),
                Type t when t == typeof(MessageFile) => await wrapper.Handle<MessageFile>(_cloudRepository, _fileRepository, request),
                _ => Result.Failure<bool>("Invalid file type"),
            };
        }
    }

    public async Task<Result<bool>> Handle<TFile>(ICloudRepository cloudRepository, IRepository<BaseFile> fileRepository, Command request) where TFile : BaseFile
    {
        var getFileResult = await fileRepository.GetByIdAsync<TFile>(request.FileId);
        if (!getFileResult.IsSuccessful)
        {
            return Result.Failure<bool>(getFileResult.Message);
        }

        var deleteFileFromCloudResult = await cloudRepository.DeleteFile(getFileResult.Data.Name);

        if (!deleteFileFromCloudResult.IsSuccessful)
        {
            return Result.Failure<bool>("Cannot delete file from cloud" + deleteFileFromCloudResult.Message);
        }

        var deleteFileFromDbResult = await fileRepository.DeleteItemAsync<TFile>(request.FileId);

        if (!deleteFileFromDbResult.IsSuccessful)
        {
            return Result.Failure<bool>(deleteFileFromDbResult.Message);
        }

        return Result.Success<bool>();
    }
}

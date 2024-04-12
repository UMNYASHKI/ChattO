using Application.Abstractions;
using Application.Extensions;
using Application.Helpers;
using Domain.Models;
using Domain.Models.Files;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Application.Files.Commands.Upload;

public class UploadProfileImage
{
    public class Command : IRequest<Result<bool>>
    {
        public IFormFile File { get; set; }

        public string Domain { get; set; }

        public Guid AccountId { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command> 
    {
        public CommandValidator()
        {
            RuleFor(x => x.File).NotNull();
            RuleFor(x => x.Domain).NotEmpty();
            RuleFor(x => x.AccountId).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly ICloudRepository _cloudRepository;

        private readonly IRepository<ProfileImage> _fileRepository;

        private readonly UserManager<AppUser> _userManager;

        private readonly IValidator<Command> _validator;

        public Handler(ICloudRepository cloudRepository, IRepository<ProfileImage> repository, UserManager<AppUser> userManager, IValidator<Command> validator)
        {
            _cloudRepository = cloudRepository;
            _fileRepository = repository;
            _userManager = userManager;
            _validator = validator;
        }

        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Failure<bool>(string.Join(". ", validationResult.Errors));
            }

            var fileName = Guid.NewGuid();
            var extension = request.File.GetFileExtension();

            var path = PathExtension.GetPath<ProfileImage>(request.Domain, fileName.ToString() + '.' + extension);

            var uploadResult = await _cloudRepository.UploadFile(request.File, path);
            if (!uploadResult.IsSuccessful)
            {
                return uploadResult;
            }

            var createResult = await _fileRepository.AddItemAsync(new ProfileImage() { Id = fileName, Name = path, AppUserId = request.AccountId, PublicUrl = _cloudRepository.GetFileUrl(path) });
            if (!createResult.IsSuccessful)
            {
                return createResult;
            }

            var user = await _userManager.FindByIdAsync(request.AccountId.ToString());
            await CheckPreviousFile(user);
            await SetProfileImageId(user, fileName);

            return Result.Success<bool>();
        }

        private async Task CheckPreviousFile(AppUser user)
        {
            if (user.ProfileImageId == null)
            {
                return;
            }

            var profile = user.ProfileImage;
            await _cloudRepository.DeleteFile(profile.Name);
            await _fileRepository.DeleteItemAsync(profile.Id);
        }

        private async Task SetProfileImageId(AppUser user, Guid profileImageId)
        {
            user.ProfileImageId = profileImageId;
            await _userManager.UpdateAsync(user);
        }
    }
}

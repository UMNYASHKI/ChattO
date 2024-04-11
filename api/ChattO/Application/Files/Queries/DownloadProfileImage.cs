using Application.Abstractions;
using Application.Extensions;
using Application.Helpers;
using Domain.Models;
using Domain.Models.Files;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Files.Queries;

public class DownloadProfileImage
{
    public class Command : IRequest<Result<string>>
    {
        public Guid AccountId { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly ICloudRepository _cloudRepository;

        public Handler(UserManager<AppUser> userManager, ICloudRepository cloudRepository)
        {
            _userManager = userManager;
            _cloudRepository = cloudRepository;
        }

        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.AccountId.ToString());

            if (user == null)
            {
                return Result.Failure<string>("Invalid account id provided");
            }

            var image = user.ProfileImage;
            if (image != null && await _cloudRepository.FileExists(image.Name))
            {
                return Result.Success(image.PublicUrl);
            }

            return Result.Success<string>(null);
        }
    }
}

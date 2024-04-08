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
    public class Command : IRequest<Result<byte[]>>
    {
        public string Domain { get; set; }

        public Guid AccountId { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<byte[]>>
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly ICloudRepository _cloudRepository;

        public Handler(UserManager<AppUser> userManager, ICloudRepository cloudRepository)
        {
            _userManager = userManager;
            _cloudRepository = cloudRepository;
        }

        public async Task<Result<byte[]>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.AccountId.ToString());

            if (user == null)
            {
                return Result.Failure<byte[]>("Invalid account id provided");
            }

            if (user.ProfileImageId == null)
            {
                return Result.Success<byte[]>(null);
            }

            var path = PathExtension.GetPath<ProfileImage>(request.Domain, user.ProfileImageId.ToString());

            return await _cloudRepository.DownloadFile(path);
        }
    }
}

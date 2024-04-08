using Application.Abstractions;
using Application.Extensions;
using Application.Helpers;
using Domain.Models.Files;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Files.Queries;

public class DownloadMessageFile
{
    public class Command : IRequest<Result<byte[]>>
    {
        public string Domain { get; set; }

        public Guid FeedId { get; set; }

        public Guid MessageId { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<byte[]>>
    {
        private readonly ICloudRepository _cloudRepository;

        private readonly IRepository<Message> _repository;

        public Handler(ICloudRepository cloudRepository)
        {
            _cloudRepository = cloudRepository;
        }

        public async Task<Result<byte[]>> Handle(Command request, CancellationToken cancellationToken)
        {
            var getMessageResult = await _repository.GetByIdAsync(request.MessageId);
            if (!getMessageResult.IsSuccessful) 
            {
                return Result.Failure<byte[]>(getMessageResult.Message);
            }

            if (getMessageResult.Data.MessageFileId == null)
            {
                return Result.Success<byte[]>(null);
            }

            var path = PathExtension.GetPath<ProfileImage>(request.Domain, getMessageResult.Data.MessageFileId.ToString(), request.FeedId.ToString());

            return await _cloudRepository.DownloadFile(path);
        }
    }
}

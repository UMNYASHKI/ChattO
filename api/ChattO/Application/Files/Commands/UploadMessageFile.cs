using Application.Abstractions;
using Application.Extensions;
using Application.Helpers;
using Domain.Models;
using Domain.Models.Files;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Files.Commands;

public class UploadMessageFile
{
    public class Command : IRequest<Result<bool>>
    {
        public IFormFile File { get; set; }

        public string Domain { get; set; }

        public Guid FeedId { get; set; }

        public Guid MessageId { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly ICloudRepository _cloudRepository;

        private readonly IRepository<MessageFile> _fileRepository;

        private readonly IRepository<Message> _messageRepository;

        public Handler(ICloudRepository cloudRepository, IRepository<MessageFile> repository, IRepository<Message> messageRepository)
        {
            _cloudRepository = cloudRepository;
            _fileRepository = repository;
            _messageRepository = messageRepository;
        }

        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var fileName = Guid.NewGuid();
            var extension = request.File.GetFileExtension();

            var path = PathExtension.GetPath<MessageFile>(request.Domain, fileName.ToString() + '.' + extension, request.FeedId.ToString());

            var uploadResult = await _cloudRepository.UploadFile(request.File, path);
            if (!uploadResult.IsSuccessful)
            {
                return uploadResult;
            }

            var createResult = await _fileRepository.AddItemAsync(new MessageFile() { Id = fileName, MessageId = request.MessageId, Name = path, PublicUrl = _cloudRepository.GetFileUrl(path) });
            if (!createResult.IsSuccessful)
            {
                return createResult;
            }

            await SetMessageFileId(request.MessageId, fileName);

            return Result.Success<bool>();
        }

        private async Task SetMessageFileId(Guid messageId, Guid messageFileId) 
        {
            var message = (await _messageRepository.GetByIdAsync(messageId)).Data;
            message.MessageFileId = messageFileId;
            await _messageRepository.UpdateItemAsync(message);
        }
    }
}

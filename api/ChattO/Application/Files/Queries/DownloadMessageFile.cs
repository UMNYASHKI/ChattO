using Application.Abstractions;
using Application.Extensions;
using Application.Helpers;
using Domain.Models;
using MediatR;
using FluentValidation;

namespace Application.Files.Queries;

public class DownloadMessageFile
{
    public class Command : IRequest<Result<string>>
    {
        public Guid MessageId { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.MessageId).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly ICloudRepository _cloudRepository;

        private readonly IRepository<Message> _repository;

        public Handler(ICloudRepository cloudRepository)
        {
            _cloudRepository = cloudRepository;
        }

        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var getMessageResult = await _repository.GetByIdAsync(request.MessageId);
            if (!getMessageResult.IsSuccessful) 
            {
                return Result.Failure<string>(getMessageResult.Message);
            }

            var message = getMessageResult.Data;

            if (message.MessageFileId != null && await _cloudRepository.FileExists(message.MessageFile.Name))
            {
                return Result.Success(message.MessageFile.PublicUrl);
            }

            return Result.Success<string>(null);
        }
    }
}

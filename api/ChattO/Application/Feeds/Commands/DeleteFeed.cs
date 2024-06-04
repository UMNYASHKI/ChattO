using Application.Abstractions;
using Application.Helpers;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.Feeds.Commands;

public class DeleteFeed
{
    public class Command : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly IRepository<Feed> _repository;
        private readonly IValidator<Command> _validator;
        public Handler(IRepository<Feed> repository, IValidator<Command> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Result.Failure<bool>(validationResult.ToString(" "));

            var deleteResult = await _repository.DeleteItemAsync(request.Id);

            return deleteResult.IsSuccessful ? Result.Success(true) : Result.Failure<bool>(deleteResult.Message);
        }
    }
}

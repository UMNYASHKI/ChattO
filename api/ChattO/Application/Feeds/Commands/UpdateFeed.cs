using Application.Abstractions;
using Application.Helpers;
using Application.Helpers.Mappings;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.Feeds.Commands;

public class UpdateFeed
{
    public class Command : IRequest<Result<bool>>, IMapWith<Feed>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
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

            return await _repository.PartialUpdateAsync(request.Id, request);
        }
    }
}

using Application.Abstractions;
using Application.Helpers;
using Domain.Models;
using FluentValidation;
using MediatR;


namespace Application.Organizations.Commands;

public class DeleteOrganization
{
    public class Command : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty);
        }
    }

    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly IRepository<Organization> _repository;
        private readonly IValidator<Command> _validator;
        public Handler(IRepository<Organization> repository, IValidator<Command> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return Result.Failure<bool>(validationResult.ToString(" "));

            return await _repository.DeleteItemAsync(request.Id);
        }
    }

}

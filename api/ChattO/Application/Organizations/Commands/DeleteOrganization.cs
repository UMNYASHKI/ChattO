using Application.Helpers;
using Application.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        private readonly IChattoDbContext _dbContext;
        private readonly IValidator<Command> _validator;
        public Handler(IChattoDbContext dbContext, IValidator<Command> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }

        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var organization = await _dbContext.Organizations
                .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

            if (organization is null)
                return Result.Failure<bool>($"Organization with id {request.Id} not found");

            try
            {
                _dbContext.Organizations.Remove(organization);
                var result = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

                return result ? Result.Success(true) : Result.Failure<bool>("Failed to delete organization");
            }
            catch
            {
                return Result.Failure<bool>("Failed to delete organization");
            }
        }
    }

}

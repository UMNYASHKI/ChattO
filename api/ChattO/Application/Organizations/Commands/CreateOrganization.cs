using Application.Interfaces;
using FluentValidation;
using Domain.Models;
using MediatR;
using Application.Helpers;

namespace Application.Organizations.Commands;

public class CreateOrganization
{
    public class  Command : IRequest<Result<Guid>>
    {
        public string Name { get; set; }
        public string Domain { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Domain).NotEmpty();
        }
    }
    public class Handler : IRequestHandler<Command, Result<Guid>>
    {
        private readonly IChattoDbContext _dbContext;
        public Handler(IChattoDbContext dbContext) => 
            _dbContext = dbContext;
        public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            var organization = new Organization
            {
                Name = request.Name,
                Domain = request.Domain
            };

            try
            {
                await _dbContext.Organizations.AddAsync(organization);
                var result = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

                if (!result)
                    return Result.Failure<Guid>("Failed to create organization");

                return Result.Success(organization.Id);
            }
            catch
            {
                return Result.Failure<Guid>("Failed to save organization");
            }
        }
    }
}

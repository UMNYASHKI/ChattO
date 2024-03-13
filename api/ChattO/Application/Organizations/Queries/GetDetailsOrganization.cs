using Application.Helpers;
using Application.Interfaces;
using Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Organizations.Queries;

public class GetDetailsOrganization
{
    public class Query : IRequest<Result<Organization>>
    {
        public Guid Id { get; set; }
    }
    public class QueryValidator : AbstractValidator<Query>
    {
        public QueryValidator()
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty);
        }
    }
    public class Handler : IRequestHandler<Query, Result<Organization>>
    {
        private readonly IChattoDbContext _dbContext;
        public Handler(IChattoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result<Organization>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                var organizationProxy = await _dbContext.Organizations
                                    .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

                if (organizationProxy is null)
                    return Result.Failure<Organization>($"Organization with id {request.Id} not found");

                return Result.Success(organizationProxy);
            }
            catch
            {
                return Result.Failure<Organization>("Failed to get organization");
            }
        }

    }
}

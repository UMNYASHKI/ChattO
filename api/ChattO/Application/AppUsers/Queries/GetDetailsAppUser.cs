using Application.Abstractions;
using Application.Helpers;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.Users.Queries;

public class GetDetailsAppUser
{
    public class Query : IRequest<Result<AppUser>>
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
    public class Handler : IRequestHandler<Query, Result<AppUser>>
    {
        private readonly IRepository<AppUser> _repository;
        public Handler(IRepository<AppUser> repository)
        {
            _repository = repository;
        }
        public async Task<Result<AppUser>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}

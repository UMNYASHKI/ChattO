using Application.Abstractions;
using Application.Helpers;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.Feeds.Queries;

public class GetDetailsFeed
{
    public class Query : IRequest<Result<Feed>>
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
    public class Handler : IRequestHandler<Query, Result<Feed>>
    {
        private readonly IRepository<Feed> _repository;
        public Handler(IRepository<Feed> repository)
        {
            _repository = repository;
        }
        public async Task<Result<Feed>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}

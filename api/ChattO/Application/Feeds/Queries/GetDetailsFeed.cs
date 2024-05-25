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
        private readonly IValidator<Query> _validator;
        public Handler(IRepository<Feed> repository, IValidator<Query> validator)
        {
            _repository = repository;
            _validator = validator;
        }
        public async Task<Result<Feed>> Handle(Query request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Result.Failure<Feed>(validationResult.ToString(" "));

            return await _repository.GetByIdAsync(request.Id);
        }
    }
}

using Application.Abstractions;
using Application.Helpers;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.Feeds.Queries;

public class GetFeedAppUsers
{
    public class Query : IRequest<Result<List<AppUserFeed>>>
    {
        public Guid FeedId { get; set; }
    }
    public class QueryValidator : AbstractValidator<Query>
    {
        public QueryValidator()
        {
            RuleFor(x => x.FeedId).NotEqual(Guid.Empty);
        }
    }

    public class Handler : IRequestHandler<Query, Result<List<AppUserFeed>>>
    {
        private readonly IRepository<AppUserFeed> _repository;
        private readonly IValidator<Query> _validator;
        public Handler(IRepository<AppUserFeed> repository, IValidator<Query> validator)
        {
            _repository = repository;
            _validator = validator;
        }
        public async Task<Result<List<AppUserFeed>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Result.Failure<List<AppUserFeed>>(validationResult.ToString(" "));

            var appUserFeedsResult = await _repository.GetAllAsync(x => x.FeedId == request.FeedId);
            if (appUserFeedsResult == null)
                return Result.Failure<List<AppUserFeed>>(appUserFeedsResult.Message);

            return Result.Success(appUserFeedsResult.Data.ToList());
        }
    }
}

using Application.Abstractions;
using Application.Helpers;
using Domain.Enums;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.Feeds.Queries;

public class GetListFeeds
{
    public class  Query : PagingProps, IRequest<Result<PagingResponse<Feed>>>
    {
        [Filter]
        public string? Name { get; set; }
        [Filter]
        public string? Description { get; set; }
        [Filter]
        public FeedType? Type { get; set; }
        [Filter]
        public Guid? GroupId { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<PagingResponse<Feed>>>
    {
        private readonly IRepository<Feed> _repository;
        private readonly IValidator<PagingProps> _validator;
        public Handler(IRepository<Feed> repository, IValidator<PagingProps> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<PagingResponse<Feed>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Result.Failure<PagingResponse<Feed>>(validationResult.ToString(" "));

            var filter = ExpressionFilter<Feed, Query>.GetFilter(request);
            var sortBy = SortingBuilder<Feed>.GetSortBy(request.ColumnName, request.Descending ?? true);
            if (!sortBy.IsSuccessful)
                return Result.Failure<PagingResponse<Feed>>(sortBy.Message);

            var listResult = await _repository.GetAllAsync(filter, sortBy.Data, request.PageNumber, request.PageSize);
            if(!listResult.IsSuccessful)
                return Result.Failure<PagingResponse<Feed>>(listResult.Message);

            var totalAmountResult = await _repository.GetTotalCountAsync(filter);
            if(!totalAmountResult.IsSuccessful)
                return Result.Failure<PagingResponse<Feed>>(totalAmountResult.Message);

            return Result.Success(new PagingResponse<Feed>(listResult.Data, totalAmountResult.Data,
                request.PageNumber, request.PageSize));
        }
    }
}

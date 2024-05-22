using Application.Abstractions;
using Application.Helpers;
using Application.Helpers.Mappings;
using AutoMapper;
using Domain.Enums;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.BillInfo.Queries;

public class Get
{
    public class Query : PagingProps, IRequest<Result<PagingResponse<BillingInfo>>>
    {
        [Filter]
        public BillingType? Type { get; set; }

        [Filter]
        public double? Price { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<PagingResponse<BillingInfo>>>
    {
        private readonly IRepository<BillingInfo> _repository;

        public Handler(IRepository<BillingInfo> repository)
        {
            _repository = repository;
        }

        public async Task<Result<PagingResponse<BillingInfo>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var expression = ExpressionFilter<BillingInfo, Query>.GetFilter(request);
            var sorting = SortingBuilder<BillingInfo>.GetSortBy(request.ColumnName, (bool)request.Descending);

            var getResult = await _repository.GetAllAsync(expression, sorting, request.PageNumber, request.PageSize);
            if (!getResult.IsSuccessful)
            {
                return Result.Failure<PagingResponse<BillingInfo>>(getResult.Message);
            }

            var totalCount = await _repository.GetTotalCountAsync(expression);

            return Result.Success(new PagingResponse<BillingInfo>(getResult.Data, totalCount.Data, request.PageNumber, request.PageSize));
        }
    }
}

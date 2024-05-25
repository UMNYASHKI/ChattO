using Application.Abstractions;
using Application.Helpers;
using Application.Helpers.Mappings;
using AutoMapper;
using Domain.Enums;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.Groups.Queries;

public class Get
{
    public class Query : PagingProps, IRequest<Result<PagingResponse<Group>>>
    {
        [Filter]
        public string? Name { get; set; }

        [Filter]
        public Guid? OrganizationId { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<PagingResponse<Group>>>
    {
        private readonly IRepository<Group> _repository;

        public Handler(IRepository<Group> repository)
        {
            _repository = repository;
        }

        public async Task<Result<PagingResponse<Group>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var expression = ExpressionFilter<Group, Query>.GetFilter(request);
            var sorting = SortingBuilder<Group>.GetSortBy(request.ColumnName, (bool)request.Descending);
            if (!sorting.IsSuccessful)
            {
                return Result.Failure<PagingResponse<Group>>(sorting.Message);
            }

            var getResult = await _repository.GetAllAsync(expression, sorting.Data, request.PageNumber, request.PageSize);
            if (!getResult.IsSuccessful)
            {
                return Result.Failure<PagingResponse<Group>>(getResult.Message);
            }

            var totalCount = await _repository.GetTotalCountAsync(expression);

            return Result.Success(new PagingResponse<Group>(getResult.Data, totalCount.Data, request.PageNumber, request.PageSize));
        }
    }
}

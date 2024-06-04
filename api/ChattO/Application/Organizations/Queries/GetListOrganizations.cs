using Application.Abstractions;
using Application.Helpers;
using Domain.Models;
using FluentValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Organizations.Queries;

public class GetListOrganizations
{
    public class Query : PagingProps, IRequest<Result<PagingResponse<Organization>>>
    {
        [Filter]
        public string? Name { get; set; }
        [Filter]
        public string? Domain { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<PagingResponse<Organization>>>
    {
        private readonly IRepository<Organization> _repository;
        private readonly IValidator<PagingProps> _validator;
        public Handler(IRepository<Organization> repository, IValidator<PagingProps> validator)
        {
            _repository = repository;
            _validator = validator;
        }
        public async Task<Result<PagingResponse<Organization>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Result.Failure<PagingResponse<Organization>>(validationResult.ToString(" "));

            var filter = ExpressionFilter<Organization, Query>.GetFilter(request);
            var sortBy = SortingBuilder<Organization>.GetSortBy(request.ColumnName, request.Descending ?? true);
            if (!sortBy.IsSuccessful)
                return Result.Failure<PagingResponse<Organization>>(sortBy.Message);

            var listResult = await _repository.GetAllAsync(filter, sortBy.Data, request.PageNumber, request.PageSize);
            if(!listResult.IsSuccessful)
                return Result.Failure<PagingResponse<Organization>>(listResult.Message);
            
            var totalAmountResult = await _repository.GetTotalCountAsync(filter);
            if(!totalAmountResult.IsSuccessful)
                return Result.Failure<PagingResponse<Organization>>(totalAmountResult.Message);

            return Result.Success(new PagingResponse<Organization>(listResult.Data, totalAmountResult.Data,
                request.PageNumber, request.PageSize));
        }
    }
}

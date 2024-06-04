using Application.Abstractions;
using Application.Helpers;
using Domain.Enums;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.AppUsers.Queries;

public class GetListAppUsers
{
    public class  Query : PagingProps, IRequest<Result<PagingResponse<AppUser>>>
    {
        [Filter]
        public Guid? GroupId { get; set; }
        [Filter]
        public string? UserName { get; set; }
        [Filter]
        public AppUserRole? Role { get; set; }
        [Filter]
        public string? Email { get; set; }
        [Filter]
        public Guid? OrganizationId { get; set; }
        [Filter]
        public bool? IsEmailSent { get; set; }
        [Filter]
        public string? DisplayName { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<PagingResponse<AppUser>>>
    {
        private readonly IRepository<AppUser> _repository;
        private readonly IValidator<PagingProps> _validator;
        public Handler(IRepository<AppUser> repository, IValidator<PagingProps> validator)
        {
            _repository = repository;
            _validator = validator;
        }
        public async Task<Result<PagingResponse<AppUser>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Result.Failure<PagingResponse<AppUser>>(validationResult.ToString(" "));

            var filter = ExpressionFilter<AppUser, Query>.GetFilter(request);
            var sortBy = SortingBuilder<AppUser>.GetSortBy(request.ColumnName, request.Descending ?? true);
            if (!sortBy.IsSuccessful)
                return Result.Failure<PagingResponse<AppUser>>(sortBy.Message);

            var listResult = await _repository.GetAllAsync(filter, sortBy.Data, request.PageNumber, request.PageSize);
            if(!listResult.IsSuccessful)
                return Result.Failure<PagingResponse<AppUser>>(listResult.Message);

            var totalAmountResult = await _repository.GetTotalCountAsync(filter);
            if(!totalAmountResult.IsSuccessful)
                return Result.Failure<PagingResponse<AppUser>>(totalAmountResult.Message);

            return Result.Success(new PagingResponse<AppUser>(listResult.Data, totalAmountResult.Data, 
                request.PageNumber, request.PageSize));
        }
    }
}

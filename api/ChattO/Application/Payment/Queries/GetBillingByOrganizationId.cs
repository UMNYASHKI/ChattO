using Application.Abstractions;
using Application.Helpers;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using MediatR;
using System.Collections.Generic;

namespace Application.Payment.Queries;

public class GetBillingByOrganizationId
{
    public class Query : IRequest<Result<PagingResponse<Billing>>> 
    {
        public Guid OrganizationId { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public string ColumnName { get; set; }

        public bool Descending { get; set; }
    }

    public class Validator : AbstractValidator<Query> 
    {
        public Validator()
        {
            RuleFor(x=>x.OrganizationId).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Query, Result<PagingResponse<Billing>>>
    {
        private readonly IValidator<Query> _validator;

        private readonly IRepository<Billing> _repository;

        public Handler(IValidator<Query> validator, IRepository<Billing> repository)
        {
            _validator = validator;
            _repository = repository;
        }

        public async Task<Result<PagingResponse<Billing>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid) 
            {
                return Result.Failure<PagingResponse<Billing>>(validationResult.ToString(" "));
            }

            var sorting = SortingBuilder<Billing>.GetSortBy(request.ColumnName, request.Descending);
            var getResult = await _repository.GetAllAsync(billing=>billing.OrganizationId == request.OrganizationId, sorting, request.PageNumber, request.PageSize);
            if (!getResult.IsSuccessful)
            {
                return Result.Failure<PagingResponse<Billing>>(getResult.Message);
            }

            var totalCount = await _repository.GetTotalCountAsync();

            return Result.Success(new PagingResponse<Billing>(getResult.Data, totalCount.Data, request.PageNumber, request.PageSize));
        }
    }
}

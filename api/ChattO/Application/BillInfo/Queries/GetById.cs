using Application.Abstractions;
using Application.Helpers;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.BillInfo.Queries;

public class GetById
{
    public class Query : IRequest<Result<BillingInfo>> 
    {
        public Guid Id { get; set; }
    }

    public class QueryValidator : AbstractValidator<Query> 
    {
        public QueryValidator()
        {
            RuleFor(x=>x.Id).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Query, Result<BillingInfo>>
    {
        private readonly IValidator<Query> _validator;

        private readonly IRepository<BillingInfo> _repository;

        public Handler(IValidator<Query> validator, IRepository<BillingInfo> repository)
        {
            _validator = validator;
            _repository = repository;
        }

        public async Task<Result<BillingInfo>> Handle(Query request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Result.Failure<BillingInfo>(validationResult.ToString(" "));
            }

            return await _repository.GetByIdAsync(request.Id);
        }
    }
}

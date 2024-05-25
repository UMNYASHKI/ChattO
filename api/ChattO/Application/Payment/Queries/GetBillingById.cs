using Application.Abstractions;
using Application.Helpers;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.Payment.Queries;

public class GetBillingById
{
    public class Query : IRequest<Result<Billing>> 
    {
        public Guid Id { get; set; }
    }

    public class Validator : AbstractValidator<Query> 
    {
        public Validator()
        {
            RuleFor(x=>x.Id).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Query, Result<Billing>>
    {
        private readonly IValidator<Query> _validator;

        private readonly IRepository<Billing> _repository;

        public Handler(IValidator<Query> validator, IRepository<Billing> repository)
        {
            _validator = validator;
            _repository = repository;
        }

        public async Task<Result<Billing>> Handle(Query request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid) 
            {
                return Result.Failure<Billing>(validationResult.ToString(" "));
            }

            return await _repository.GetByIdAsync(request.Id);
        }
    }
}

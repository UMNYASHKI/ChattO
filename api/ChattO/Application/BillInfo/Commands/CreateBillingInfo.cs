using Application.Abstractions;
using Application.Helpers;
using Domain.Enums;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.BillInfo.Commands;

public class CreateBillingInfo
{
    public class Command : IRequest<Result<bool>>
    {
        public BillingType Type { get; set; }

        public double Price { get; set; }

        public Currency Currency { get; set; } = Currency.USD;
    }

    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly IRepository<BillingInfo> _repository;

        public Handler(IRepository<BillingInfo> repository)
        {
            _repository = repository;
        }

        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var createResult = await _repository.AddItemAsync(new BillingInfo() 
            {
                Type = request.Type,
                Price = request.Price,
                Currency = request.Currency,
            });
            if (!createResult.IsSuccessful)
            {
                return Result.Failure<bool>(createResult.Message);
            }

            return Result.Success<bool>();
        }
    }
}

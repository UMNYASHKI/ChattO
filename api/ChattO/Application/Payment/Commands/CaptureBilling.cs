using Application.Abstractions;
using Application.Helpers;
using Application.Payment.DTOs;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.Payment.Commands;

public class CaptureBilling
{
    public class Command : IRequest<Result<CaptureOrderResponse>>
    {
        public Guid BillingId { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.BillingId).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, Result<CaptureOrderResponse>>
    {
        private readonly IValidator<Command> _validator;

        private readonly IBillingService _billingService;

        private readonly IRepository<Billing> _billingRepository;

        public Handler(IValidator<Command> validator, IBillingService billingService, IRepository<Billing> repository)
        {
            _validator = validator;
            _billingService = billingService;
            _billingRepository = repository;
        }

        public async Task<Result<CaptureOrderResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return Result.Failure<CaptureOrderResponse>(validationResult.ToString(" "));
            }

            var getBillingResult = await _billingRepository.GetByIdAsync(request.BillingId);
            if (!getBillingResult.IsSuccessful)
            {
                return Result.Failure<CaptureOrderResponse>("Failed to fing billing");
            }

            var billing = getBillingResult.Data;

            var captureOrderResult = await _billingService.CaptureOrder(request.BillingId.ToString());
            if (!captureOrderResult.IsSuccessful)
            {
                return Result.Failure<CaptureOrderResponse>(captureOrderResult.Message);
            }

            billing.Status = BillingStatus.Captured;
            await _billingRepository.UpdateItemAsync(billing);

            return Result.Success(captureOrderResult.Data);
        }
    }
}

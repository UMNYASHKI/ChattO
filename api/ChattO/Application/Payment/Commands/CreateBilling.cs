using Application.Abstractions;
using Application.Helpers;
using Application.Helpers.Mappings;
using Application.Payment.DTOs;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.Payment.Commands;

public class CreateBilling
{
    public class Command : IRequest<Result<CreateOrderResponse>>, IMapWith<Billing>
    {
        public Guid BillingInfoId { get; set; }

        public Guid OrganizationId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Command, Billing>()
                .ForMember(org => org.BillingInfoId, opt => opt.MapFrom(c => c.BillingInfoId))
                .ForMember(org => org.OrganizationId, opt => opt.MapFrom(c => c.OrganizationId));
        }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.BillingInfoId).NotEmpty();
            RuleFor(x => x.OrganizationId).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, Result<CreateOrderResponse>>
    {
        private readonly IBillingService _billingService;

        private readonly IRepository<Billing> _billingRepository;

        private readonly IRepository<BillingInfo> _billingInfoRepository;

        private readonly IValidator<Command> _validator;

        private readonly IMapper _mapper;

        public Handler(IBillingService billingService, IRepository<Billing> repository, IRepository<BillingInfo> billingInfoRepository, IValidator<Command> validator, IMapper mapper)
        {
            _billingService = billingService;
            _billingRepository = repository;
            _billingInfoRepository = billingInfoRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<Result<CreateOrderResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return Result.Failure<CreateOrderResponse>(validationResult.ToString(" "));
            }

            var billingId = Guid.NewGuid();
            var billing = _mapper.Map<Billing>(request);
            billing.Id = billingId;

            var createBillingResult = await _billingRepository.AddItemAsync(billing);
            if (!createBillingResult.IsSuccessful)
            {
                return Result.Failure<CreateOrderResponse>(createBillingResult.Message);
            }

            var billingInfo = (await _billingInfoRepository.GetByIdAsync(request.BillingInfoId)).Data;
            var createOrderResult = await _billingService.CreateOrder(billingInfo.Price.ToString(), billingInfo.Currency.ToString(), billingId.ToString());
            if (!createOrderResult.IsSuccessful) 
            {
                return Result.Failure<CreateOrderResponse>("Failed to create order" + createOrderResult.Message);
            }

            var response = createOrderResult.Data;
            response.BillingId = billingId;
            return Result.Success(response);
        }
    }
}

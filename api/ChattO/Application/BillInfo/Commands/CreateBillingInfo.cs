using Application.Abstractions;
using Application.Helpers;
using Application.Helpers.Mappings;
using AutoMapper;
using Domain.Enums;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.BillInfo.Commands;

public class CreateBillingInfo
{
    public class Command : IRequest<Result<bool>>, IMapWith<BillingInfo>
    {
        public BillingType Type { get; set; }

        public double Price { get; set; }

        public Currency Currency { get; set; } = Currency.USD;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Command, BillingInfo>()
                .ForMember(org => org.Type, opt => opt.MapFrom(c => c.Type))
                .ForMember(org => org.Currency, opt => opt.MapFrom(c => c.Currency))
                .ForMember(org => org.Price, opt => opt.MapFrom(c => c.Price));
        }
    }

    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly IRepository<BillingInfo> _repository;

        private readonly IMapper _mapper;

        public Handler(IRepository<BillingInfo> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var createResult = await _repository.AddItemAsync(_mapper.Map<BillingInfo>(request));
            if (!createResult.IsSuccessful)
            {
                return Result.Failure<bool>(createResult.Message);
            }

            return Result.Success<bool>();
        }
    }
}

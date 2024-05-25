using Application.Abstractions;
using Application.Helpers;
using Application.Helpers.Mappings;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Organizations.Commands;

public class UpdateOrganization
{
    public class Command : IRequest<Result<bool>>, IMapWith<Organization>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Command, Organization>()
                .ForMember(org => org.Name,
                    opt => opt.MapFrom(c => c.Name))
                .ForMember(org => org.Domain,
                    opt => opt.MapFrom(c => c.Domain));
        }
    }
    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Domain).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly IRepository<Organization> _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<Command> _validator;
        public Handler(IRepository<Organization> repository, IMapper mapper, IValidator<Command> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return Result.Failure<bool>(validationResult.ToString(" "));

            var isUnique = await _repository.IsUnique(o => o.Name == request.Name || o.Domain == request.Domain);

            if (!isUnique.IsSuccessful)
                return Result.Failure<bool>($"{nameof(Organization)} with this name or domain already exists");

            var organization = await _repository.GetByIdAsync(request.Id);

            if (organization.Data is null)
                return Result.Failure<bool>($"Organization with id {request.Id} not found");

            _mapper.Map(request, organization.Data);

            return await _repository.UpdateItemAsync(organization.Data);
        }
    }
}

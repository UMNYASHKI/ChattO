using Application.Helpers.Mappings;
using Application.Helpers;
using AutoMapper;
using Domain.Models;
using MediatR;
using FluentValidation;
using Application.Interfaces;
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
        private readonly IChattoDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IValidator<Command> _validator;
        public Handler(IChattoDbContext dbContext, IMapper mapper, IValidator<Command> validator)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return Result.Failure<bool>(validationResult.ToString(" "));

            var organization = await _dbContext.Organizations
                .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

            if (organization is null)
                return Result.Failure<bool>($"Organization with id {request.Id} not found");

            _mapper.Map(request, organization);

            try
            {
                var result = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

                return result ? Result.Success<bool>() : Result.Failure<bool>("Failed to update organization");
            }
            catch
            {
                return Result.Failure<bool>("Failed to update organization");
            }
        }
    }
}

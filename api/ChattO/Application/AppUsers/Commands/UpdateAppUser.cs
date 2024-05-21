using Application.Abstractions;
using Application.Helpers;
using Application.Helpers.Mappings;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.AppUsers.Commands;

public class UpdateAppUser
{
    public class Command : IRequest<Result<bool>>, IMapWith<AppUser>
    {
        public Guid Id { get; set; }
        public string? DisplayName { get; set; }

        public string? Email { get; set; }

        public string? UserName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Command, AppUser>()
                .ForMember(q => q.Id, opt => opt.MapFrom(r => r.Id))
                .ForMember(q => q.DisplayName, opt => opt.MapFrom(r => r.DisplayName))
                .ForMember(q => q.Email, opt => opt.MapFrom(r => r.Email))
                .ForMember(q => q.UserName, opt => opt.MapFrom(r => r.UserName));
        }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly IRepository<AppUser> _repository;
        private readonly IValidator<Command> _validator;

        public Handler(IRepository<AppUser> repository, IValidator<Command> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Result.Failure<bool>(validationResult.ToString(" "));

            return await _repository.PartialUpdateAsync(request.Id, request);
        }
    }
}

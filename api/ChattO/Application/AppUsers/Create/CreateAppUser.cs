using Application.Abstractions;
using Application.DTOs.EmailData;
using Application.Helpers;
using Application.Helpers.Mappings;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.AppUsers.Create;

public class CreateAppUser
{
    public class Command : IRequest<Result<bool>>, IMapWith<AppUser>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Command, AppUser>()
                .ForMember(user => user.DisplayName,
                                   opt => opt.MapFrom(c => c.FirstName + " " + c.LastName)) // How does the diplayName should look like?
                .ForMember(user => user.UserName,
                                   opt => opt.MapFrom(c => c.UserName))
                .ForMember(user => user.Email,
                                   opt => opt.MapFrom(c => c.Email));
        }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }

    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly IRepository<AppUser> _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<Command> _validator;
        private readonly IEmailService _emailService;
        public Handler(IRepository<AppUser> repository, IMapper mapper, 
            IValidator<Command> validator, IEmailService emailService)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
            _emailService = emailService;
        }
        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Result.Failure<bool>(validationResult.ToString(" "));

            var isUniqueResult = await _repository.IsUnique(u => u.Email == request.Email);
            if (!isUniqueResult.IsSuccessful)
            {
                return Result.Failure<bool>(isUniqueResult.Message);
            }
            else if (!isUniqueResult.Data)
            {
                return Result.Failure<bool>($"User with email {request.Email} already exists");
            }

            var password = PasswordGenerator.GenerateRandomPassword();
            var user = _mapper.Map<AppUser>(request);
            user.PasswordHash = password;

            var result = await _repository.AddItemAsync(user);
            if (!result.IsSuccessful)
                return Result.Failure<bool>($"Failed to create user");

            var emailResult = await SendEmail(user, password);
            if (!emailResult.IsSuccessful)
                return Result.Failure<bool>("User was created." + emailResult.Message);

            return Result.Success<bool>();
        }

        private async Task<Result<bool>> SendEmail(AppUser user, string password)
        {
            var result = await _emailService.SendEmailOnUserRegistration(
                new UserRegistration(user.DisplayName,
                    user.Email, 
                    user.Role,
                    password));
            if (!result.IsSuccessful) 
                return Result.Failure<bool>($"Failed to send email to {user.Email}");

            return Result.Success<bool>();
        }
    }
}

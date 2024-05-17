using Application.Abstractions;
using Application.DTOs.EmailData;
using Application.Helpers;
using Application.Helpers.Mappings;
using AutoMapper;
using Domain.Enums;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.AppUsers.Commands;

public class CreateAppUser
{
    public class Command : IRequest<Result<bool>> 
    {
        public List<CommandUser> AppUsers { get; set; }
        public AppUserRole Role { get; set; }
        public Guid OrganizationId { get; set; }
        
        public class CommandUser : IMapWith<AppUser>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }

            public void Mapping(Profile profile)
            {
                profile.CreateMap<CommandUser, AppUser>()
                    .ForMember(user => user.DisplayName,
                                       opt => opt.MapFrom(c => c.FirstName + " " + c.LastName)) // How does the diplayName should look like?
                    .ForMember(user => user.UserName,
                                       opt => opt.MapFrom(c => c.UserName))
                    .ForMember(user => user.Email,
                                       opt => opt.MapFrom(c => c.Email));
            }
        }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.OrganizationId).NotEmpty();
            RuleFor(x => x.AppUsers).NotEmpty();
        }
    }

    public class UserCommandValidator : AbstractValidator<Command.CommandUser>
    {
        public UserCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }

    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly IRepository<AppUser> _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<Command> _validatorCommand;
        private readonly IValidator<Command.CommandUser> _validatorUser;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        public Handler(IRepository<AppUser> repository, IMapper mapper, 
            IValidator<Command> validator, IEmailService emailService,
            IUserService userService, IValidator<Command.CommandUser> validatorUser)
        {
            _repository = repository;
            _mapper = mapper;
            _validatorCommand = validator;
            _emailService = emailService;
            _userService = userService;
            _validatorUser = validatorUser;
        }
        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorCommand.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Result.Failure<bool>(validationResult.ToString(" "));

            var emailUniquenessResult = await CheckOnUniquenes(request.AppUsers.Select(u => u.Email).ToList());
            if (!emailUniquenessResult.IsSuccessful)
                return Result.Failure<bool>(emailUniquenessResult.Message);

            var usersResult = await MapAppUsers(request);
            if (!usersResult.IsSuccessful)
                return Result.Failure<bool>(usersResult.Message);

            var userRegistrations = GetUserRegistrations(usersResult.Data);

            var result = await RegisterUsers(usersResult.Data);
            if (!result.IsSuccessful)
                return Result.Failure<bool>(result.Message);

            var emailResult = await SendEmail(userRegistrations);
            if (!emailResult.IsSuccessful)
                return Result.Failure<bool>("Users were created." + emailResult.Message);

            return Result.Success<bool>();
        }

        private async Task<Result<bool>> SendEmail(List<UserRegistration> users)
        {
            var result = await _emailService.SendEmailOnUserRegistration(users);
            if (!result.IsSuccessful) 
                return Result.Failure<bool>($"Failed to send emails");

            return Result.Success<bool>();
        }

        private List<UserRegistration> GetUserRegistrations(List<AppUser> users)
        {
            var userRegistrations = new List<UserRegistration>();
            foreach (var user in users)
            {
                userRegistrations.Add(new UserRegistration(user.DisplayName, user.Email, user.Role, user.PasswordHash));
            }
            return userRegistrations;
        }

        private async Task<Result<List<AppUser>>> MapAppUsers(Command request)
        {
            var appUsers = new List<AppUser>();
            foreach (var user in request.AppUsers)
            {
                var validationResult = await _validatorUser.ValidateAsync(user);
                if (!validationResult.IsValid)
                    return Result.Failure<List<AppUser>>(validationResult.ToString(" "));

                var appUser = _mapper.Map<AppUser>(user);
                appUser.OrganizationId = request.OrganizationId;
                appUser.Role = request.Role;
                appUser.PasswordHash = PasswordGenerator.GenerateRandomPassword();
                appUsers.Add(appUser);
            }

            return Result.Success(appUsers);
        }

        private async Task<Result<bool>> CheckOnUniquenes(List<string> emails)
        {
            var errors = new List<string>();
            foreach (var email in emails)
            {
                var result = await _repository.IsUnique(u => u.Email == email);
                if (!result.Data)
                    errors.Add($"User with email {email} already exists.");
            }

            if (errors.Any())
                return Result.Failure<bool>(string.Join(" ", errors));

            return Result.Success<bool>();
        }

        private async Task<Result<bool>> RegisterUsers(List<AppUser> appUsers)
        {
            var tasks = new List<Task<Result<bool>>>();
            foreach (var user in appUsers)
            {
                tasks.Add(_userService.RegisterUserAsync(user));
            }

            var results = await Task.WhenAll(tasks);

            if (results.Any(r => !r.IsSuccessful))
            {
                var errors = results.Where(r => !r.IsSuccessful).Select(r => r.Message).ToList();
                return Result.Failure<bool>(string.Join(" ", errors));
            }
                
            return Result.Success<bool>();
        }
    }
}

using Application.Abstractions;
using Application.Helpers;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.UserGroups.Commands;

public class Delete
{
    public class Command : IRequest<Result<bool>>
    {
        public Guid GroupId { get; set; }

        public ICollection<Guid> UsersId { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.GroupId).NotEmpty();
            RuleFor(x => x.UsersId).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly IRepository<AppUserGroup> _userGroupsRepository;

        private readonly IValidator<Command> _validator;

        public Handler(IRepository<AppUserGroup> userGroupRepository, IValidator<Command> validator)
        {
            _userGroupsRepository = userGroupRepository;
            _validator = validator;
        }
        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Result.Failure<bool>(validationResult.ToString(" "));
            }

            var userGroups = await GetUserGroups(request);
            if (!userGroups.IsSuccessful)
            {
                return Result.Failure<bool>(userGroups.Message);
            }

            return await _userGroupsRepository.DeleteItemsAsync(userGroups.Data);
        }

        private async Task<Result<IEnumerable<AppUserGroup>>> GetUserGroups(Command request)
        {
            var result = new List<AppUserGroup>();
            foreach (var userId in request.UsersId)
            {
                var data = await _userGroupsRepository.GetAllAsync(item => item.GroupId == request.GroupId && item.AppUserId == userId);
                if (!data.IsSuccessful)
                {
                    return data;
                }

                result.Add(data.Data.First());
            }

            return Result.Success((IEnumerable<AppUserGroup>)result);
        }
    }
}

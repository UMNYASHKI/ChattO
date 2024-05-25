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

    //public class Handler : IRequestHandler<Command, Result<bool>>
    //{
    //    private readonly IRepository<AppUserGroup> _userGroupsRepository;

    //    private readonly IValidator<Command> _validator;

    //    public Handler(IRepository<AppUserGroup> userGroupRepository, IValidator<Command> validator)
    //    {
    //        _userGroupsRepository = userGroupRepository;
    //        _validator = validator;
    //    }
    //    public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
    //    {
    //        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

    //        if (!validationResult.IsValid)
    //        {
    //            return Result.Failure<bool>(validationResult.ToString(" "));
    //        }

    //        var userGroups = request.UsersId.Select(userId => new AppUserGroup() { AppUserId = userId, GroupId = request.GroupId });
    //        return await _userGroupsRepository.DeleteItemsAsync(userGroups);
    //    }
    //}
}

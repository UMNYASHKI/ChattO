using Application.Abstractions;
using Application.Helpers;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.Groups.Commands;

public class Update
{
    public class Command : IRequest<Result<bool>>
    {
        public string? Name { get; set; }
        public List<Guid>? AddedUsersId { get; set; }
        public List<Guid>? RemovedUsersId { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly IRepository<Group> _repository;

        public Handler(IRepository<Group> repository)
        {
            _repository = repository;
        }
        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

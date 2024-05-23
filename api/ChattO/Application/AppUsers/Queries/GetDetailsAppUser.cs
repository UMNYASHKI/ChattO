using Application.Abstractions;
using Application.Helpers;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.AppUsers.Queries;

public class GetDetailsAppUser
{
    public class Query : IRequest<Result<AppUser>>
    {
        public Guid Id { get; set; }
    }
    public class QueryValidator : AbstractValidator<Query>
    {
        public QueryValidator()
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty);
        }
    }
    public class Handler : IRequestHandler<Query, Result<AppUser>>
    {
        private readonly IRepository<AppUser> _repository;
        private readonly IValidator<Query> _validator;
        public Handler(IRepository<AppUser> repository, IValidator<Query> validator)
        {
            _repository = repository;
            _validator = validator;
        }
        public async Task<Result<AppUser>> Handle(Query request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Result.Failure<AppUser>(validationResult.ToString(" "));

            return await _repository.GetByIdAsync(request.Id);
        }
    }
}

using Application.Abstractions;
using Application.Helpers;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.Groups.Queries;

public class GetById
{
    public class Query : IRequest<Result<Group>> 
    {
        public Guid Id { get; set; }
    }

    public class QueryValidator : AbstractValidator<Query> 
    {
        public QueryValidator()
        {
            RuleFor(x=>x.Id).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Query, Result<Group>>
    {
        private readonly IValidator<Query> _validator;

        private readonly IRepository<Group> _repository;

        public Handler(IValidator<Query> validator, IRepository<Group> repository)
        {
            _validator = validator;
            _repository = repository;
        }

        public async Task<Result<Group>> Handle(Query request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Result.Failure<Group>(validationResult.ToString(" "));
            }

            return await _repository.GetByIdAsync(request.Id);
        }
    }
}

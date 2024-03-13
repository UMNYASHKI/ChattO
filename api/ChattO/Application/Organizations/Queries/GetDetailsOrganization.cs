using Application.Abstractions;
using Application.Helpers;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Organizations.Queries;

public class GetDetailsOrganization
{
    public class Query : IRequest<Result<Organization>>
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
    public class Handler : IRequestHandler<Query, Result<Organization>>
    {
        private readonly IRepository<Organization> _repository;
        public Handler(IRepository<Organization> repository)
        {
            _repository = repository;
        }
        public async Task<Result<Organization>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}

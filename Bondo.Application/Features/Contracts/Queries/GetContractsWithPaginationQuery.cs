using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bondo.Application.DTOs.Contract;
using Bondo.Application.Extensions;
using Bondo.Application.Interfaces.Repositories;
using Bondo.Domain.Entities;
using Bondo.Shared;
using FluentValidation;
using MediatR;

namespace Bondo.Application.Features.Contracts.Queries;
public record GetContractWithPaginationQuery : IRequest<PaginatedResult<ContractDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public GetContractWithPaginationQuery() { }

    public GetContractWithPaginationQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}

internal class GetContractWithPaginationQueryHandler : IRequestHandler<GetContractWithPaginationQuery, PaginatedResult<ContractDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetContractWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<ContractDto>> Handle(GetContractWithPaginationQuery query, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Repository<Contract>().Entities
            //    .OrderBy(x => x.CreatedDate)
               .ProjectTo<ContractDto>(_mapper.ConfigurationProvider)
               .ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);
    }
}
public class GetContractWithPaginationValidator : AbstractValidator<GetContractWithPaginationQuery>
{
    public GetContractWithPaginationValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageSize at least greater than or equal to 1.");
    }
}
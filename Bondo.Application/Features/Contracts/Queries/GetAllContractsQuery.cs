using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bondo.Application.DTOs.Contract;
using Bondo.Application.Interfaces.Repositories;
using Bondo.Domain.Entities;
using Bondo.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bondo.Application.Features.Contracts.Queries;
public record GetAllContractsQuery : IRequest<Result<List<ContractDto>>>;

internal class GetAllContractsQueryHandler : IRequestHandler<GetAllContractsQuery, Result<List<ContractDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllContractsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<ContractDto>>> Handle(GetAllContractsQuery query, CancellationToken cancellationToken)
    { 
        var Contracts = await _unitOfWork.Repository<Contract>().Entities
                .ProjectTo<ContractDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

        return await Result<List<ContractDto>>.SuccessAsync(Contracts);
    }
}

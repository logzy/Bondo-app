using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bondo.Application.DTOs.Contract;
using Bondo.Application.Interfaces.Repositories;
using Bondo.Domain.Entities;
using Bondo.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bondo.Application;
public record GetAllContractApplicationsQuery : IRequest<Result<List<ContractApplicationDto>>>;


internal class GetAllContractApplicationsQueryHandler : IRequestHandler<GetAllContractApplicationsQuery, Result<List<ContractApplicationDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllContractApplicationsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<ContractApplicationDto>>> Handle(GetAllContractApplicationsQuery query, CancellationToken cancellationToken)
    { 
        var applications = await _unitOfWork.Repository<ContractApplication>().Entities
                .ProjectTo<ContractApplicationDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

        return await Result<List<ContractApplicationDto>>.SuccessAsync(applications);
    }
}
using AutoMapper;
using Bondo.Application.DTOs.Contract;
using Bondo.Application.Interfaces.Repositories;
using Bondo.Shared;
using Bondo.Domain.Entities;
using MediatR;

namespace Bondo.Application.Features.Contracts.Queries;
public record GetContractApplicationByIdQuery : IRequest<Result<ContractApplicationDto>>
{
    public int Id { get; set; }

    public GetContractApplicationByIdQuery()
    {

    }

    public GetContractApplicationByIdQuery(int id)
    {
        Id = id;
    }
}

internal class GetContractApplicationByIdQueryHandler : IRequestHandler<GetContractApplicationByIdQuery, Result<ContractApplicationDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetContractApplicationByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<ContractApplicationDto>> Handle(GetContractApplicationByIdQuery query, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<ContractApplication>().GetByIdAsync(query.Id);
        if(entity == null)
            return await Result<ContractApplicationDto>.FailureAsync("Contract Application not found!");

        
        var contract = _mapper.Map<ContractApplicationDto>(entity);
        return await Result<ContractApplicationDto>.SuccessAsync(contract);
    }
}

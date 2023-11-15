using AutoMapper;
using Bondo.Application.DTOs.Contract;
using Bondo.Application.Interfaces.Repositories;
using Bondo.Shared;
using Bondo.Domain.Entities;
using MediatR;

namespace Bondo.Application.Features.Contracts.Queries;
public record GetContractByIdQuery : IRequest<Result<ContractDto>>
{
    public int Id { get; set; }

    public GetContractByIdQuery()
    {

    }

    public GetContractByIdQuery(int id)
    {
        Id = id;
    }
}

internal class GetContractByIdQueryHandler : IRequestHandler<GetContractByIdQuery, Result<ContractDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetContractByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<ContractDto>> Handle(GetContractByIdQuery query, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<Contract>().GetByIdAsync(query.Id);
        var Contract = _mapper.Map<ContractDto>(entity);
        return await Result<ContractDto>.SuccessAsync(Contract);
    }
}

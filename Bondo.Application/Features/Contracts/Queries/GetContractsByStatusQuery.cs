using AutoMapper;
using Bondo.Application.DTOs.Contract;
using Bondo.Application.Interfaces.Repositories;
using Bondo.Domain.Enums;
using Bondo.Shared;
using MediatR;

namespace Bondo.Application.Features.Contracts.Queries;
public record class GetContractsByStatusQuery: IRequest<Result<List<ContractDto>>>
{
    public ContractEnums.ContractStatus Status { get; set; }

    public GetContractsByStatusQuery()
    {

    }

    public GetContractsByStatusQuery(ContractEnums.ContractStatus status)
    {
        Status = status;
    }
}

internal class GetContractsByStatusQueryHandler : IRequestHandler<GetContractsByStatusQuery, Result<List<ContractDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IContractRepository _contractRepository;

    public GetContractsByStatusQueryHandler(IUnitOfWork unitOfWork,
     IMapper mapper, IContractRepository contractRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _contractRepository = contractRepository;
    }

    public async Task<Result<List<ContractDto>>> Handle(GetContractsByStatusQuery query, CancellationToken cancellationToken)
    {
        var entity = await _contractRepository.GetContractsByStatusAsync(query.Status);
        var Contract = _mapper.Map<List<ContractDto>>(entity);
        return await Result<List<ContractDto>>.SuccessAsync(Contract);
    }
}
using AutoMapper;
using Bondo.Application.DTOs.Contract;
using Bondo.Application.Interfaces.Repositories;
using Bondo.Shared;
using MediatR;

namespace Bondo.Application;
public record class GetContractsByOwnerQuery : IRequest<Result<ContractDto>>
{
    public string OwnerId { get; set; }

    public GetContractsByOwnerQuery()
    {

    }

    public GetContractsByOwnerQuery(string ownerId)
    {
        OwnerId = ownerId;
    }
}

internal class GetContractsByOwnerQueryHandler : IRequestHandler<GetContractsByOwnerQuery, Result<ContractDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IContractRepository _contractRepository;

    public GetContractsByOwnerQueryHandler(IUnitOfWork unitOfWork,
     IMapper mapper, IContractRepository contractRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _contractRepository = contractRepository;
    }

    public async Task<Result<ContractDto>> Handle(GetContractsByOwnerQuery query, CancellationToken cancellationToken)
    {
        var entity = await _contractRepository.GetContractsByOwnerAsync(query.OwnerId);
        var Contract = _mapper.Map<ContractDto>(entity);
        return await Result<ContractDto>.SuccessAsync(Contract);
    }
}
using AutoMapper;
using Bondo.Application.DTOs.Contract;
using Bondo.Application.Interfaces.Repositories;
using Bondo.Shared;
using MediatR;

namespace Bondo.Application.Features.Contracts.Queries;
public record class GetContractsByContractorQuery : IRequest<Result<List<ContractDto>>>
{
    public string ContractorId { get; set; }

    public GetContractsByContractorQuery()
    {

    }

    public GetContractsByContractorQuery(string contractorId)
    {
        ContractorId = contractorId;
    }
}

internal class GetContractsByContractorQueryHandler : IRequestHandler<GetContractsByContractorQuery, Result<List<ContractDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IContractRepository _contractRepository;

    public GetContractsByContractorQueryHandler(IUnitOfWork unitOfWork,
     IMapper mapper, IContractRepository contractRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _contractRepository = contractRepository;
    }

    public async Task<Result<List<ContractDto>>> Handle(GetContractsByContractorQuery query, CancellationToken cancellationToken)
    {
        var entity = await _contractRepository.GetContractsByContractorAsync(query.ContractorId);
        var Contract = _mapper.Map<List<ContractDto>>(entity);
        return await Result<List<ContractDto>>.SuccessAsync(Contract);
    }
}
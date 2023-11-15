using AutoMapper;
using Bondo.Application.DTOs.Contract;
using Bondo.Application.Interfaces.Repositories;
using Bondo.Shared;
using MediatR;
using Bondo.Domain.Entities;
using Bondo.Domain.Enums;
namespace Bondo.Application.Features.Contracts.Queries;
public record class GetContractsByVisibilityQuery: IRequest<Result<List<ContractDto>>>
{
    public ContractEnums.Visibility Visibility { get; set; }

    public GetContractsByVisibilityQuery()
    {

    }

    public GetContractsByVisibilityQuery(ContractEnums.Visibility visibility)
    {
        Visibility = visibility;
    }
}

internal class GetContractsByVisibilityQueryHandler : IRequestHandler<GetContractsByVisibilityQuery, Result<List<ContractDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IContractRepository _contractRepository;

    public GetContractsByVisibilityQueryHandler(IUnitOfWork unitOfWork,
     IMapper mapper, IContractRepository contractRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _contractRepository = contractRepository;
    }

    public async Task<Result<List<ContractDto>>> Handle(GetContractsByVisibilityQuery query, CancellationToken cancellationToken)
    {
        var entity = await _contractRepository.GetContractsByVisibilityAsync(query.Visibility);
        var Contract = _mapper.Map<List<ContractDto>>(entity);
        return await Result<List<ContractDto>>.SuccessAsync(Contract);
    }
}
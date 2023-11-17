using AutoMapper;
using Bondo.Application.DTOs.Contract;
using Bondo.Application.Interfaces.Repositories;
using Bondo.Domain.Entities;
using Bondo.Shared;
using MediatR;

namespace Bondo.Application;
public record GetAllApplicationsByContractIdQuery  : IRequest<Result<ContractApplicationDto>>
{
    public int Id { get; set; }

    public GetAllApplicationsByContractIdQuery()
    {

    }

    public GetAllApplicationsByContractIdQuery(int id)
    {
        Id = id;
    }
}

internal class GetContractByIdQueryHandler : IRequestHandler<GetAllApplicationsByContractIdQuery, Result<ContractApplicationDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetContractByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<ContractApplicationDto>> Handle(GetAllApplicationsByContractIdQuery query, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<ContractApplication>().GetByIdAsync(query.Id);
        var application = _mapper.Map<ContractApplicationDto>(entity);
        return await Result<ContractApplicationDto>.SuccessAsync(application);
    }
}

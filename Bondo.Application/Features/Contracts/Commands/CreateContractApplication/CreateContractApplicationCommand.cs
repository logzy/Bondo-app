using AutoMapper;
using Bondo.Application.Common.Mappings;
using Bondo.Application.Interfaces.Repositories;
using Bondo.Domain.Entities;
using Bondo.Domain.Enums;
using Bondo.Shared;
using MediatR;

namespace Bondo.Application.Features.Contracts.Commands;

public record CreateContractApplicationCommand : IRequest<Result<int>>, IMapFrom<Contract>
{
    public int ContractId { get; set; }
    public string ContractorId { get; set; }
    public string AdditionalNotes { get; set; }
}

internal class CreateContractApplicationCommandHandler : IRequestHandler<CreateContractApplicationCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
 
    public CreateContractApplicationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper; 
    }
 
    public async Task<Result<int>> Handle(CreateContractApplicationCommand command, CancellationToken cancellationToken)
    {
        var contract = new ContractApplication()
        {
            ContractId = command.ContractId,
            ContractorId = command.ContractorId,
            AdditionalNotes = command.AdditionalNotes
        };
 
        await _unitOfWork.Repository<ContractApplication>().AddAsync(contract);
        contract.AddDomainEvent(new ContractApplicationCreatedEvent(contract));
        await _unitOfWork.Save(cancellationToken);
        return await Result<int>.SuccessAsync(contract.Id, "Contract Application Created.");
    }
}
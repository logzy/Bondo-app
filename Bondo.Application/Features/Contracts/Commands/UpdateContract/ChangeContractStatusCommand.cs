using AutoMapper;
using Bondo.Application.Interfaces.Repositories;
using Bondo.Shared;
using MediatR;
using Bondo.Domain.Entities;
using Bondo.Domain.Enums;
using Bondo.Application.DTOs.Contract;

namespace Bondo.Application.Features.Contracts.Commands;
public record ChangeContractStatusCommand : IRequest<Result<ContractDto>>
{
    public int Id { get; set; }
    // public string OwnerUserId { get; set; }
    // public string? ContractorUserId { get; set; }
    public ContractEnums.ContractStatus Status { get; set; }
}

internal class ChangeContractStatusCommandHandler : IRequestHandler<ChangeContractStatusCommand, Result<ContractDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ChangeContractStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<ContractDto>> Handle(ChangeContractStatusCommand command, CancellationToken cancellationToken)
    {
        var contract = await _unitOfWork.Repository<Contract>().GetByIdAsync(command.Id);
        if (contract != null)
        {
            // Contract.ContractorUserId = command.ContractorUserId;
            contract.Status = command.Status;
            await _unitOfWork.Repository<Contract>().UpdateAsync(contract);
            contract.AddDomainEvent(new ContractUpdatedEvent(contract));

            await _unitOfWork.Save(cancellationToken);
            var contractDto = _mapper.Map<ContractDto>(contract);
            return await Result<ContractDto>.SuccessAsync(contractDto, "Contract status Updated.");
        }
        else
        {
            return await Result<ContractDto>.FailureAsync("Contract Not Found.");
        }
    }
}
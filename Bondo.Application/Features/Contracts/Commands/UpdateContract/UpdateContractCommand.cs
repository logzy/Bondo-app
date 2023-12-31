﻿using AutoMapper;
using Bondo.Application.Interfaces.Repositories;
using Bondo.Shared;
using MediatR;
using Bondo.Domain.Entities;
using Bondo.Domain.Enums;
using Bondo.Application.DTOs.Contract;

namespace Bondo.Application.Features.Contracts.Commands;
public record UpdateContractCommand : IRequest<Result<ContractDto>>
{
    public int Id { get; set; }
    // public string OwnerUserId { get; set; }
    // public string? ContractorUserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal ContractValue { get; set; }
    public DateTime DeliveryDate { get; set; }
    public ContractEnums.Visibility Visibility { get; set; }
    public ContractEnums.DisbursementOption DisbursementOption { get; set; }
    public ContractEnums.ContractStatus Status { get; set; }
    public string PaymentTerms { get; set; }
    public string AgreementUrl { get; set; }
}

internal class UpdateContractCommandHandler : IRequestHandler<UpdateContractCommand, Result<ContractDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateContractCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<ContractDto>> Handle(UpdateContractCommand command, CancellationToken cancellationToken)
    {
        var contract = await _unitOfWork.Repository<Contract>().GetByIdAsync(command.Id);
        if (contract != null)
        {
            // Contract.ContractorUserId = command.ContractorUserId;
            contract.Title = command.Title;
            contract.Description = command.Description;
            contract.Visibility = command.Visibility;
            contract.DisbursementOption = command.DisbursementOption;
            contract.Status = command.Status;
            contract.PaymentTerms = command.PaymentTerms;
            contract.AgreementUrl = command.AgreementUrl;

            await _unitOfWork.Repository<Contract>().UpdateAsync(contract);
            contract.AddDomainEvent(new ContractUpdatedEvent(contract));

            await _unitOfWork.Save(cancellationToken);
            var contractDto = _mapper.Map<ContractDto>(contract);
            return await Result<ContractDto>.SuccessAsync(contractDto, "Contract Updated.");
        }
        else
        {
            return await Result<ContractDto>.FailureAsync("Contract Not Found.");
        }
    }
}
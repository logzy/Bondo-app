using AutoMapper;
using Bondo.Application.Common.Mappings;
using Bondo.Application.Interfaces.Repositories;
using Bondo.Domain.Entities;
using Bondo.Domain.Enums;
using Bondo.Shared;
using MediatR;

namespace Bondo.Application.Features.Contracts.Commands;
public record CreateContractCommand : IRequest<Result<int>>, IMapFrom<Contract>
{
    public string OwnerUserId { get; set; }
    public string? ContractorUserId { get; set; }
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

internal class CreateContractCommandHandler : IRequestHandler<CreateContractCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
 
    public CreateContractCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper; 
    }
 
    public async Task<Result<int>> Handle(CreateContractCommand command, CancellationToken cancellationToken)
    {
        var contract = new Contract()
        {
            OwnerUserId = command.OwnerUserId,
            ContractorUserId = command.ContractorUserId,
            Title = command.Title,
            DeliveryDate = command.DeliveryDate,
            Description = command.Description,
            Visibility = command.Visibility,
            DisbursementOption = command.DisbursementOption,
            Status = command.Status,
            PaymentTerms = command.PaymentTerms,
            AgreementUrl = command.AgreementUrl
        };
 
        await _unitOfWork.Repository<Contract>().AddAsync(contract);
        contract.AddDomainEvent(new ContractCreatedEvent(contract));
        await _unitOfWork.Save(cancellationToken);
        return await Result<int>.SuccessAsync(contract.Id, "Contract Created.");
    }
}
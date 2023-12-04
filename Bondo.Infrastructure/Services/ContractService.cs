using Bondo.Application;
using Bondo.Application.DTOs.Contract;
using Bondo.Application.Features.Contracts.Commands;
using Bondo.Application.Features.Contracts.Queries;
using Bondo.Application.Interfaces;
using Bondo.Application.Models.Contract;
using Bondo.Domain.Enums;
using Bondo.Shared;
using MediatR;

namespace Bondo.Infrastructure.Services;
public class ContractService : IContractService
{
    private readonly IMediator _mediator;
    public ContractService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<Result<int>> ApplyToContract(ContractApplicationRequestModel requestModel, string userId)
    {
        CreateContractApplicationCommand command = new CreateContractApplicationCommand{
            ContractId = requestModel.ContractId,
            ContractorId = userId,
            AdditionalNotes = requestModel.AdditionalNotes
        };
        var result = await _mediator.Send(command);
        return result;
    }

    public async Task<Result<ContractDto>> ChangeStatus(int contractId, ContractEnums.ContractStatus status)
    {
        ChangeContractStatusCommand command = new ChangeContractStatusCommand{
            Id = contractId,
            Status = status
        };
        var result = await _mediator.Send(command);
        return result;
    }

    public async Task<Result<int>> CreateContract(CreateContractRequestModel requestModel, string userId)
    {
        CreateContractCommand command = new CreateContractCommand{
            OwnerUserId = userId,
            ContractorUserId = requestModel.ContractorUserId,
            Title = requestModel.Title,
            Description = requestModel.Description,
            ContractValue = requestModel.ContractValue,
            DeliveryDate = requestModel.DeliveryDate,
            Visibility = requestModel.ContractorUserId == null ? ContractEnums.Visibility.Public : ContractEnums.Visibility.Private,
            DisbursementOption = requestModel.DisbursementOption,
            Status = ContractEnums.ContractStatus.InActive,
            PaymentTerms = requestModel.PaymentTerms,
            AgreementUrl = requestModel.AgreementUrl

        };
        var result = await _mediator.Send(command);
        // perform other functions from here... etc notification
        return result;
    }

    public Task<Result<List<ContractApplicationDto>>> GetContractApplicationByContract(int contractId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<ContractApplicationDto>>> GetContractApplicationById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<ContractApplicationDto>>> GetContractApplicationByUser(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<ContractApplicationDto>>> GetContractApplications()
    {
        throw new NotImplementedException();
    }

    public  async Task<Result<ContractDto>> GetContractById(int id)
    {
        var result = await _mediator.Send(new GetContractByIdQuery(id));
        return result;
    }

    public async Task<Result<List<ContractDto>>> GetContracts()
    {
        var result = await _mediator.Send(new GetAllContractsQuery());
        return result;
    }

    public async Task<Result<List<ContractDto>>> GetContractsByContractor(string contractorId)
    {
        var result = await _mediator.Send(new GetContractsByContractorQuery(contractorId));
        return result;
    }

    public async Task<Result<List<ContractDto>>> GetContractsByOwner(string ownerId)
    {
        var result = await _mediator.Send(new GetContractsByOwnerQuery(ownerId));
        return result;
    }

    public async Task<Result<ContractDto>> UpdateContract(UpdateContractRequestModel requestModel, string userId)
    {
       UpdateContractCommand command = new UpdateContractCommand{
            Id = requestModel.Id,
            Title = requestModel.Title,
            Description = requestModel.Description,
            ContractValue = requestModel.ContractValue,
            DeliveryDate = requestModel.DeliveryDate,
            DisbursementOption = requestModel.DisbursementOption,
            Status = ContractEnums.ContractStatus.InActive,
            PaymentTerms = requestModel.PaymentTerms,
            AgreementUrl = requestModel.AgreementUrl

        };
        var result = await _mediator.Send(command);
        return result;
    }
}

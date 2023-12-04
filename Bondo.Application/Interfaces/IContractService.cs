using Bondo.Application.DTOs.Contract;
using Bondo.Application.Models.Contract;
using Bondo.Domain.Enums;
using Bondo.Shared;

namespace Bondo.Application.Interfaces;
public interface IContractService
{
    public Task<Result<List<ContractDto>>> GetContracts();
    public Task<Result<List<ContractDto>>> GetContractsByOwner(string ownerId);
    public Task<Result<List<ContractDto>>> GetContractsByContractor(string contractorId);
    public Task<Result<int>> CreateContract(CreateContractRequestModel requestModel, string userId);
    public Task<Result<ContractDto>> UpdateContract(UpdateContractRequestModel requestModel, string userId);
    public Task<Result<ContractDto>> GetContractById(int id);
    public Task<Result<ContractDto>> ChangeStatus(int contractId, ContractEnums.ContractStatus status);
    public Task<Result<int>> ApplyToContract(ContractApplicationRequestModel requestModel, string userId);

    public Task<Result<List<ContractApplicationDto>>> GetContractApplications();
    public Task<Result<List<ContractApplicationDto>>> GetContractApplicationById(int id);
    public Task<Result<List<ContractApplicationDto>>> GetContractApplicationByUser(string userId);
    public Task<Result<List<ContractApplicationDto>>> GetContractApplicationByContract(int contractId);

}

using Bondo.Domain.Entities;
using Bondo.Domain.Enums;

namespace Bondo.Application.Interfaces.Repositories;
public interface IContractRepository
{
    Task<List<Contract>> GetContractsByVisibilityAsync(ContractEnums.Visibility visibility);
    Task<List<Contract>> GetContractsByStatusAsync(ContractEnums.ContractStatus status);
    Task<List<Contract>> GetContractsByOwnerAsync(string ownerId);
    Task<List<Contract>> GetContractsByContractorAsync(string contractorId);
}

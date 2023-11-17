using Bondo.Domain.Entities;
using Bondo.Domain.Enums;

namespace Bondo.Application.Interfaces.Repositories;
public interface IContractApplicationRepository
{
    Task<List<ContractApplication>> GetApplicationsByContractorAsync(string contractorId);
    Task<List<ContractApplication>> GetApplicationsByStatusAsync(ContractEnums.ApplicationStatus status);

}

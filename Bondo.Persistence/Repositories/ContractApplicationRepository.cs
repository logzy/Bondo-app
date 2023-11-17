using Bondo.Application.Interfaces;
using Bondo.Application.Interfaces.Repositories;
using Bondo.Domain.Entities;
using Bondo.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Bondo.Persistence.Repositories;
public class ContractApplicationRepository : IContractApplicationRepository
{
    private readonly IGenericRepository<ContractApplication> _repository;

    public ContractApplicationRepository(IGenericRepository<ContractApplication> repository) 
    {
        _repository = repository;
    }
    public async Task<List<ContractApplication>> GetApplicationsByContractorAsync(string contractorId)
    {
        return await _repository.Entities.Where(x => x.ContractorId == contractorId).ToListAsync();
    }

    public async Task<List<ContractApplication>> GetApplicationsByStatusAsync(ContractEnums.ApplicationStatus status)
    {
        return await _repository.Entities.Where(x => x.Status == status).ToListAsync();
    }
}

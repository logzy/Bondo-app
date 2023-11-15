using Bondo.Application.Interfaces;
using Bondo.Application.Interfaces.Repositories;
using Bondo.Domain.Entities;
using Bondo.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Bondo.Persistence;
public class ContractRepository : IContractRepository
{
    private readonly IGenericRepository<Contract> _repository;

    public ContractRepository(IGenericRepository<Contract> repository) 
    {
        _repository = repository;
    }

    public async Task<List<Contract>> GetContractsByContractorAsync(string contractorId)
    {
        return await _repository.Entities.Where(x => x.ContractorUserId == contractorId).ToListAsync();
    }

    public async Task<List<Contract>> GetContractsByOwnerAsync(string ownerId)
    {
        return await _repository.Entities.Where(x => x.OwnerUserId == ownerId).ToListAsync();
    }

    public async Task<List<Contract>> GetContractsByStatusAsync(ContractEnums.ContractStatus status)
    {
        return await _repository.Entities.Where(x => x.Status == status).ToListAsync();
    }

    public async Task<List<Contract>> GetContractsByVisibilityAsync(ContractEnums.Visibility visibility)
    {
        return await _repository.Entities.Where(x => x.Visibility == visibility).ToListAsync();
    }

}

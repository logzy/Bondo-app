using Bondo.Domain.Entities;

namespace Bondo.Application.Interfaces.Repositories;
public interface IUserRepository
{
        Task<List<ApplicationUser>> GetAll();
}

using Bondo.Application.Interfaces.Repositories;
using Bondo.Domain.Entities;
using Bondo.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Bondo.Persistence;
public class UserRepository : IUserRepository
{
        protected readonly AppSqlDbContext _context;

        public UserRepository(AppSqlDbContext context)
        {
            _context = context;
        }
        public async Task<List<ApplicationUser>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }
    
}

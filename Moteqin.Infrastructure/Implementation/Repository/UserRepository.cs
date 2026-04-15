using Microsoft.EntityFrameworkCore;
using Moteqin.Domain.Entity;
using Moteqin.Infrastructure.Implementation.Persistence.Conetxt;

namespace Moteqin.Infrastructure.Implementation.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MoteqinDbContext _context;

        public UserRepository(MoteqinDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
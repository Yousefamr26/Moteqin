using Microsoft.EntityFrameworkCore;
using Moteqin.Domain.Entity;
using Moteqin.Infrastructure.Implementation.Persistence.Conetxt;

namespace Moteqin.Infrastructure.Implementation.Repository
{
    public class StreakRepository : Repository<Streak>, IStreakRepository
    {
        public StreakRepository(MoteqinDbContext context) : base(context)
        {
        }

        public async Task<Streak> GetUserStreakAsync(string userId)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task UpdateStreakAsync(Streak streak)
        {
            _dbSet.Update(streak);
        }
    }
}
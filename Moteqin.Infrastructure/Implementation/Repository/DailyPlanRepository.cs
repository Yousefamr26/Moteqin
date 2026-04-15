using Microsoft.EntityFrameworkCore;
using Moteqin.Domain.Entity;
using Moteqin.Infrastructure.Implementation.Persistence.Conetxt;

namespace Moteqin.Infrastructure.Implementation.Repository
{
    public class DailyPlanRepository : Repository<DailyPlan>, IDailyPlanRepository
    {
        public DailyPlanRepository(MoteqinDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<DailyPlan>> GetUserPlansAsync(string userId)
        {
            return await _dbSet.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<DailyPlan> GetTodayPlanAsync(string userId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Date.Date == DateTime.UtcNow.Date);
        }

        public async Task AddPlanAsync(DailyPlan plan)
        {
            await _dbSet.AddAsync(plan);
        }
    }
}
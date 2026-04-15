using Microsoft.EntityFrameworkCore;
using Moteqin.Domain.Entity;
using Moteqin.Infrastructure.Implementation.Persistence.Conetxt;

namespace Moteqin.Infrastructure.Implementation.Repository
{
    public class PointRepository : Repository<Point>, IPointRepository
    {
        public PointRepository(MoteqinDbContext context) : base(context)
        {
        }

        public async Task<int> GetUserPointsAsync(string userId)
        {
            return await _dbSet
                .Where(x => x.UserId == userId)
                .SumAsync(x => x.Value);
        }

        public async Task AddPointsAsync(Point point)
        {
            await _dbSet.AddAsync(point);
        }
        public async Task<List<LeaderboardDto>> GetLeaderboardAsync(int take)
        {
            return await _dbSet
                .GroupBy(x => x.UserId)
                .Select(g => new LeaderboardDto
                {
                    UserId = g.Key,
                    Points = g.Sum(x => x.Value)
                })
                .OrderByDescending(x => x.Points)
                .Take(take)
                .ToListAsync();
        }
    }
}
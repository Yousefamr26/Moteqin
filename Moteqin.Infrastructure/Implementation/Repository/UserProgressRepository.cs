using Microsoft.EntityFrameworkCore;
using Moteqin.Domain.Entity;
using Moteqin.Infrastructure.Implementation.Persistence.Conetxt;

namespace Moteqin.Infrastructure.Implementation.Repository
{
    public class UserProgressRepository : Repository<UserProgress>, IUserProgressRepository
    {
        public UserProgressRepository(MoteqinDbContext context) : base(context)
        {
        }

        public async Task<UserProgress> GetUserAyahProgressAsync(string userId, int ayahId)
        {
            return await _dbSet
                .Include(x => x.Ayah)
                .ThenInclude(a => a.Surah)
                .FirstOrDefaultAsync(x => x.UserId == userId && x.AyahId == ayahId);
        }

        public async Task<IEnumerable<UserProgress>> GetUserProgressAsync(string userId)
        {
            return await _dbSet
                .Include(x => x.Ayah)
                .ThenInclude(a => a.Surah)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserProgress>> GetUserProgressBySurahAsync(string userId, int surahId)
        {
            return await _dbSet
                .Include(x => x.Ayah)
                .ThenInclude(a => a.Surah)
                .Where(x => x.UserId == userId && x.Ayah.SurahId == surahId)
                .ToListAsync();
        }

        public async Task<int> CountCompletedAsync(string userId)
        {
            return await _dbSet
                .CountAsync(x => x.UserId == userId &&
                                 x.Status == ProgressStatus.Memorized);
        }

        public async Task RemoveRangeAsync(IEnumerable<UserProgress> progresses)
        {
            _dbSet.RemoveRange(progresses);
            await Task.CompletedTask;
        }

        public async Task UpdateProgressAsync(UserProgress progress)
        {
            _dbSet.Update(progress);
            await Task.CompletedTask;
        }
        public async Task<List<UserProgress>> GetByUserAndAyahRangeAsync(
       string userId,
       int fromAyahId,
       int toAyahId)
        {
            return await _context.UserProgresses
                .Where(x =>
                    x.UserId == userId &&
                    x.AyahId >= fromAyahId &&
                    x.AyahId <= toAyahId)
                .ToListAsync();
        }
    }
}
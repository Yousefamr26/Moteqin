using Microsoft.EntityFrameworkCore;
using Moteqin.Domain.Entity;
using Moteqin.Infrastructure.Implementation.Persistence.Conetxt;

namespace Moteqin.Infrastructure.Implementation.Repository
{
    public class RecordingRepository : Repository<Recording>, IRecordingRepository
    {
        public RecordingRepository(MoteqinDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Recording>> GetUserRecordingsAsync(string userId)
        {
            return await _dbSet.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<Recording> GetRecordingWithFeedbackAsync(int id)
        {
            return await _dbSet
                .Include(r => r.Feedbacks)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteRecordingAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }
    }
}
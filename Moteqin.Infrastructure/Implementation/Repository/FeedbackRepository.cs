using Microsoft.EntityFrameworkCore;
using Moteqin.Domain.Entity;
using Moteqin.Infrastructure.Implementation.Persistence.Conetxt;

namespace Moteqin.Infrastructure.Implementation.Repository
{
    public class FeedbackRepository : Repository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(MoteqinDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Feedback>> GetByRecordingIdAsync(int recordingId)
        {
            return await _dbSet.Where(x => x.RecordingId == recordingId).ToListAsync();
        }

        public async Task AddFeedbackAsync(Feedback feedback)
        {
            await _dbSet.AddAsync(feedback);
        }
        public async Task<List<Feedback>> GetAllWithRecordingAsync()
        {
            return await _dbSet
                .Include(x => x.Recording)
                .ToListAsync();
        }
    }
}
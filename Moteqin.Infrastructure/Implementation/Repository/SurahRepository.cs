using Microsoft.EntityFrameworkCore;
using Moteqin.Domain.Entity;
using Moteqin.Infrastructure.Implementation.Persistence.Conetxt;

namespace Moteqin.Infrastructure.Implementation.Repository
{
    public class SurahRepository : Repository<Surah>, ISurahRepository
    {
        public SurahRepository(MoteqinDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Surah>> GetAllSurahsAsync()
        {
            return await _dbSet
                .Include(s => s.Ayahs)
                .ToListAsync();
        }

        public async Task<Surah> GetSurahWithAyahsAsync(int id)
        {
            return await _dbSet
                .Include(s => s.Ayahs)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
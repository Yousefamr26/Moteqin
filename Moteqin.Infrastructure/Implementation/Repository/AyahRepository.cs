using Microsoft.EntityFrameworkCore;
using Moteqin.Domain.Entity;
using Moteqin.Infrastructure.Implementation.Persistence.Conetxt;

namespace Moteqin.Infrastructure.Implementation.Repository
{
    public class AyahRepository : Repository<Ayah>, IAyahRepository
    {
        public AyahRepository(MoteqinDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Ayah>> GetBySurahIdAsync(int surahId)
        {
            return await _dbSet.Where(x => x.SurahId == surahId).ToListAsync();
        }
    }
}
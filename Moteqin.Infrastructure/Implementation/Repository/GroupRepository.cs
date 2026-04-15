using Microsoft.EntityFrameworkCore;
using Moteqin.Domain.Entity;
using Moteqin.Infrastructure.Implementation.Persistence.Conetxt;

namespace Moteqin.Infrastructure.Implementation.Repository
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        public GroupRepository(MoteqinDbContext context) : base(context)
        {
        }

        public async Task<Group> GetGroupWithMembersAsync(int groupId)
        {
            return await _dbSet
                .Include(g => g.Members)
                .FirstOrDefaultAsync(x => x.Id == groupId);
        }

        public async Task<IEnumerable<Group>> GetUserGroupsAsync(string userId)
        {
            return await _dbSet
                .Include(g => g.Members)
                .Where(g => g.Members.Any(m => m.UserId == userId))
                .ToListAsync();
        }

        public async Task AddGroupAsync(Group group)
        {
            await _dbSet.AddAsync(group);
        }
    }
}
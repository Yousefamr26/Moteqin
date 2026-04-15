using Microsoft.EntityFrameworkCore;
using Moteqin.Domain.Entity;
using Moteqin.Infrastructure.Implementation.Persistence.Conetxt;

namespace Moteqin.Infrastructure.Implementation.Repository
{
    public class GroupMemberRepository : Repository<GroupMember>, IGroupMemberRepository
    {
        public GroupMemberRepository(MoteqinDbContext context) : base(context)
        {
        }

        public async Task AddMemberAsync(GroupMember member)
        {
            await _dbSet.AddAsync(member);
        }

        public async Task RemoveMemberAsync(string userId, int groupId)
        {
            var entity = await _dbSet
                .FirstOrDefaultAsync(x => x.UserId == userId && x.GroupId == groupId);

            if (entity != null)
                _dbSet.Remove(entity);
        }
        public IQueryable<GroupMember> GetQueryable()
        {
            return _dbSet.AsQueryable();
        }
    }
}
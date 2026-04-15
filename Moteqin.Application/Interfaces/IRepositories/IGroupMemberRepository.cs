public interface IGroupMemberRepository:IRepository<GroupMember>
{
    IQueryable<GroupMember> GetQueryable();
    Task AddMemberAsync(GroupMember member);
    Task RemoveMemberAsync(string userId, int groupId);
}
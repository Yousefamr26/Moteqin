public interface IGroupRepository:IRepository<Group>
{
    Task<Group> GetGroupWithMembersAsync(int groupId);
    Task<IEnumerable<Group>> GetUserGroupsAsync(string userId);
}

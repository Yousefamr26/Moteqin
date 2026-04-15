using Moteqin.Domain.Common;

public class GroupMember : BaseEntity
{
    public int GroupId { get; set; }
    public string UserId { get; set; }

    public GroupRole Role { get; set; }

    public Group Group { get; set; }
    public ApplicationUser User { get; set; }
}
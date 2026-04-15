using Moteqin.Domain.Common;

public class Group : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public string CreatedByUserId { get; set; }

    public ApplicationUser CreatedByUser { get; set; }

    public ICollection<GroupMember> Members { get; set; }
}
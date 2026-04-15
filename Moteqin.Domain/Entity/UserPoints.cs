using Moteqin.Domain.Common;

public class UserPoints : BaseEntity
{
    public string UserId { get; set; }

    public int TotalPoints { get; set; }

    public ApplicationUser User { get; set; }
}
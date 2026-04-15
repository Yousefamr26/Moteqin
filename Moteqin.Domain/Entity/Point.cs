using Moteqin.Domain.Common;

public class Point : BaseEntity
{
    public string UserId { get; set; }
    public int Value { get; set; }

    public string Reason { get; set; }

    public ApplicationUser User { get; set; }
}
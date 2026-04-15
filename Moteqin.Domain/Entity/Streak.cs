using Moteqin.Domain.Common;

public class Streak : BaseEntity
{
    public string UserId { get; set; }

    public int CurrentStreak { get; set; }
    public int LongestStreak { get; set; }

    public DateTime LastActiveDate { get; set; }

    public ApplicationUser User { get; set; }
}
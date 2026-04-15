using Microsoft.AspNetCore.Identity;
using Moteqin.Domain.Entity;

public class ApplicationUser : IdentityUser
{
    public UserLevel Level { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;

    public ICollection<UserProgress> Progresses { get; set; }
    public ICollection<Recording> Recordings { get; set; }
    public ICollection<Feedback> Feedbacks { get; set; }
    public ICollection<GroupMember> GroupMembers { get; set; }
    public ICollection<Point> Points { get; set; }
    public Streak Streak { get; set; }
    public ICollection<Notification> Notifications { get; set; }
}
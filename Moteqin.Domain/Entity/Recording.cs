using Moteqin.Domain.Common;

public class Recording : BaseEntity
{
    public string UserId { get; set; }

    public int AyahIdFrom { get; set; }
    public int AyahIdTo { get; set; }

    public string FileUrl { get; set; }
    public int Duration { get; set; }

    public RecordingStatus Status { get; set; }

    public ApplicationUser User { get; set; }
    public ICollection<Feedback> Feedbacks { get; set; }
}
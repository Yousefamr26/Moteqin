using Moteqin.Domain.Common;

public class Feedback : BaseEntity
{
    public int RecordingId { get; set; }

    public string SheikhId { get; set; } = default!;
    public string CommentText { get; set; } = default!;

    public string? AudioUrl { get; set; }  

    public FeedbackStatus Status { get; set; }

    public Recording Recording { get; set; }
    public ApplicationUser Sheikh { get; set; }
}
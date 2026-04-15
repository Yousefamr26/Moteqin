using Moteqin.Domain.Common;

public class DailyPlan : BaseEntity
{
    public string UserId { get; set; }

    public int AyahIdFrom { get; set; }
    public int AyahIdTo { get; set; }

    public DateTime Date { get; set; }

    public PlanType Type { get; set; }

    public bool IsCompleted { get; set; }

    public ApplicationUser User { get; set; }
}
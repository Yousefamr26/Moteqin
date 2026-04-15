public class DailyPlanDto
{
    public int Id { get; set; }

    public int From { get; set; }
    public int To { get; set; }

    public string Type { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime Date { get; set; }
}
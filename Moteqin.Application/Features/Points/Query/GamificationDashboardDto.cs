public class GamificationDashboardDto
{
    public int TotalPoints { get; set; }
    public int CurrentStreak { get; set; }
    public int LongestStreak { get; set; }
    public int Rank { get; set; }
    public bool CanEarnToday { get; set; }
}
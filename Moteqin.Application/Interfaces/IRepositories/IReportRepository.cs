public interface IReportRepository
{
    Task<object> GetWeeklyReportAsync(string userId);
    Task<object> GetMonthlyReportAsync(string userId);
}
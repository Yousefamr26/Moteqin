using Moteqin.Infrastructure.Implementation.Persistence.Conetxt;

public class ReportRepository : IReportRepository
{
    private readonly MoteqinDbContext _context;

    public ReportRepository(MoteqinDbContext context)
    {
        _context = context;
    }

    public async Task<object> GetWeeklyReportAsync(string userId)
    {
        return await Task.FromResult(new { });
    }

    public async Task<object> GetMonthlyReportAsync(string userId)
    {
        return await Task.FromResult(new { });
    }
}
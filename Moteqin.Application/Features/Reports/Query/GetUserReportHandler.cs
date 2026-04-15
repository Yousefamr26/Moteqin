using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Moteqin.Domain.Common;
using Moteqin.Domain.Interfaces;

public class GetUserReportHandler : IRequestHandler<GetUserReportQuery, Result<ReportDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;

    public GetUserReportHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
    {
        _unitOfWork = unitOfWork;
        _httpContext = httpContext;
    }

    public async Task<Result<ReportDto>> Handle(GetUserReportQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContext.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Result<ReportDto>.Failure("User not found");

        var progress = await _unitOfWork.UserProgresses
            .GetUserProgressAsync(userId);

        var now = DateTime.UtcNow;

        var weekStart = now.AddDays(-7);
        var monthStart = now.AddMonths(-1);

        var weekly = progress
            .Where(x => x.MemorizedAt != null && x.MemorizedAt >= weekStart)
            .ToList();

        var monthly = progress
            .Where(x => x.MemorizedAt != null && x.MemorizedAt >= monthStart)
            .ToList();

        var totalMemorized = progress
            .Count(x => x.Status == ProgressStatus.Memorized);

        var activeDays = progress
            .Where(x => x.MemorizedAt != null)
            .Select(x => x.MemorizedAt.Value.Date)
            .Distinct()
            .Count();

        var weeklyCount = weekly
            .Count(x => x.Status == ProgressStatus.Memorized);

        var monthlyCount = monthly
            .Count(x => x.Status == ProgressStatus.Memorized);

        var result = new ReportDto
        {
            TotalMemorized = totalMemorized,
            ActiveDays = activeDays,
            WeeklyProgress = weeklyCount,
            MonthlyProgress = monthlyCount
        };

        return Result<ReportDto>.Success(result);
    }
}
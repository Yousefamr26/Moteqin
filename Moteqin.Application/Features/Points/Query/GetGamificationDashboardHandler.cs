using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Moteqin.Domain.Common;
using Moteqin.Domain.Interfaces;

public class GetGamificationDashboardHandler : IRequestHandler<GetGamificationDashboardQuery, Result<GamificationDashboardDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;

    public GetGamificationDashboardHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
    {
        _unitOfWork = unitOfWork;
        _httpContext = httpContext;
    }

    public async Task<Result<GamificationDashboardDto>> Handle(GetGamificationDashboardQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContext.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Result<GamificationDashboardDto>.Failure("User not found");

        var totalPoints = await _unitOfWork.Points.GetUserPointsAsync(userId);

        var streak = await _unitOfWork.Streaks.GetUserStreakAsync(userId);

        var leaderboard = await _unitOfWork.Points.GetLeaderboardAsync(int.MaxValue);

        var rank = leaderboard
            .Select((x, index) => new { x.UserId, Rank = index + 1 })
            .FirstOrDefault(x => x.UserId == userId)?.Rank ?? 0;

        var today = DateTime.UtcNow.Date;

        var canEarn = !await _unitOfWork.Points
            .AnyAsync(x => x.UserId == userId && x.CreatedAt.Date == today);

        return Result<GamificationDashboardDto>.Success(new GamificationDashboardDto
        {
            TotalPoints = totalPoints,
            CurrentStreak = streak?.CurrentStreak ?? 0,
            LongestStreak = streak?.LongestStreak ?? 0,
            Rank = rank,
            CanEarnToday = canEarn
        });
    }
}
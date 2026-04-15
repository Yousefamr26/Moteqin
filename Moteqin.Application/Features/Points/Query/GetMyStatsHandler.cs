using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Moteqin.Domain.Common;
using Moteqin.Domain.Interfaces;

public class GetMyStatsHandler : IRequestHandler<GetMyStatsQuery, Result<MyStatsDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;

    public GetMyStatsHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
    {
        _unitOfWork = unitOfWork;
        _httpContext = httpContext;
    }

    public async Task<Result<MyStatsDto>> Handle(GetMyStatsQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContext.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Result<MyStatsDto>.Failure("User not found");

        var totalPoints = await _unitOfWork.Points
            .GetUserPointsAsync(userId);

        var streak = await _unitOfWork.Streaks
            .GetUserStreakAsync(userId);

        var result = new MyStatsDto
        {
            TotalPoints = totalPoints,
            CurrentStreak = streak?.CurrentStreak ?? 0,
            LongestStreak = streak?.LongestStreak ?? 0
        };

        return Result<MyStatsDto>.Success(result);
    }
}
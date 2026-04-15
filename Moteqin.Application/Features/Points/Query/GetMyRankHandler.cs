using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Moteqin.Domain.Common;
using Moteqin.Domain.Interfaces;

public class GetMyRankHandler : IRequestHandler<GetMyRankQuery, Result<MyRankDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;

    public GetMyRankHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
    {
        _unitOfWork = unitOfWork;
        _httpContext = httpContext;
    }

    public async Task<Result<MyRankDto>> Handle(GetMyRankQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContext.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Result<MyRankDto>.Failure("User not found");

        var leaderboard = await _unitOfWork.Points.GetLeaderboardAsync(int.MaxValue);

        var ranked = leaderboard
            .Select((x, index) => new { x.UserId, x.Points, Rank = index + 1 })
            .ToList();

        var my = ranked.FirstOrDefault(x => x.UserId == userId);

        return Result<MyRankDto>.Success(new MyRankDto
        {
            UserId = userId,
            Rank = my?.Rank ?? 0,
            TotalUsers = ranked.Count,
            Points = my?.Points ?? 0
        });
    }
}
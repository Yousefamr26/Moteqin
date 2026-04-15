using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Moteqin.Domain.Common;
using Moteqin.Domain.Entity;
using Moteqin.Domain.Interfaces;

public class AddUserActivityHandler : IRequestHandler<AddUserActivityCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;

    public AddUserActivityHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
    {
        _unitOfWork = unitOfWork;
        _httpContext = httpContext;
    }

    public async Task<Result<bool>> Handle(AddUserActivityCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContext.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Result<bool>.Failure("User not found");

        var today = DateTime.UtcNow.Date;

        await _unitOfWork.Points.AddPointsAsync(new Point
        {
            UserId = userId,
            Value = request.Points,
            Reason = request.Reason
        });

        var streak = await _unitOfWork.Streaks.GetUserStreakAsync(userId);

        if (streak == null)
        {
            streak = new Streak
            {
                UserId = userId,
                CurrentStreak = 1,
                LongestStreak = 1,
                LastActiveDate = today
            };

            await _unitOfWork.Streaks.AddAsync(streak); 
        }
        else
        {
            var diff = (today - streak.LastActiveDate.Date).Days;

            if (diff == 1)
            {
                streak.CurrentStreak++;
            }
            else if (diff > 1)
            {
                streak.CurrentStreak = 1;
            }

            streak.LastActiveDate = today;

            if (streak.CurrentStreak > streak.LongestStreak)
                streak.LongestStreak = streak.CurrentStreak;

            await _unitOfWork.Streaks.UpdateStreakAsync(streak);
        }

        await _unitOfWork.SaveChangesAsync();

        return Result<bool>.Success(true);
    }
}
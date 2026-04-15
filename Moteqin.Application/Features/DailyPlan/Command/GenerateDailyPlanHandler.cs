using MediatR;
using Microsoft.AspNetCore.Http;
using Moteqin.Domain.Common;
using Moteqin.Domain.Entity;
using Moteqin.Domain.Interfaces;
using System.Security.Claims;

public class GenerateDailyPlanHandler
    : IRequestHandler<GenerateDailyPlanCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;

    public GenerateDailyPlanHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
    {
        _unitOfWork = unitOfWork;
        _httpContext = httpContext;
    }

    public async Task<Result<string>> Handle(GenerateDailyPlanCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContext.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Result<string>.Failure("Unauthorized");

        var today = DateTime.UtcNow.Date;

        var exist = await _unitOfWork.DailyPlans
            .FindAsync(x => x.UserId == userId && x.Date == today);

        if (exist.Any())
            return Result<string>.Failure("Plan already exists for today");

        int startAyah = 1;
        int endAyah = 1;

       
        if (request.Type == PlanType.Memorize)
        {
            var lastProgress = (await _unitOfWork.UserProgresses
                .FindAsync(x => x.UserId == userId))
                .OrderByDescending(x => x.AyahId)
                .FirstOrDefault();

            startAyah = lastProgress?.AyahId + 1 ?? 1;
            endAyah = startAyah + request.NumberOfAyahs - 1;
        }

      
        else
        {
            var reviewed = (await _unitOfWork.UserProgresses
                .FindAsync(x => x.UserId == userId && x.Status == ProgressStatus.Memorized))
                .Take(request.NumberOfAyahs)
                .ToList();

            if (!reviewed.Any())
                return Result<string>.Failure("No ayahs to review");

            startAyah = reviewed.Min(x => x.AyahId);
            endAyah = reviewed.Max(x => x.AyahId);
        }

        var plan = new DailyPlan
        {
            UserId = userId,
            AyahIdFrom = startAyah,
            AyahIdTo = endAyah,
            Date = today,
            Type = request.Type,
            IsCompleted = false
        };

        await _unitOfWork.DailyPlans.AddAsync(plan);

      
        for (int i = startAyah; i <= endAyah; i++)
        {
            var existProgress = await _unitOfWork.UserProgresses
                .FindAsync(x => x.UserId == userId && x.AyahId == i);

            if (!existProgress.Any())
            {
                await _unitOfWork.UserProgresses.AddAsync(new UserProgress
                {
                    UserId = userId,
                    AyahId = i,
                    Status = ProgressStatus.Memorizing,
                    CreatedAt = DateTime.UtcNow
                });
            }
        }

        await _unitOfWork.SaveChangesAsync();

        return Result<string>.Success("Plan generated successfully");
    }
}
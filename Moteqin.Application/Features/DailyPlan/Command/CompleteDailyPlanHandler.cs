using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Moteqin.Domain.Common;
using Moteqin.Domain.Interfaces;

public class CompleteDailyPlanHandler
    : IRequestHandler<CompleteDailyPlanCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;

    public CompleteDailyPlanHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
    {
        _unitOfWork = unitOfWork;
        _httpContext = httpContext;
    }

    public async Task<Result<string>> Handle(CompleteDailyPlanCommand request, CancellationToken cancellationToken)
    {
        
        var userId = _httpContext.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Result<string>.Failure("Unauthorized");

       
        var plan = await _unitOfWork.DailyPlans.GetByIdAsync(request.PlanId);

        if (plan == null)
            return Result<string>.Failure("Plan not found");

        if (plan.UserId != userId)
            return Result<string>.Failure("You are not allowed");

        if (plan.IsCompleted)
            return Result<string>.Failure("Plan already completed");

        
        plan.IsCompleted = true;
        plan.UpdatedAt = DateTime.UtcNow;

      
        var progresses = await _unitOfWork.UserProgresses
            .FindAsync(x =>
                x.UserId == userId &&
                x.AyahId >= plan.AyahIdFrom &&
                x.AyahId <= plan.AyahIdTo);

        foreach (var p in progresses)
        {
            if (p.Status == ProgressStatus.Memorizing)
            {
                p.Status = ProgressStatus.Memorized;
                p.MemorizedAt = DateTime.UtcNow;
            }
        }

        
        await _unitOfWork.SaveChangesAsync();

        return Result<string>.Success("Plan completed successfully");
    }
}
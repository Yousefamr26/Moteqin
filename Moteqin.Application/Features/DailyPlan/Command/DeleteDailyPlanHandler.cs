using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Moteqin.Domain.Common;
using Moteqin.Domain.Interfaces;

public class DeleteDailyPlanHandler
    : IRequestHandler<DeleteDailyPlanCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;

    public DeleteDailyPlanHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
    {
        _unitOfWork = unitOfWork;
        _httpContext = httpContext;
    }

    public async Task<Result<string>> Handle(DeleteDailyPlanCommand request, CancellationToken cancellationToken)
    {
       
        var userId = _httpContext.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Result<string>.Failure("Unauthorized");

       
        var plan = await _unitOfWork.DailyPlans.GetByIdAsync(request.PlanId);

        if (plan == null)
            return Result<string>.Failure("Plan not found");

        if (plan.UserId != userId)
            return Result<string>.Failure("You are not allowed to delete this plan");

       
        _unitOfWork.DailyPlans.Delete(plan);

      
        var progresses = await _unitOfWork.UserProgresses
            .FindAsync(x =>
                x.UserId == userId &&
                x.AyahId >= plan.AyahIdFrom &&
                x.AyahId <= plan.AyahIdTo);

        foreach (var p in progresses)
        {
            if (p.Status == ProgressStatus.Memorizing)
            {
                p.Status = ProgressStatus.NotStarted;
            }
        }

  
        await _unitOfWork.SaveChangesAsync();

        return Result<string>.Success("Plan deleted successfully");
    }
}
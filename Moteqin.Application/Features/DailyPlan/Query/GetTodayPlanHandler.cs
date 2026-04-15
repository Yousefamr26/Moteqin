using MediatR;
using Microsoft.AspNetCore.Http;
using Moteqin.Domain.Interfaces;
using System.Security.Claims;

public class GetTodayPlanHandler
    : IRequestHandler<GetTodayPlanQuery, Result<List<DailyPlanDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;

    public GetTodayPlanHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
    {
        _unitOfWork = unitOfWork;
        _httpContext = httpContext;
    }

    public async Task<Result<List<DailyPlanDto>>> Handle(GetTodayPlanQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContext.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var today = DateTime.UtcNow.Date;

        var plans = await _unitOfWork.DailyPlans
            .FindAsync(x => x.UserId == userId && x.Date == today);

        var data = plans.Select(x => new DailyPlanDto
        {
            Id = x.Id,
            From = x.AyahIdFrom,
            To = x.AyahIdTo,
            Type = x.Type.ToString(),
            IsCompleted = x.IsCompleted,
            Date = x.Date
        }).ToList();

        return Result<List<DailyPlanDto>>.Success(data);
    }
}
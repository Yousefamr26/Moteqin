using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Moteqin.Domain.Common;
using Moteqin.Domain.Interfaces;

public class GetUserPlansHandler
    : IRequestHandler<GetUserPlansQuery, Result<List<DailyPlanDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;

    public GetUserPlansHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
    {
        _unitOfWork = unitOfWork;
        _httpContext = httpContext;
    }

    public async Task<Result<List<DailyPlanDto>>> Handle(GetUserPlansQuery request, CancellationToken cancellationToken)
    {
       
        var userId = _httpContext.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Result<List<DailyPlanDto>>.Failure("Unauthorized");

        
        var plans = await _unitOfWork.DailyPlans
            .FindAsync(x => x.UserId == userId);

       
        var data = plans
            .OrderByDescending(x => x.Date)
            .Select(x => new DailyPlanDto
            {
                Id = x.Id,
                From = x.AyahIdFrom,
                To = x.AyahIdTo,
                Type = x.Type.ToString(),
                IsCompleted = x.IsCompleted,
                Date = x.Date
            })
            .ToList();

      
        return Result<List<DailyPlanDto>>.Success(data);
    }
}
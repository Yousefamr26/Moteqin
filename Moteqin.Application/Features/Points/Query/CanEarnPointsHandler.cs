using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Moteqin.Domain.Common;
using Moteqin.Domain.Interfaces;

public class CanEarnPointsHandler : IRequestHandler<CanEarnPointsQuery, Result<CanEarnPointsDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;

    public CanEarnPointsHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
    {
        _unitOfWork = unitOfWork;
        _httpContext = httpContext;
    }

    public async Task<Result<CanEarnPointsDto>> Handle(CanEarnPointsQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContext.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Result<CanEarnPointsDto>.Failure("User not found");

        var today = DateTime.UtcNow.Date;

        var alreadyEarned = await _unitOfWork.Points
            .AnyAsync(x => x.UserId == userId && x.CreatedAt.Date == today);

        if (alreadyEarned)
        {
            return Result<CanEarnPointsDto>.Success(new CanEarnPointsDto
            {
                CanEarn = false,
                Reason = "Already earned points today"
            });
        }

        return Result<CanEarnPointsDto>.Success(new CanEarnPointsDto
        {
            CanEarn = true,
            Reason = "Eligible"
        });
    }
}
using MediatR;
using Microsoft.AspNetCore.Http;
using Moteqin.Domain.Interfaces;
using System.Security.Claims;

public class GetProgressPercentageHandler
    : IRequestHandler<GetProgressPercentageQuery, Result<double>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;

    public GetProgressPercentageHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
    {
        _unitOfWork = unitOfWork;
        _httpContext = httpContext;
    }

    public async Task<Result<double>> Handle(GetProgressPercentageQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContext.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Result<double>.Failure("Unauthorized");

        var total = await _unitOfWork.Ayahs.CountAsync();
        var completed = await _unitOfWork.UserProgresses.CountCompletedAsync(userId);

        var percent = total == 0 ? 0 : (completed * 100.0) / total;

        return Result<double>.Success(percent);
    }
}
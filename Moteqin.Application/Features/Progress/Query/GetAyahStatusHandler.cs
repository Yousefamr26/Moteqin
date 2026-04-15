using MediatR;
using Microsoft.AspNetCore.Http;
using Moteqin.Domain.Interfaces;
using System.Security.Claims;

public class GetAyahStatusHandler
    : IRequestHandler<GetAyahStatusQuery, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;

    public GetAyahStatusHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
    {
        _unitOfWork = unitOfWork;
        _httpContext = httpContext;
    }

    public async Task<Result<string>> Handle(GetAyahStatusQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContext.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Result<string>.Failure("Unauthorized");

        var progress = await _unitOfWork.UserProgresses
            .GetUserAyahProgressAsync(userId, request.AyahId);

        if (progress == null)
            return Result<string>.Success("NotStarted");

        return Result<string>.Success(progress.Status.ToString());
    }
}
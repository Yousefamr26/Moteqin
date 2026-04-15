using MediatR;
using Microsoft.AspNetCore.Http;
using Moteqin.Domain.Interfaces;
using System.Security.Claims;

public class ResetSurahProgressHandler
    : IRequestHandler<ResetSurahProgressCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;

    public ResetSurahProgressHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
    {
        _unitOfWork = unitOfWork;
        _httpContext = httpContext;
    }

    public async Task<Result<string>> Handle(ResetSurahProgressCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContext.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Result<string>.Failure("Unauthorized");

        var progress = await _unitOfWork.UserProgresses
            .GetUserProgressBySurahAsync(userId, request.SurahId);

        if (!progress.Any())
            return Result<string>.Failure("No progress found");

        await _unitOfWork.UserProgresses.RemoveRangeAsync(progress);
        await _unitOfWork.SaveChangesAsync();

        return Result<string>.Success("Surah reset successfully");
    }
}
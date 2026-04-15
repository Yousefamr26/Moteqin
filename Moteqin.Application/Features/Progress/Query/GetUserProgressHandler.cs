using MediatR;
using Microsoft.AspNetCore.Http;
using Moteqin.Domain.Interfaces;
using System.Security.Claims;

public class GetUserProgressHandler
    : IRequestHandler<GetUserProgressQuery, Result<List<UserProgressDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;

    public GetUserProgressHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
    {
        _unitOfWork = unitOfWork;
        _httpContext = httpContext;
    }

    public async Task<Result<List<UserProgressDto>>> Handle(GetUserProgressQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContext.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Result<List<UserProgressDto>>.Failure("Unauthorized");

        var progress = await _unitOfWork.UserProgresses
            .GetUserProgressAsync(userId);

        var data = progress.Select(x => new UserProgressDto
        {
            AyahId = x.AyahId,
            SurahName = x.Ayah.Surah.Name,
            AyahNumber = x.Ayah.AyahNumber,
            Status = x.Status.ToString()
        }).ToList();

        return Result<List<UserProgressDto>>.Success(data);
    }
}
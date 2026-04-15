using MediatR;
using Microsoft.AspNetCore.Http;
using Moteqin.Domain.Interfaces;
using System.Security.Claims;

public class GetProgressBySurahHandler
    : IRequestHandler<GetProgressBySurahQuery, Result<List<UserProgressDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;

    public GetProgressBySurahHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
    {
        _unitOfWork = unitOfWork;
        _httpContext = httpContext;
    }

    public async Task<Result<List<UserProgressDto>>> Handle(GetProgressBySurahQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContext.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Result<List<UserProgressDto>>.Failure("Unauthorized");

        var progress = await _unitOfWork.UserProgresses
            .GetUserProgressBySurahAsync(userId, request.SurahId);

        var data = progress.Select(x => new UserProgressDto
        {
            AyahId = x.AyahId,
            AyahNumber = x.Ayah.AyahNumber,
            SurahName = x.Ayah.Surah.Name,
            Status = x.Status.ToString()
        }).ToList();

        return Result<List<UserProgressDto>>.Success(data);
    }
}
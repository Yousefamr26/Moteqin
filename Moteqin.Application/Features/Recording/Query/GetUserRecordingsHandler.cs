using MediatR;
using Microsoft.AspNetCore.Http;
using Moteqin.Domain.Interfaces;
using System.Security.Claims;

public class GetUserRecordingsHandler
    : IRequestHandler<GetUserRecordingsQuery, Result<List<RecordingDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;

    public GetUserRecordingsHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
    {
        _unitOfWork = unitOfWork;
        _httpContext = httpContext;
    }

    public async Task<Result<List<RecordingDto>>> Handle(GetUserRecordingsQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContext.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var recordings = await _unitOfWork.Recordings.GetUserRecordingsAsync(userId);

        var data = recordings.Select(x => new RecordingDto
        {
            Id = x.Id,
            AyahIdFrom = x.AyahIdFrom,
            AyahIdTo = x.AyahIdTo,
            FileUrl = x.FileUrl,
            Duration = x.Duration,
            Status = x.Status.ToString()
        }).ToList();

        return Result<List<RecordingDto>>.Success(data);
    }
}
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Moteqin.Domain.Common;
using Moteqin.Domain.Entity;
using Moteqin.Domain.Interfaces;

public class AddFeedbackHandler : IRequestHandler<AddFeedbackCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;

    public AddFeedbackHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContext)
    {
        _unitOfWork = unitOfWork;
        _httpContext = httpContext;
    }

    public async Task<Result<string>> Handle(AddFeedbackCommand request, CancellationToken cancellationToken)
    {
      
        var sheikhId = _httpContext.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(sheikhId))
            return Result<string>.Failure("Unauthorized");

       
        var recording = await _unitOfWork.Recordings.GetByIdAsync(request.RecordingId);

        if (recording == null)
            return Result<string>.Failure("Recording not found");

        var feedback = new Feedback
        {
            RecordingId = request.RecordingId,
            SheikhId = sheikhId,
            CommentText = request.Comment,
            Status = FeedbackStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Feedbacks.AddAsync(feedback);

   
        var progressList = await _unitOfWork.UserProgresses
            .GetByUserAndAyahRangeAsync(
                recording.UserId,
                recording.AyahIdFrom,
                recording.AyahIdTo);

        if (progressList != null && progressList.Any())
        {
            foreach (var p in progressList)
            {
                p.Status = ProgressStatus.Reviewing;
                p.LastReviewedAt = DateTime.UtcNow;
            }
        }

       
        await _unitOfWork.SaveChangesAsync();

        return Result<string>.Success("Feedback added successfully");
    }
}
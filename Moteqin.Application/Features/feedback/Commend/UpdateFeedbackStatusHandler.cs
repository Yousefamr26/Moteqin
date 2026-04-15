using MediatR;
using Moteqin.Domain.Common;
using Moteqin.Domain.Interfaces;

public class UpdateFeedbackStatusHandler
    : IRequestHandler<UpdateFeedbackStatusCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateFeedbackStatusHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(UpdateFeedbackStatusCommand request, CancellationToken cancellationToken)
    {
       
        var feedback = await _unitOfWork.Feedbacks.GetByIdAsync(request.FeedbackId);

        if (feedback == null)
            return Result<string>.Failure("Feedback not found");

   
        feedback.Status = request.Status;
        feedback.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Feedbacks.Update(feedback);

        
        var recording = await _unitOfWork.Recordings.GetByIdAsync(feedback.RecordingId);

        if (recording != null && request.Status == FeedbackStatus.Approved)
        {
            var progresses = await _unitOfWork.UserProgresses
                .GetByUserAndAyahRangeAsync(
                    recording.UserId,
                    recording.AyahIdFrom,
                    recording.AyahIdTo);

            if (progresses != null)
            {
                foreach (var p in progresses)
                {
                    p.Status = ProgressStatus.Memorized;
                    p.LastReviewedAt = DateTime.UtcNow;
                }
            }
        }

      
        await _unitOfWork.SaveChangesAsync();

        return Result<string>.Success("Feedback status updated successfully");
    }
}
using MediatR;
using Moteqin.Domain.Interfaces;

public class GetFeedbackByRecordingHandler
    : IRequestHandler<GetFeedbackByRecordingQuery, Result<List<FeedbackDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetFeedbackByRecordingHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<FeedbackDto>>> Handle(GetFeedbackByRecordingQuery request, CancellationToken cancellationToken)
    {
        var feedbacks = await _unitOfWork.Feedbacks
            .FindAsync(x => x.RecordingId == request.RecordingId);

        var data = feedbacks.Select(x => new FeedbackDto
        {
            RecordingId = x.RecordingId,
            Comment = x.CommentText,
            Status = x.Status.ToString()
        }).ToList();

        return Result<List<FeedbackDto>>.Success(data);
    }
}
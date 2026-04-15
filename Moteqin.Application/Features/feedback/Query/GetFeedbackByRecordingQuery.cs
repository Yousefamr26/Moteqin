using MediatR;

public record GetFeedbackByRecordingQuery(int RecordingId)
    : IRequest<Result<List<FeedbackDto>>>;
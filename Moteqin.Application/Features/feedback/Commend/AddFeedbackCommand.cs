using MediatR;

public record AddFeedbackCommand(
    int RecordingId,
    string Comment,
    FeedbackStatus Status
) : IRequest<Result<string>>;
using MediatR;

public record DeleteRecordingCommand(int RecordingId)
    : IRequest<Result<string>>;
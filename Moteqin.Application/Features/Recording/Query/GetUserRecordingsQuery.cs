using MediatR;

public record GetUserRecordingsQuery : IRequest<Result<List<RecordingDto>>>;
using MediatR;

public record ResetSurahProgressCommand(int SurahId)
    : IRequest<Result<string>>;
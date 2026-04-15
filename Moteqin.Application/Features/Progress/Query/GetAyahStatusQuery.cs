using MediatR;

public record GetAyahStatusQuery(int AyahId)
    : IRequest<Result<string>>;
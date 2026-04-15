using MediatR;

public class GetUserProgressQuery : IRequest<Result<List<UserProgressDto>>>
{
}
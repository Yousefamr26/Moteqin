using MediatR;

public class GetLeaderboardQuery : IRequest<Result<List<LeaderboardDto>>>
{
    public int Take { get; set; } = 10;
}
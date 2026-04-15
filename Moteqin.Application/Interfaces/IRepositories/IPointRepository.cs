public interface IPointRepository:IRepository<Point>
{
    Task<int> GetUserPointsAsync(string userId);
    Task AddPointsAsync(Point point);
    Task<List<LeaderboardDto>> GetLeaderboardAsync(int take);
}
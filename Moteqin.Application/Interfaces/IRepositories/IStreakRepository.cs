public interface IStreakRepository:IRepository<Streak>
{
    Task<Streak> GetUserStreakAsync(string userId);
    Task UpdateStreakAsync(Streak streak);
}
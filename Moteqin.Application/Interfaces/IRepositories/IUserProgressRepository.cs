using Moteqin.Domain.Entity;

public interface IUserProgressRepository : IRepository<UserProgress>
{
    Task<UserProgress> GetUserAyahProgressAsync(string userId, int ayahId);

    Task<IEnumerable<UserProgress>> GetUserProgressAsync(string userId);

    Task<IEnumerable<UserProgress>> GetUserProgressBySurahAsync(string userId, int surahId);

    Task<int> CountCompletedAsync(string userId);

    Task RemoveRangeAsync(IEnumerable<UserProgress> progresses);

    Task UpdateProgressAsync(UserProgress progress);
    Task<List<UserProgress>> GetByUserAndAyahRangeAsync(
       string userId,
       int fromAyahId,
       int toAyahId);
}
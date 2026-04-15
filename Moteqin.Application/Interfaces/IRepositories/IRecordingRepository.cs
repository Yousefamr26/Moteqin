public interface IRecordingRepository: IRepository<Recording>
{
    Task<IEnumerable<Recording>> GetUserRecordingsAsync(string userId);

    Task<Recording?> GetRecordingWithFeedbackAsync(int id);

    Task DeleteRecordingAsync(int id);
}
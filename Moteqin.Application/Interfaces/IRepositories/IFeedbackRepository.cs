public interface IFeedbackRepository :IRepository<Feedback>
{
    Task<IEnumerable<Feedback>> GetByRecordingIdAsync(int recordingId);

    Task AddFeedbackAsync(Feedback feedback);
    Task<List<Feedback>> GetAllWithRecordingAsync();
}
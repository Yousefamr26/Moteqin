using Moteqin.Domain.Entity;

public interface ISurahRepository
{
    Task<IEnumerable<Surah>> GetAllSurahsAsync();
    Task<Surah> GetSurahWithAyahsAsync(int id);
}
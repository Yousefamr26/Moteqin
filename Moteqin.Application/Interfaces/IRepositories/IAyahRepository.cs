using Moteqin.Domain.Entity;

public interface IAyahRepository: IRepository<Ayah>
{
    Task<IEnumerable<Ayah>> GetBySurahIdAsync(int surahId);
}
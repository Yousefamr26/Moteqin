using Moteqin.Domain.Entity;
using Moteqin.Infrastructure.Implementation.Persistence.Conetxt;

public static class DbInitializer
{
    public static async Task SeedAsync(MoteqinDbContext context)
    {
        if (context.Surahs.Any())
            return;

        var surahs = new List<Surah>
        {
            new Surah
            {
                Name = "الفاتحة",
                NumberOfAyahs = 7,
                Ayahs = new List<Ayah>
                {
                    new Ayah { Text = "بسم الله الرحمن الرحيم" },
                    new Ayah { Text = "الحمد لله رب العالمين" },
                    new Ayah { Text = "الرحمن الرحيم" },
                    new Ayah { Text = "مالك يوم الدين" },
                    new Ayah { Text = "إياك نعبد وإياك نستعين" },
                    new Ayah { Text = "اهدنا الصراط المستقيم" },
                    new Ayah { Text = "صراط الذين أنعمت عليهم غير المغضوب عليهم ولا الضالين" }
                }
            },

            new Surah
            {
                Name = "البقرة",
                NumberOfAyahs = 3,
                Ayahs = new List<Ayah>
                {
                    new Ayah { Text = "الم" },
                    new Ayah { Text = "ذلك الكتاب لا ريب فيه" },
                    new Ayah { Text = "هدى للمتقين" }
                }
            },

            new Surah
            {
                Name = "آل عمران",
                NumberOfAyahs = 2,
                Ayahs = new List<Ayah>
                {
                    new Ayah { Text = "الم" },
                    new Ayah { Text = "الله لا إله إلا هو الحي القيوم" }
                }
            }
        };

        await context.Surahs.AddRangeAsync(surahs);
        await context.SaveChangesAsync();
    }
}
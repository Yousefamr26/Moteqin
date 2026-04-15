using AutoMapper;
using Moteqin.Domain.Entity;

public class QuranProfile : Profile
{
    public QuranProfile()
    {
        CreateMap<Surah, SurahDto>();
        CreateMap<Ayah, AyahDto>();
    }
}
using MediatR;

public class GetProgressBySurahQuery : IRequest<Result<List<UserProgressDto>>>
{
    public int SurahId { get; set; }

    public GetProgressBySurahQuery(int surahId)
    {
        SurahId = surahId;
    }
}
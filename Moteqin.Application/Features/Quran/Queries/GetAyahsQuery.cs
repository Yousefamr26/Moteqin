using MediatR;

public class GetAyahsQuery : IRequest<Result<List<AyahDto>>>
{
    public int SurahId { get; set; }
}
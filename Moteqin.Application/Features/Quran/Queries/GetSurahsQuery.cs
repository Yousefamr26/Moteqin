using MediatR;

public class GetSurahsQuery : IRequest<Result<List<SurahDto>>>
{
}
using MediatR;

public class GetTodayPlanQuery : IRequest<Result<List<DailyPlanDto>>>
{
}
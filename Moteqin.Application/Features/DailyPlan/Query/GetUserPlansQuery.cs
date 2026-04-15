using MediatR;
using Moteqin.Domain.Common;

public class GetUserPlansQuery : IRequest<Result<List<DailyPlanDto>>>
{
}
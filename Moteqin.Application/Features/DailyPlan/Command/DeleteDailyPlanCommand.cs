using MediatR;
using Moteqin.Domain.Common;

public class DeleteDailyPlanCommand : IRequest<Result<string>>
{
    public int PlanId { get; set; }
}
using MediatR;

public class CompleteDailyPlanCommand : IRequest<Result<string>>
{
    public int PlanId { get; set; }
}
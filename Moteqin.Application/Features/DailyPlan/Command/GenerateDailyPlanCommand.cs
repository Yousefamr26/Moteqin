using MediatR;

public class GenerateDailyPlanCommand : IRequest<Result<string>>
{
    public int NumberOfAyahs { get; set; }   
    public PlanType Type { get; set; }       
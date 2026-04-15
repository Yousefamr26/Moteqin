public interface IDailyPlanRepository:IRepository<DailyPlan>
{
    Task<IEnumerable<DailyPlan>> GetUserPlansAsync(string userId);

    Task<DailyPlan> GetTodayPlanAsync(string userId);

    Task AddPlanAsync(DailyPlan plan);
}
public interface IUserRepository
{
    Task<ApplicationUser> GetByIdAsync(string id);
    Task<ApplicationUser> GetByEmailAsync(string email);
}
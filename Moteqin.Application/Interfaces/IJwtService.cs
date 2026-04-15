public interface IJwtService
{
    Task<string> GenerateToken(ApplicationUser user);
}
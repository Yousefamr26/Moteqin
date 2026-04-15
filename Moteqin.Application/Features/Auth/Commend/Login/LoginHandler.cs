using MediatR;
using Microsoft.AspNetCore.Identity;

public class LoginHandler : IRequestHandler<LoginCommand, Result<AuthResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtService _jwt;

    public LoginHandler(UserManager<ApplicationUser> userManager, IJwtService jwt)
    {
        _userManager = userManager;
        _jwt = jwt;
    }

    public async Task<Result<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Request.Email);

        if (user == null)
            return Result<AuthResponse>.Failure("Invalid email or password");

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Request.Password);

        if (!isPasswordValid)
            return Result<AuthResponse>.Failure("Invalid email or password");

        var token = await _jwt.GenerateToken(user);

        return Result<AuthResponse>.Success(new AuthResponse
        {
            UserId = user.Id,
            UserName = user.UserName,
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        }, "Login successful");
    }
}
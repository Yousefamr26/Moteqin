using MediatR;
using Microsoft.AspNetCore.Identity;

public class RegisterHandler : IRequestHandler<RegisterCommand, Result<AuthResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtService _jwt;

    public RegisterHandler(UserManager<ApplicationUser> userManager, IJwtService jwt)
    {
        _userManager = userManager;
        _jwt = jwt;
    }

    public async Task<Result<AuthResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Request.Email);

        if (existingUser != null)
            return Result<AuthResponse>.Failure("Email already exists");

        var user = new ApplicationUser
        {
            UserName = request.Request.UserName,
            Email = request.Request.Email,
            NormalizedEmail = request.Request.Email.ToUpper()
        };

        var result = await _userManager.CreateAsync(user, request.Request.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors
                .Select(e => new Error("Identity", e.Description))
                .ToList();

            return Result<AuthResponse>.Failure("Registration failed", errors);
        }
        var token = await _jwt.GenerateToken(user);

        return Result<AuthResponse>.Success(new AuthResponse
        {
            UserId = user.Id,
            UserName = user.UserName,
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        }, "User registered successfully");
    }
}
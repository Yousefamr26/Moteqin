using MediatR;

public class LoginCommand : IRequest<Result<AuthResponse>>
{
    public LoginRequest Request { get; set; }
}
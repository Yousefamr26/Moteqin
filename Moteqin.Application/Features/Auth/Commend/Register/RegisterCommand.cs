using MediatR;

public class RegisterCommand : IRequest<Result<AuthResponse>>
{
    public RegisterRequest Request { get; set; }
}
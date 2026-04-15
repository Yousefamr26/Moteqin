using FluentValidation;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(x => x.Request.Email).NotEmpty();
        RuleFor(x => x.Request.Password).NotEmpty();
    }
}
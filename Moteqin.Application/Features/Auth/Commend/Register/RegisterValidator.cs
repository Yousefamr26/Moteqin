using FluentValidation;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Request.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Request.Password).MinimumLength(6);
        RuleFor(x => x.Request.UserName).NotEmpty();
    }
}
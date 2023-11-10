using FluentValidation;

namespace Application.Authentication.Commands.SignIn;

public class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(v => v.Username).NotEmpty();
        RuleFor(v => v.Password).NotEmpty();
    }
}
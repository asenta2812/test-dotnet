using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Authentication.Commands.SignUp;

public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public SignUpCommandValidator(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        RuleFor(v => v.Username).NotEmpty().Must(IsUsernameUnique).WithMessage("Username is already taken");
        RuleFor(v => v.Password).NotEmpty();
    }


    private bool IsUsernameUnique(string? username)
    {
        var a = _dbContext.Authors.FirstOrDefault(x => x.Username == username);
        return _dbContext.Authors.FirstOrDefault(x => x.Username == username) == null;
    }
}
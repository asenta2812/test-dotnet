using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Authentication.Commands.SignIn;

public class SignInCommand : IRequest<string>
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}

public class SignInCommandHandler : IRequestHandler<SignInCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtProvider _jwtProvider;

    public SignInCommandHandler(IApplicationDbContext context, IJwtProvider jwtProvider)
    {
        _context = context;
        _jwtProvider = jwtProvider;
    }

    public async Task<string> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(x => x.Username == request.Username, cancellationToken);
        if (author == null) throw new UnauthorizedAccessException("Username or password is incorrect");

        var isValidPassword = BCrypt.Net.BCrypt.Verify(request.Password, author.Password);
        if (isValidPassword == false)
            throw new UnauthorizedAccessException("Username or password is incorrect");

        return _jwtProvider.GenerateJwtToken(author);
    }
}
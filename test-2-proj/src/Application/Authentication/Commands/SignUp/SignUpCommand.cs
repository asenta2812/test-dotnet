using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Authentication.Commands.SignUp;

public class SignUpCommand : IRequest<int>
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, int>
{
    private readonly IApplicationDbContext _context;

    public SignUpCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var author = new Author
        {
            Username = request.Username,
            Password = hashPassword
        };

        _context.Authors.Add(author);
        await _context.SaveChangesAsync(cancellationToken);
        return author.Id;
    }
}
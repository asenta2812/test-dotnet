using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Blogs.Commands.Delete;

public record DeleteBlogCommand(int Id) : IRequest;

public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentAuthorService _currentAuthor;

    public DeleteBlogCommandHandler(IApplicationDbContext context, ICurrentAuthorService currentAuthor)
    {
        _context = context;
        _currentAuthor = currentAuthor;
    }

    public async Task Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Blogs
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null || entity.AuthorId != _currentAuthor.AuthorId)
            throw new NotFoundException(nameof(Blog), request.Id);

        entity.IsDeleted = true;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
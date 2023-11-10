using System.Text.Json.Serialization;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Blogs.Commands.Update;

public class UpdateBlogCommand : IRequest<bool>
{
    [JsonIgnore] public int Id { get; set; }

    public string? Title { get; set; }
    public string? Content { get; set; }
}

public class UpdatedBlogCommandHandler : IRequestHandler<UpdateBlogCommand, bool>
{
    private readonly IApplicationDbContext _applicationDb;
    private readonly ICurrentAuthorService _currentAuthor;

    public UpdatedBlogCommandHandler(IApplicationDbContext applicationDb, ICurrentAuthorService currentAuthor)
    {
        _applicationDb = applicationDb;
        _currentAuthor = currentAuthor;
    }

    public async Task<bool> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
    {
        var blog = await _applicationDb.Blogs.FindAsync(new object[] { request.Id }, cancellationToken);

        if (blog == null || blog.AuthorId != _currentAuthor.AuthorId) throw new NotFoundException("Blog", request.Id);


        if (request.Title != null) blog.Title = request.Title;
        if (request.Content != null) blog.Content = request.Content;

        await _applicationDb.SaveChangesAsync(cancellationToken);

        return true;
    }
}
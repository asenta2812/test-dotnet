using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Blogs.Commands.Create;

public class CreateBlogPostCommand : IRequest<int>
{
    public string? Title { get; set; }

    public string? Content { get; set; }
}

public class CreateBlogPostCommandHandler : IRequestHandler<CreateBlogPostCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentAuthorService _currentAuthorService;

    public CreateBlogPostCommandHandler(IApplicationDbContext context, ICurrentAuthorService currentAuthorService)
    {
        _context = context;
        _currentAuthorService = currentAuthorService;
    }

    public async Task<int> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var entity = new Blog
        {
            Title = request.Title,
            Content = request.Content,
            AuthorId = _currentAuthorService.AuthorId,
            Status = BlogStatus.Active
        };

        _context.Blogs.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
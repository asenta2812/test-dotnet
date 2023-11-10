using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Blogs.Queries;

public class BlogDto : IMapFrom<Blog>
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Content { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Blog, BlogDto>();
    }
}

public record GetListBlogQuery : IRequest<ListResponse<BlogDto>>;

public class GetListBlogQueryHandler : IRequestHandler<GetListBlogQuery, ListResponse<BlogDto>>
{
    private readonly ICurrentAuthorService _currentAuthor;
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetListBlogQueryHandler(IApplicationDbContext dbContext, IMapper mapper, ICurrentAuthorService currentAuthor)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _currentAuthor = currentAuthor;
    }

    public async Task<ListResponse<BlogDto>> Handle(GetListBlogQuery request, CancellationToken cancellationToken)
    {
        var items = await _dbContext.Blogs
            .Where(w => w.AuthorId == _currentAuthor.AuthorId && w.IsDeleted == false)
            .ProjectTo<BlogDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new ListResponse<BlogDto>(items);
    }
}
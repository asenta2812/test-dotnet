using System.Security.Claims;
using Application.Common.Interfaces;

namespace Presentation.Services;

public class CurrentAuthorService : ICurrentAuthorService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentAuthorService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? AuthorId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}
using Application.Blogs.Commands.Create;
using Application.Blogs.Commands.Delete;
using Application.Blogs.Commands.Update;
using Application.Blogs.Queries;
using Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Authorize]
[Route("blogs")]
public class BlogController : ApiBaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResponse<BlogDto>>> GetBlogPosts([FromQuery] GetListBlogQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateBlogPostCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<bool>> Update(int id, UpdateBlogCommand command)
    {
        command.Id = id;

        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteBlogCommand(id));

        return NoContent();
    }
}
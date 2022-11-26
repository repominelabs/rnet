using Application.Common.Models;
using Application.Features.Post.Commands.CreatePost;
using Application.Features.Post.Commands.DeletePost;
using Application.Features.Post.Commands.UpdatePost;
using Application.Features.Post.Queries.GetPostsWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.V2;

[Route("Api/V{version:apiVersion}/[controller]/[action]")]
[ApiVersion("2.0")]
public class PostController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreatePostCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Delete(DeletePostCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost]
    public async Task<ActionResult<PaginatedList<Domain.Entities.Post>>> GetPostsWithPagination(GetPostsWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Update(UpdatePostCommand command)
    {
        return await Mediator.Send(command);
    }
}

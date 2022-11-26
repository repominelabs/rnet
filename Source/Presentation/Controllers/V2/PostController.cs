using Application.Features.Post.Commands.CreatePost;
using Application.Features.Post.Commands.DeletePost;
using Application.Features.Post.Commands.UpdatePost;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.V2;

[Route("Api/V{version:apiVersion}/[controller]")]
[ApiVersion("2.0")]
public class PostController : BaseController
{
    public PostController()
    {
    }

    [HttpPost("Create")]
    public async Task<ActionResult<int>> Create(CreatePostCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("Delete")]
    public async Task<ActionResult<int>> Delete(DeletePostCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("Update")]
    public async Task<ActionResult<int>> Update(UpdatePostCommand command)
    {
        return await Mediator.Send(command);
    }
}

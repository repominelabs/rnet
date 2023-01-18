using Application.Features.Comment.CreateComment;
using Application.Features.Comment.DeleteComment;
using Application.Features.Comment.UpdateComment;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.V2;

[Route("Api/V{version:apiVersion}/[controller]/[action]")]
[ApiVersion("2.0")]
public class CommentController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateCommentCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Delete(DeleteConfigurationCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Update(UpdateCommentCommand command)
    {
        return await Mediator.Send(command);
    }
}

using Application.Features.Comment.CreateComment;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.V1;

[Route("Api/V{version:apiVersion}/[controller]/[action]")]
[ApiVersion("1.0", Deprecated = true)]
public class CommentController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateCommentCommand command)
    {
        return await Mediator.Send(command);
    }
}

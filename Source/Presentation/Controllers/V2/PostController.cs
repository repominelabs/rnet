using Application.Features.Post.Commands.CreatePost;
using Application.Features.Post.Commands.UpdatePost;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.V2;

[Route("Api/V{version:apiVersion}/[controller]")]
[ApiVersion("2.0")]
public class PostController : BaseController
{
    private readonly IPostService _postService;
    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpPost("Create")]
    public async Task<ActionResult<int>> Create(CreatePostCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("Update")]
    public async Task<ActionResult<int>> Update(UpdatePostCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("CreatePosts")]
    public ActionResult<string> CreatePosts([FromBody] List<Post> request)
    {
        var res = _postService.CreatePosts(request);
        return Ok();
    }

    [HttpPost("CreatePostsAsync")]
    public async Task<ActionResult<string>> CreatePostsAsync([FromBody] List<Post> request)
    {
        var res = await _postService.CreatePostsAsync(request);
        return Ok();
    }
}

using Domain.Entities;

namespace Application.Interfaces.Services;

public interface IPostService
{
    dynamic CreatePosts(List<Post> request);
    Task<dynamic> CreatePostsAsync(List<Post> request);
}

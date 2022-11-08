using Application.Interfaces.Services;
using Domain.Entities;

namespace Application.Services;

public class PostService : IPostService
{
    public PostService()
	{
	}

	public dynamic CreatePosts(List<Post> request)
	{
        return "";
    }

	public async Task<dynamic> CreatePostsAsync(List<Post> request)
	{
		return "";
    }
}

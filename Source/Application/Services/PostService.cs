using Application.Interfaces.DatabaseManagers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Oracle.ManagedDataAccess.Client;

namespace Application.Services;

public class PostService : IPostService
{
	private readonly IPostRepository _postRepository;
    private readonly IDatabaseManager _databaseManager ;

    public PostService(IPostRepository postRepository, IDatabaseManager databaseManager)
	{
		_postRepository = postRepository;
		_databaseManager = databaseManager;
	}

	public dynamic CreatePosts(List<Post> request)
	{
        using (var conn = new OracleConnection("{connection string}"))
        {
            conn.Open();
            // Create the transaction
            // You could use `var` instead of `SqlTransaction`
            using (OracleTransaction tran = conn.BeginTransaction())
            {
                try
                {
                    foreach (Post post in request)
                    {
                        int postId = _databaseManager.Create(post, conn, tran);
                        foreach (Comment comment in post.Comments)
                        {
                            comment.PostId = postId;
                            _databaseManager.Create(comment, conn, tran);
                        }
                    }
                    List<Post> posts = _databaseManager.Get<Post>(null, conn, tran);
                    foreach (Post post in posts)
                    {
                        post.Title = "Test";
                        _postRepository.CreateOrUpdate(post, false, null, conn, tran);

                        List<Comment> comments = _databaseManager.Get<Comment>($"post_id = '{post.Id}'", conn, tran);
                        Console.WriteLine(comments);
                    }
                    tran.Commit();
                    return "SUCCCESS";
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception:" + ex.Message);
                    // Roll the transaction back
                    tran.Rollback();
                    // Handle the error however you need to.
                    return "ERROR";
                }
            }
        }
    }

	public async Task<dynamic> CreatePostsAsync(List<Post> request)
	{
        using (var conn = new OracleConnection("{connection string}"))
        {
            conn.Open();
            // Create the transaction
            // You could use `var` instead of `SqlTransaction`
            using (OracleTransaction tran = conn.BeginTransaction())
            {
                try
                {
                    foreach (Post post in request)
                    {
                        int postId = await _databaseManager.CreateAsync(post, conn, tran);
                        foreach (Comment comment in post.Comments)
                        {
                            comment.PostId = postId;
                            await _databaseManager.CreateAsync(comment, conn, tran);
                        }
                    }
                    List<Post> posts = await _databaseManager.GetAsync<Post>(null, conn, tran);
                    foreach (Post post in posts)
                    {
                        post.Title = "Test";
                        await _postRepository.CreateOrUpdateAsync(post, false, null, conn, tran);

                        List<Comment> comments = await _databaseManager.GetAsync<Comment>($"post_id = '{post.Id}'",conn, tran);
                        Console.WriteLine(comments);
                    }
                    tran.Commit();
                    return "SUCCCESS";
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception:" + ex.Message);
                    // Roll the transaction back
                    tran.Rollback();
                    // Handle the error however you need to.
                    return "ERROR";
                }
            }
        }
    }
}

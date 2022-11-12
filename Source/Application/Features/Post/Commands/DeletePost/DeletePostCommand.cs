using Application.Interfaces.Contexts;
using MediatR;

namespace Application.Features.Post.Commands.DeletePost;

public class DeletePostCommand : IRequest<int>
{
    public int Id { get; set; }
}

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, int>
{
    private readonly IApplicationDbContext _context;

    public DeletePostCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<int> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Post post = new() { Id = request.Id };
        _context.Posts.Remove(post);
        return Task.FromResult(0);
    }
}
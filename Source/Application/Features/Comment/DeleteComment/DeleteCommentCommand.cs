using Application.Interfaces.Contexts;
using MediatR;

namespace Application.Features.Comment.DeleteComment;

public class DeleteCommentCommand : IRequest<int>
{
    public int Id { get; set; }
}

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, int>
{
    private readonly IApplicationDbContext _context;

    public DeleteCommentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<int> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Comment comment = new() { Id = request.Id };
        _context.Comments.Remove(comment);
        return Task.FromResult(0);
    }
}

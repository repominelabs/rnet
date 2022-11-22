using MediatR;

namespace Application.Features.Comment.CreateComment;

public class CreateCommentCommand : IRequest<int>
{
    public int PostId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Body { get; set; }
}

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, int>
{
    public Task<int> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

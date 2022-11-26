using Application.Interfaces.Contexts;
using AutoMapper;
using Domain.Events.Comment;
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
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateCommentCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Comment comment = _mapper.Map<CreateCommentCommand, Domain.Entities.Comment>(request);

        comment.AddDomainEvent(new CommentCreatedEvent(comment));
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync(cancellationToken);

        return comment.Id;
    }
}

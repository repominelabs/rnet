using Application.Interfaces.Contexts;
using AutoMapper;
using Domain.Events.Post;
using MediatR;

namespace Application.Features.Post.Commands.CreatePost;

public class CreatePostCommand : IRequest<int>
{
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreatePostCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Post post = _mapper.Map<CreatePostCommand, Domain.Entities.Post>(request);

        post.AddDomainEvent(new PostCreatedEvent(post));
        _context.Posts.Add(post);
        await _context.SaveChangesAsync(cancellationToken);

        return post.Id;
    }
}

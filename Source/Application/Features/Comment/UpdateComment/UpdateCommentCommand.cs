using Application.Interfaces.Contexts;
using AutoMapper;
using MediatR;

namespace Application.Features.Comment.UpdateComment;

public class UpdateCommentCommand : IRequest<int>
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Body { get; set; }
}

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateCommentCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<int> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Comment comment = _mapper.Map<UpdateCommentCommand, Domain.Entities.Comment>(request);
        _context.Comments.Update(comment);
        return Task.FromResult(0);
    }
}

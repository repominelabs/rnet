using Application.Interfaces.Repositories;
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
    private readonly IUnitOfWork _unitOfWork;

    public CreatePostCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Post post = new ();
        int response = await _unitOfWork.Posts.CreateAsync(post);
        return response;
    }
}

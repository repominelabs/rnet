using Application.Interfaces.Repositories;
using AutoMapper;
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
    private readonly IMapper _mapper;

    public CreatePostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Post post = _mapper.Map<CreatePostCommand, Domain.Entities.Post>(request);
        int response = await _unitOfWork.Posts.CreateAsync(post);
        return response;
    }
}

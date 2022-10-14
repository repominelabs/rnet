using Application.Interfaces.Contexts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Post.Commands.UpdatePost;

public class UpdatePostCommand : IRequest<int>
{
    public int MyProperty { get; set; }
}

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdatePostCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<int> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Post post = _mapper.Map<UpdatePostCommand, Domain.Entities.Post>(request);
        _context.Posts.Update(post);
        return Task.FromResult(0);
    }
}
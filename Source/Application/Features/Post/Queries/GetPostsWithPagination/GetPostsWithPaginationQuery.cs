using Application.Common.Mappings;
using Application.Common.Models;
using Application.Interfaces.Contexts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.Features.Post.Queries.GetPostsWithPagination;

public class GetPostsWithPaginationQuery : IRequest<PaginatedList<Domain.Entities.Post>>
{
    public int Id { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetPostsWithPaginationQueryHandler : IRequestHandler<GetPostsWithPaginationQuery, PaginatedList<Domain.Entities.Post>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPostsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<Domain.Entities.Post>> Handle(GetPostsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Posts
            .Where(x => x.Id == request.Id)
            .OrderBy(x => x.Title)
            .ProjectTo<Domain.Entities.Post>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}

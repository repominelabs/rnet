using Application.Features.Post.Commands.CreatePost;
using AutoMapper;

namespace Application.Common.Mappings;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<CreatePostCommand, Domain.Entities.Post>();
    }
}

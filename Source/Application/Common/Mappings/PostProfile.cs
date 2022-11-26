using Application.Features.Post.Commands.CreatePost;
using Application.Features.Post.Commands.UpdatePost;
using AutoMapper;

namespace Application.Common.Mappings;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<CreatePostCommand, Domain.Entities.Post>();
        CreateMap<UpdatePostCommand, Domain.Entities.Post>();
    }
}

using Application.Features.Comment.CreateComment;
using Application.Features.Comment.UpdateComment;
using AutoMapper;

namespace Application.Common.Mappings;

public class CommentProfile : Profile
{
	public CommentProfile()
	{
        CreateMap<CreateCommentCommand, Domain.Entities.Comment>();
        CreateMap<UpdateCommentCommand, Domain.Entities.Comment>();
    }
}

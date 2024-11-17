using AutoMapper;
using StudioModel.Domain;
using StudioModel.Dtos.Comment;

namespace StudioBack.Mapper
{
    public class CommentMapper : Profile
    {
        public CommentMapper()
        {
            CreateMap<Comment, CommentDto>().ReverseMap();
        }
    }
}

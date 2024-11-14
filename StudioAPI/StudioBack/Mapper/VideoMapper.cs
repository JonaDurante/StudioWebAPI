using AutoMapper;
using StudioModel.Domain;
using StudioModel.Dtos.Video;

namespace StudioBack.Mapper
{
    public class VideoMapper :Profile
    {
        public VideoMapper()
        {
            CreateMap<RequestVideoDto, Video>();
        }
    }
}

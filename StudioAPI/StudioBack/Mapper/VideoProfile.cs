using AutoMapper;
using StudioModel.Domain;
using StudioModel.Dtos.Video;

namespace StudioBack.Mapper
{
    public class VideoProfile :Profile
    {
        public VideoProfile()
        {
            CreateMap<RequestVideoDto, Video>();
        }
    }
}

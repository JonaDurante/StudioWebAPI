using AutoMapper;
using StudioModel.Domain;
using StudioModel.Dtos.Course;

namespace StudioBack.Mapper
{
	public class CourseProfile : Profile
	{
		public CourseProfile()
		{
			CreateMap<CourseDto, Course>().ReverseMap();
		}
	}
}

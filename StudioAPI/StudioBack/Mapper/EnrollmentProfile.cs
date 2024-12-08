using AutoMapper;
using StudioModel.Domain;
using StudioModel.Dtos.Enrollment;

namespace StudioBack.Mapper
{
	public class EnrollmentProfile : Profile
	{
		public EnrollmentProfile()
		{
			CreateMap<EnrollmentDto, Enrollment>().ReverseMap();
		}
	}
}

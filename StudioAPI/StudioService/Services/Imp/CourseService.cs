using AutoMapper;
using StudioDataAccess.Uow;
using StudioModel.Domain;
using StudioModel.Dtos.Course;

namespace StudioService.Services.Imp
{
	public class CourseService : ICourseService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public CourseService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public Task<Course?> Create(Guid id, CourseDto courseDto)
		{
			throw new NotImplementedException();
		}

		public void Delete(Guid id)
		{
			_unitOfWork.CourseRepository.LogicDelete(id);
			_unitOfWork.Save();
			return;
		}

		public Task<Course?> Get(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<List<Course>> GetAll()
		{
			return await _unitOfWork.CourseRepository.GetAll();
		}

		public Task<Course?> Update(Guid id, CourseDto courseDto)
		{
			throw new NotImplementedException();
		}
	}
}

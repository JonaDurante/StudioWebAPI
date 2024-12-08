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
		public async Task<Course?> Create(Guid id, CourseDto courseDto)
		{
			var course = _mapper.Map<Course>(courseDto);

			if (course != null)
			{
				await _unitOfWork.CourseRepository.Add(course);
				_unitOfWork.Save();
				return course;
			}
			return null;
		}

		public void Delete(Guid id)
		{
			//AGREGAR LOGICA PARA EVITAR BORRAR CURSOS CON ALUMNOS INSCRIPTOS
			_unitOfWork.CourseRepository.LogicDelete(id);
			_unitOfWork.Save();
			return;
		}

		public async Task<Course?> GetById(Guid id)
		{
			var course = _unitOfWork.CourseRepository.GetActive(up => up.Id == id).FirstOrDefault();

			if (course != null)
			{
				return course;
			}
			return null;
		}

		public async Task<List<Course>> GetAll()
		{
			return await _unitOfWork.CourseRepository.GetAll();
		}

		public async Task<Course?> Update(Guid id, CourseDto courseDto)
		{
			var course = await GetById(id);
			if (course != null)
			{
				_mapper.Map(courseDto, course);
				_unitOfWork.CourseRepository.Update(course);
				_unitOfWork.Save();
				return course;
			}
			return null;
		}
	}
}

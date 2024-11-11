using AutoMapper;
using StudioDataAccess.Uow;
using StudioModel.Domain;
using StudioModel.Dtos.Enrollment;

namespace StudioService.Services.Imp
{
	public class EnrollmentService : IEnrollmentService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public EnrollmentService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<Enrollment> Create(Guid id, EnrollmentDto enrollmentDto)
		{
			var enrollmentActive = await _unitOfWork.EnrollmentRepository.GetById(id);
			if (enrollmentActive != null)
			{
				var enrollment = _mapper.Map<Enrollment>(enrollmentDto);
				await _unitOfWork.EnrollmentRepository.Add(enrollment);
				_unitOfWork.Save();
				return enrollment;
			}
			throw new Exception("El usuario ya esta registrado");
		}

		public Task<List<Enrollment>> GetAllEnrollments()
		{
			return _unitOfWork.EnrollmentRepository.GetAll();
		}

		public async Task<Enrollment> GetById(Guid id)
		{
			var enrollment = await _unitOfWork.EnrollmentRepository.GetById(id);
			return enrollment;
		}

		public void Delete(Guid id)
		{
			_unitOfWork.EnrollmentRepository.LogicDelete(id);
			_unitOfWork.Save();
			return;
		}

		public async Task<IEnumerable<Enrollment>> GetAllEnrollmentsByUser(Guid id)
		{
			var listEnrollments = await _unitOfWork.EnrollmentRepository.GetAll();
			return listEnrollments.Where(x => x.UserId == id.ToString());
		}
	}
}

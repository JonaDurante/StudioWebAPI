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
			var enrollment = _mapper.Map<Enrollment>(enrollmentDto);

			if (enrollment != null)
			{
				await _unitOfWork.EnrollmentRepository.Add(enrollment);
				_unitOfWork.Save();
				return enrollment;
			}
			return null;
		}

		public Task<List<Enrollment>> GetAllEnrollments()
		{
			throw new NotImplementedException();
		}

		public Task<Enrollment> GetById(Guid id)
		{
			throw new NotImplementedException();
		}
		public void Delete(Guid id)
		{
			_unitOfWork.EnrollmentRepository.LogicDelete(id);
			_unitOfWork.Save();
			return;
		}

	}
}

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
        public async Task<bool> EnrollUser(EnrollmentDto enrollmentDto)
        {
            if (enrollmentDto.EnrollmentDate < DateTime.UtcNow)
            {
                throw new Exception("La fecha debe ser mayor al dia de hoy");
            }

            var course = await _unitOfWork.CourseRepository.GetById(enrollmentDto.CourseId);
            if (course == null)
            {
                throw new Exception("El curso especificado no existe.");
            }

            var user = await _unitOfWork.UserProfileRepository.GetById(course.Id);
            if (user == null)
            {
                throw new Exception("El usuario no existe.");
            }

            var existingEnrollment = await _unitOfWork.EnrollmentRepository
        .Filter(e => e.UserId == user.Id && e.CourseId == course.Id, true)
        .FirstOrDefaultAsync();

            if (existingEnrollment != null)
            {
                throw new Exception("El usuario ya está inscrito en este curso.");
            }


            var enrollment = _mapper.Map<Enrollment>(enrollmentDto);
            await _unitOfWork.EnrollmentRepository.Add(enrollment);
            _unitOfWork.Save();

            return true;
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

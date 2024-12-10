using AutoMapper;
using FluentAssertions;
using Moq;
using StudioDataAccess.Repositories;
using StudioDataAccess.Uow;
using StudioModel.Domain;
using StudioModel.Dtos.Enrollment;
using StudioService.Services.Imp;
using Xunit;

namespace StudioBack.Tests
{
    public class EnrollmentServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IMapper> _mapperMock;
        private EnrollmentService _enrollmentService;

        public EnrollmentServiceTests()
        {
            Setup();
            _enrollmentService = new EnrollmentService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        private void Setup()
        {
            var mockCourseRepository = new Mock<ICourseRepository>();
            mockCourseRepository.Setup(c => c.GetById(It.IsAny<Guid>())).ReturnsAsync(new Course()
            {
                Id = Guid.NewGuid()
            });
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(uow => uow.CourseRepository).Returns(mockCourseRepository.Object);
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task EnrollUserTest()
        {
            var mockedDto = new EnrollmentDto()
            {
                CourseId = Guid.NewGuid(),
                EnrollmentDate = DateTime.UtcNow.AddDays(1),
                UserId = Guid.NewGuid().ToString(),
            };

            var result = await _enrollmentService.EnrollUser(mockedDto);
        }

        [Fact]
        public async Task EnrollUserTestFailOnEnrollmentDay()
        {
            var mockedDto = new EnrollmentDto()
            {
                CourseId = Guid.NewGuid(),
                EnrollmentDate = DateTime.UtcNow.AddDays(-10),
                UserId = Guid.NewGuid().ToString(),
            };

            Func<Task> act = async () => await _enrollmentService.EnrollUser(mockedDto);

            await act.Should().ThrowAsync<Exception>().WithMessage("La fecha debe ser mayor al dia de hoy");
        }
    }
}

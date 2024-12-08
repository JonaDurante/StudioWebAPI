using System.ComponentModel.DataAnnotations;

namespace StudioModel.Dtos.Enrollment
{
    public class EnrollmentDto
	{
		[Required]
		[DataType(DataType.DateTime)]
		public DateTime EnrollmentDate { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public Guid CourseId { get; set; }
	}
}

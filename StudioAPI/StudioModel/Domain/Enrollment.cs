using System.ComponentModel.DataAnnotations;

namespace StudioModel.Domain
{
    public class Enrollment : Entity
	{
		[Required]
		[DataType(DataType.DateTime)]
		public DateTime EnrollmentDate { get; set; }
		
		[Required]
		public string UserId { get; set; }
		public UserApp User { get; set; }

		[Required]
		public Guid CourseId { get; set; }
		public Course Course { get; set; }
	}
}

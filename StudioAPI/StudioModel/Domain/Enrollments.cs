using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioModel.Domain
{
	public class Enrollments
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		public DateTime EnrollmentDate { get; set; }

		public string UserAppId { get; set; }
		public UserApp User { get; set; }
		public Guid CourseId { get; set; }
		public Courses Course { get; set; }
	}
}

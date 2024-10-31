using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioModel.Domain
{
	public class Enrollment
	{
		[Key]
		public Guid Id { get; set; }

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

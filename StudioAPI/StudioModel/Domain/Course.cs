using System.ComponentModel.DataAnnotations;

namespace StudioModel.Domain
{
	public class Course : Entity
	{
		[Required]
		[StringLength(20)]
		public string Name { get; set; }

		[Required]
		[StringLength(100)]
		public string Description { get; set; }

		[Required]
		public string Level { get; set; }

		public ICollection<Enrollment> Enrollments { get; set; }

	}
}

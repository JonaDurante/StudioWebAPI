using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StudioModel.Abstraction;

namespace StudioModel.Domain
{
	public class UserProfile : Entity
	{
		[Required]
		[DataType(DataType.Text)]
		public string UserName { get; set; }

		[Required]
		[MaxLength(50)]
		public string FirstName { get; set; }

		[Required]
		[MaxLength(50)]
		public string LastName { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime BirthDate { get; set; }

		[MaxLength(50)]
		public string Address { get; set; }

		public DateTime RegistrationDate { get; set; } = DateTime.Now;

		public DateTime LastClassDate { get; set; }

		[DataType(DataType.ImageUrl)]
		public string? UserPhoto { get; set; }

		public UserApp User { get; set; }
	}
}

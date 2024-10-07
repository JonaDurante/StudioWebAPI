using System.ComponentModel.DataAnnotations;

namespace StudioModel.Dtos.UserProfile
{
	public class UserProfileDto
	{
		[DataType(DataType.Text)]
		public string UserName { get; set; }

		[DataType(DataType.Text)]
		public string FirstName { get; set; }

		[DataType(DataType.Text)]
		public string LastName { get; set; }

		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[DataType(DataType.DateTime)]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime BirthDate { get; set; }

		public DateTime? LastClassDate { get; set; }

		[MaxLength(30)]
		public string Address { get; set; }

		[DataType(DataType.PhoneNumber)]
		public string? PhoneNumber { get; set; }

		[DataType(DataType.ImageUrl)]
		public string? UserPhoto { get; set; }

	}
}

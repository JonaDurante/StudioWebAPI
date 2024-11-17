using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioModel.Domain
{
	public class UserProfile : Entity
	{
		[Required]
		[DataType(DataType.Text)]
		public string UserName { get; set; }

		[Required]
		[MaxLength(20)]
		public string FirstName { get; set; }

		[Required]
		[MaxLength(20)]
		public string LastName { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime BirthDate { get; set; }

		[MaxLength(30)]
		public string Address { get; set; }

		[DataType(DataType.PhoneNumber)]
		public string? PhoneNumber { get; set; }

		public DateTime RegistrationDate { get; set; } = DateTime.Now;

		public DateTime? LastClassDate { get; set; }

		[DataType(DataType.ImageUrl)]
		public string? UserPhoto { get; set; }

        [ForeignKey("UserApp")]
		public string IdUser { get; set; }
		public UserApp UserApp { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
	}
}

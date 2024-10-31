using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioModel.Domain
{
	public class UserApp : IdentityUser
	{
		[NotMapped]
		public string Role { get; set; }

		public UserProfile? UserProfile { get; set; }
	}
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;

namespace StudioModel.Domain
{
	public class UserApp : IdentityUser
	{
		[NotMapped]
		public string Role { get; set; }
		public UserProfile? UserProfile { get; set; }
	}
}

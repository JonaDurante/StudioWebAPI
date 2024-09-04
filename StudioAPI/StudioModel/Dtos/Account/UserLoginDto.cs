using System.ComponentModel.DataAnnotations;

namespace StudioModel.Dtos.Account
{
    public class UserLoginDto
    {
        [Required]
        [DataType(DataType.Text)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}

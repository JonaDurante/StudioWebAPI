using System.ComponentModel.DataAnnotations;

namespace StudioModel.Dtos.Account
{
    public class UserRegisterDto 
    {
        [Required]
        [DataType(DataType.Text)]
        public required string UserName { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}

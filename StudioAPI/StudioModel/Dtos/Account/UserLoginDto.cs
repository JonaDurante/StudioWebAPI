using System.ComponentModel.DataAnnotations;

namespace StudioModel.Dtos.Account
{
    public class UserLoginDto
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}

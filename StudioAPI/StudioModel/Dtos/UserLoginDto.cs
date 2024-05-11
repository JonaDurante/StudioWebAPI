using System.ComponentModel.DataAnnotations;

namespace StudioModel.Dtos
{
    public class UserLoginDto
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace StudioModel.Dtos.UserAndRole
{
    public class UserAndRoleDto
    {
        [DataType(DataType.Text)]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        public string CurrentRole { get; set; }

        [DataType(DataType.Text)]
        public string Role { get; set; }
    }
}

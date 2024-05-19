using System.ComponentModel.DataAnnotations;

namespace StudioModel.Dtos.Role
{
    public class RoleDto
    {
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
    }
}

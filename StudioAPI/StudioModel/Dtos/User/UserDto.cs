using System.ComponentModel.DataAnnotations;

namespace StudioModel.Dtos.User
{
    public class UserDto
    {
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [DataType(DataType.Text)]
        public string CustomUserName { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "DD/MM/YYYY")]
        public DateTime Birthday { get; set; }

        [DataType(DataType.Text)]
        public string Role { get; set; }
    }
}

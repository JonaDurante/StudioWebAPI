using System.ComponentModel.DataAnnotations;

namespace StudioModel.Dtos.User
{
    public class ProfileDto
    {
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [DataType(DataType.Text)]
        public string CustomUserName { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.ImageUrl)]
        public string UserPhoto { get; set; }


    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace StudioDataAccess
{
    public class UserApp : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string CustomUserName { get; set; }

        [PersonalData]
        [Column(TypeName = "Datetime")]
        [DisplayFormat(DataFormatString = "DD/MM/YYYY")]
        public DateTime Birthday { get; set; }
    }
}

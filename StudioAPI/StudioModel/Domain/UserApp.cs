using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace StudioModel.Domain
{
    public class UserApp : IdentityUser
    {
        [Column(TypeName = "varchar(100)")]
        public string? UserPhoto { get; set; }

        [PersonalData]
        [Column(TypeName = "Datetime")]
        [DisplayFormat(DataFormatString = "DD/MM/YYYY")]
        public DateTime Birthday { get; set; }

        [NotMapped]
        public string Role { get; set; }
    }
}

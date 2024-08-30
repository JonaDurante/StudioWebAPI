using StudioModel.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioModel.Dtos.Account
{
    public  class ProfileEditDto
    {
        public ProfileDto userProfile { get; set; }
        public string idUser { get; set; }
    }
}

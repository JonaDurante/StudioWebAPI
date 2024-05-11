using Microsoft.AspNetCore.Mvc;
using StudioModel.Domain;
using StudioModel.Dtos;

namespace StudioService.LoginService
{
    public interface ILoginService
    {
        Task<UserToken?> Login(UserLoginDto userLoginDto);
    }
}

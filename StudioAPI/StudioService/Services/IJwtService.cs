﻿using StudioModel.Domain;

namespace StudioService.Services
{
    public interface IJwtService
    {
        UserToken GeneratedToken(UserApp userApp);
        UserToken RefreshToken(string token);
    }
}

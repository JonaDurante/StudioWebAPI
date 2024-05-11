using StudioModel.Domain;

namespace StudioService
{
    public interface IJwtService
    {
        UserToken GeneratedToken(UserApp userApp);
    }
}

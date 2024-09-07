using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StudioModel.Constant;

namespace StudioBack.Helppers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthotizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _claimValue;
        private readonly string UserClaimName = "Role";
        public AuthotizeAttribute(string claimValue)
        {
            _claimValue = claimValue;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity!.IsAuthenticated || 
                (!context.HttpContext.User.HasClaim(UserClaimName, _claimValue.ToLower()) &&
                !context.HttpContext.User.HasClaim(UserClaimName, AuthorizationData.Admin.ToLower())))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}

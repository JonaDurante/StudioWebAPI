using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StudioModel.Constant;

namespace StudioBack.Helppers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthotizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _claimValue;

        public AuthotizeAttribute(string claimValue)
        {
            _claimValue = claimValue;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var a = context.HttpContext.User;

            if (!context.HttpContext.User.Identity!.IsAuthenticated)
            {
                context.Result = new ForbidResult();
                return;
            }

            if (!context.HttpContext.User.HasClaim(AuthorizationData.UserClaimName, _claimValue.ToLower()) &&
                !context.HttpContext.User.HasClaim(AuthorizationData.UserClaimName, AuthorizationData.Admin.ToLower()))
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}

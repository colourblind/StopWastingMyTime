using System;
using System.Linq;
using System.Web.Mvc;

namespace StopWastingMyTime
{
    public class PermissionsRequired : FilterAttribute, IAuthorizationFilter
    {
        private readonly string[] _requiredPermissions;

        public PermissionsRequired(params string[] requiredPermissions)
        {
            _requiredPermissions = requiredPermissions;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            Models.User user = new Models.User(filterContext.HttpContext.User.Identity.Name);
            if (user == null || user.IsNew)
                throw new UnauthorizedAccessException("User not logged in");

            foreach (string permission in _requiredPermissions)
            {
                if (!user.HasPermission(permission))
                    throw new UnauthorizedAccessException(String.Format("User does not have required permission", user.UserId, permission));
            }
        }
    }
}

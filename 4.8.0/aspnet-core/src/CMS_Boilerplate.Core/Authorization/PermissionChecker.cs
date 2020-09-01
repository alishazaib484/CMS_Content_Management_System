using Abp.Authorization;
using CMS_Boilerplate.Authorization.Roles;
using CMS_Boilerplate.Authorization.Users;

namespace CMS_Boilerplate.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}

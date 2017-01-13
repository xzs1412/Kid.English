using Abp.Authorization;
using Kid.English.Authorization.Roles;
using Kid.English.MultiTenancy;
using Kid.English.Users;

namespace Kid.English.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}

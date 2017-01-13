using Abp.MultiTenancy;
using Abp.Zero.Configuration;

namespace Kid.English.Authorization.Roles
{
    /// <summary>
    /// 应用角色配置
    /// 在管理管理配置里添加2个静态角色:宿主"Admin"和租户"Admin"
    /// </summary>
    public static class AppRoleConfig
    {
        public static void Configure(IRoleManagementConfig roleManagementConfig)
        {
            //Static host roles

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Host.Admin,
                    MultiTenancySides.Host)
                );

            //Static tenant roles

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Tenants.Admin,
                    MultiTenancySides.Tenant)
                );
        }
    }
}

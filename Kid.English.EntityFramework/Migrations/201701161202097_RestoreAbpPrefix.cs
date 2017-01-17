namespace Kid.English.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RestoreAbpPrefix : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "Abp.AuditLogs", newName: "AbpAuditLogs");
            RenameTable(name: "Abp.BackgroundJobs", newName: "AbpBackgroundJobs");
            RenameTable(name: "Abp.Features", newName: "AbpFeatures");
            RenameTable(name: "Abp.Editions", newName: "AbpEditions");
            RenameTable(name: "Abp.Languages", newName: "AbpLanguages");
            RenameTable(name: "Abp.LanguageTexts", newName: "AbpLanguageTexts");
            RenameTable(name: "Abp.Notifications", newName: "AbpNotifications");
            RenameTable(name: "Abp.NotificationSubscriptions", newName: "AbpNotificationSubscriptions");
            RenameTable(name: "Abp.OrganizationUnits", newName: "AbpOrganizationUnits");
            RenameTable(name: "Abp.Permissions", newName: "AbpPermissions");
            RenameTable(name: "Abp.Users", newName: "AbpUsers");
            RenameTable(name: "Abp.UserClaims", newName: "AbpUserClaims");
            RenameTable(name: "Abp.UserLogins", newName: "AbpUserLogins");
            RenameTable(name: "Abp.UserRoles", newName: "AbpUserRoles");
            RenameTable(name: "Abp.Settings", newName: "AbpSettings");
            RenameTable(name: "Abp.Roles", newName: "AbpRoles");
            RenameTable(name: "Abp.TenantNotifications", newName: "AbpTenantNotifications");
            RenameTable(name: "Abp.Tenants", newName: "AbpTenants");
            RenameTable(name: "Abp.UserAccounts", newName: "AbpUserAccounts");
            RenameTable(name: "Abp.UserLoginAttempts", newName: "AbpUserLoginAttempts");
            RenameTable(name: "Abp.UserNotifications", newName: "AbpUserNotifications");
            RenameTable(name: "Abp.UserOrganizationUnits", newName: "AbpUserOrganizationUnits");
            MoveTable(name: "Abp.AbpAuditLogs", newSchema: "dbo");
            MoveTable(name: "Abp.AbpBackgroundJobs", newSchema: "dbo");
            MoveTable(name: "Abp.AbpFeatures", newSchema: "dbo");
            MoveTable(name: "Abp.AbpEditions", newSchema: "dbo");
            MoveTable(name: "Abp.AbpLanguages", newSchema: "dbo");
            MoveTable(name: "Abp.AbpLanguageTexts", newSchema: "dbo");
            MoveTable(name: "Abp.AbpNotifications", newSchema: "dbo");
            MoveTable(name: "Abp.AbpNotificationSubscriptions", newSchema: "dbo");
            MoveTable(name: "Abp.AbpOrganizationUnits", newSchema: "dbo");
            MoveTable(name: "Abp.AbpPermissions", newSchema: "dbo");
            MoveTable(name: "Abp.AbpUsers", newSchema: "dbo");
            MoveTable(name: "Abp.AbpUserClaims", newSchema: "dbo");
            MoveTable(name: "Abp.AbpUserLogins", newSchema: "dbo");
            MoveTable(name: "Abp.AbpUserRoles", newSchema: "dbo");
            MoveTable(name: "Abp.AbpSettings", newSchema: "dbo");
            MoveTable(name: "Abp.AbpRoles", newSchema: "dbo");
            MoveTable(name: "Abp.AbpTenantNotifications", newSchema: "dbo");
            MoveTable(name: "Abp.AbpTenants", newSchema: "dbo");
            MoveTable(name: "Abp.AbpUserAccounts", newSchema: "dbo");
            MoveTable(name: "Abp.AbpUserLoginAttempts", newSchema: "dbo");
            MoveTable(name: "Abp.AbpUserNotifications", newSchema: "dbo");
            MoveTable(name: "Abp.AbpUserOrganizationUnits", newSchema: "dbo");
        }
        
        public override void Down()
        {
            MoveTable(name: "dbo.AbpUserOrganizationUnits", newSchema: "Abp");
            MoveTable(name: "dbo.AbpUserNotifications", newSchema: "Abp");
            MoveTable(name: "dbo.AbpUserLoginAttempts", newSchema: "Abp");
            MoveTable(name: "dbo.AbpUserAccounts", newSchema: "Abp");
            MoveTable(name: "dbo.AbpTenants", newSchema: "Abp");
            MoveTable(name: "dbo.AbpTenantNotifications", newSchema: "Abp");
            MoveTable(name: "dbo.AbpRoles", newSchema: "Abp");
            MoveTable(name: "dbo.AbpSettings", newSchema: "Abp");
            MoveTable(name: "dbo.AbpUserRoles", newSchema: "Abp");
            MoveTable(name: "dbo.AbpUserLogins", newSchema: "Abp");
            MoveTable(name: "dbo.AbpUserClaims", newSchema: "Abp");
            MoveTable(name: "dbo.AbpUsers", newSchema: "Abp");
            MoveTable(name: "dbo.AbpPermissions", newSchema: "Abp");
            MoveTable(name: "dbo.AbpOrganizationUnits", newSchema: "Abp");
            MoveTable(name: "dbo.AbpNotificationSubscriptions", newSchema: "Abp");
            MoveTable(name: "dbo.AbpNotifications", newSchema: "Abp");
            MoveTable(name: "dbo.AbpLanguageTexts", newSchema: "Abp");
            MoveTable(name: "dbo.AbpLanguages", newSchema: "Abp");
            MoveTable(name: "dbo.AbpEditions", newSchema: "Abp");
            MoveTable(name: "dbo.AbpFeatures", newSchema: "Abp");
            MoveTable(name: "dbo.AbpBackgroundJobs", newSchema: "Abp");
            MoveTable(name: "dbo.AbpAuditLogs", newSchema: "Abp");
            RenameTable(name: "Abp.AbpUserOrganizationUnits", newName: "UserOrganizationUnits");
            RenameTable(name: "Abp.AbpUserNotifications", newName: "UserNotifications");
            RenameTable(name: "Abp.AbpUserLoginAttempts", newName: "UserLoginAttempts");
            RenameTable(name: "Abp.AbpUserAccounts", newName: "UserAccounts");
            RenameTable(name: "Abp.AbpTenants", newName: "Tenants");
            RenameTable(name: "Abp.AbpTenantNotifications", newName: "TenantNotifications");
            RenameTable(name: "Abp.AbpRoles", newName: "Roles");
            RenameTable(name: "Abp.AbpSettings", newName: "Settings");
            RenameTable(name: "Abp.AbpUserRoles", newName: "UserRoles");
            RenameTable(name: "Abp.AbpUserLogins", newName: "UserLogins");
            RenameTable(name: "Abp.AbpUserClaims", newName: "UserClaims");
            RenameTable(name: "Abp.AbpUsers", newName: "Users");
            RenameTable(name: "Abp.AbpPermissions", newName: "Permissions");
            RenameTable(name: "Abp.AbpOrganizationUnits", newName: "OrganizationUnits");
            RenameTable(name: "Abp.AbpNotificationSubscriptions", newName: "NotificationSubscriptions");
            RenameTable(name: "Abp.AbpNotifications", newName: "Notifications");
            RenameTable(name: "Abp.AbpLanguageTexts", newName: "LanguageTexts");
            RenameTable(name: "Abp.AbpLanguages", newName: "Languages");
            RenameTable(name: "Abp.AbpEditions", newName: "Editions");
            RenameTable(name: "Abp.AbpFeatures", newName: "Features");
            RenameTable(name: "Abp.AbpBackgroundJobs", newName: "BackgroundJobs");
            RenameTable(name: "Abp.AbpAuditLogs", newName: "AuditLogs");
        }
    }
}

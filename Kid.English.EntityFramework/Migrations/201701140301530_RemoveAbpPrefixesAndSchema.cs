namespace Kid.English.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAbpPrefixesAndSchema : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AbpAuditLogs", newName: "AuditLogs");
            RenameTable(name: "dbo.AbpBackgroundJobs", newName: "BackgroundJobs");
            RenameTable(name: "dbo.AbpFeatures", newName: "Features");
            RenameTable(name: "dbo.AbpEditions", newName: "Editions");
            RenameTable(name: "dbo.AbpLanguages", newName: "Languages");
            RenameTable(name: "dbo.AbpLanguageTexts", newName: "LanguageTexts");
            RenameTable(name: "dbo.AbpNotifications", newName: "Notifications");
            RenameTable(name: "dbo.AbpNotificationSubscriptions", newName: "NotificationSubscriptions");
            RenameTable(name: "dbo.AbpOrganizationUnits", newName: "OrganizationUnits");
            RenameTable(name: "dbo.AbpPermissions", newName: "Permissions");
            RenameTable(name: "dbo.AbpUsers", newName: "Users");
            RenameTable(name: "dbo.AbpUserClaims", newName: "UserClaims");
            RenameTable(name: "dbo.AbpUserLogins", newName: "UserLogins");
            RenameTable(name: "dbo.AbpUserRoles", newName: "UserRoles");
            RenameTable(name: "dbo.AbpSettings", newName: "Settings");
            RenameTable(name: "dbo.AbpRoles", newName: "Roles");
            RenameTable(name: "dbo.AbpTenantNotifications", newName: "TenantNotifications");
            RenameTable(name: "dbo.AbpTenants", newName: "Tenants");
            RenameTable(name: "dbo.AbpUserAccounts", newName: "UserAccounts");
            RenameTable(name: "dbo.AbpUserLoginAttempts", newName: "UserLoginAttempts");
            RenameTable(name: "dbo.AbpUserNotifications", newName: "UserNotifications");
            RenameTable(name: "dbo.AbpUserOrganizationUnits", newName: "UserOrganizationUnits");
            MoveTable(name: "dbo.AuditLogs", newSchema: "Abp");
            MoveTable(name: "dbo.BackgroundJobs", newSchema: "Abp");
            MoveTable(name: "dbo.Features", newSchema: "Abp");
            MoveTable(name: "dbo.Editions", newSchema: "Abp");
            MoveTable(name: "dbo.Languages", newSchema: "Abp");
            MoveTable(name: "dbo.LanguageTexts", newSchema: "Abp");
            MoveTable(name: "dbo.Notifications", newSchema: "Abp");
            MoveTable(name: "dbo.NotificationSubscriptions", newSchema: "Abp");
            MoveTable(name: "dbo.OrganizationUnits", newSchema: "Abp");
            MoveTable(name: "dbo.Permissions", newSchema: "Abp");
            MoveTable(name: "dbo.Users", newSchema: "Abp");
            MoveTable(name: "dbo.UserClaims", newSchema: "Abp");
            MoveTable(name: "dbo.UserLogins", newSchema: "Abp");
            MoveTable(name: "dbo.UserRoles", newSchema: "Abp");
            MoveTable(name: "dbo.Settings", newSchema: "Abp");
            MoveTable(name: "dbo.Roles", newSchema: "Abp");
            MoveTable(name: "dbo.TenantNotifications", newSchema: "Abp");
            MoveTable(name: "dbo.Tenants", newSchema: "Abp");
            MoveTable(name: "dbo.UserAccounts", newSchema: "Abp");
            MoveTable(name: "dbo.UserLoginAttempts", newSchema: "Abp");
            MoveTable(name: "dbo.UserNotifications", newSchema: "Abp");
            MoveTable(name: "dbo.UserOrganizationUnits", newSchema: "Abp");
        }
        
        public override void Down()
        {
            MoveTable(name: "Abp.UserOrganizationUnits", newSchema: "dbo");
            MoveTable(name: "Abp.UserNotifications", newSchema: "dbo");
            MoveTable(name: "Abp.UserLoginAttempts", newSchema: "dbo");
            MoveTable(name: "Abp.UserAccounts", newSchema: "dbo");
            MoveTable(name: "Abp.Tenants", newSchema: "dbo");
            MoveTable(name: "Abp.TenantNotifications", newSchema: "dbo");
            MoveTable(name: "Abp.Roles", newSchema: "dbo");
            MoveTable(name: "Abp.Settings", newSchema: "dbo");
            MoveTable(name: "Abp.UserRoles", newSchema: "dbo");
            MoveTable(name: "Abp.UserLogins", newSchema: "dbo");
            MoveTable(name: "Abp.UserClaims", newSchema: "dbo");
            MoveTable(name: "Abp.Users", newSchema: "dbo");
            MoveTable(name: "Abp.Permissions", newSchema: "dbo");
            MoveTable(name: "Abp.OrganizationUnits", newSchema: "dbo");
            MoveTable(name: "Abp.NotificationSubscriptions", newSchema: "dbo");
            MoveTable(name: "Abp.Notifications", newSchema: "dbo");
            MoveTable(name: "Abp.LanguageTexts", newSchema: "dbo");
            MoveTable(name: "Abp.Languages", newSchema: "dbo");
            MoveTable(name: "Abp.Editions", newSchema: "dbo");
            MoveTable(name: "Abp.Features", newSchema: "dbo");
            MoveTable(name: "Abp.BackgroundJobs", newSchema: "dbo");
            MoveTable(name: "Abp.AuditLogs", newSchema: "dbo");
            RenameTable(name: "dbo.UserOrganizationUnits", newName: "AbpUserOrganizationUnits");
            RenameTable(name: "dbo.UserNotifications", newName: "AbpUserNotifications");
            RenameTable(name: "dbo.UserLoginAttempts", newName: "AbpUserLoginAttempts");
            RenameTable(name: "dbo.UserAccounts", newName: "AbpUserAccounts");
            RenameTable(name: "dbo.Tenants", newName: "AbpTenants");
            RenameTable(name: "dbo.TenantNotifications", newName: "AbpTenantNotifications");
            RenameTable(name: "dbo.Roles", newName: "AbpRoles");
            RenameTable(name: "dbo.Settings", newName: "AbpSettings");
            RenameTable(name: "dbo.UserRoles", newName: "AbpUserRoles");
            RenameTable(name: "dbo.UserLogins", newName: "AbpUserLogins");
            RenameTable(name: "dbo.UserClaims", newName: "AbpUserClaims");
            RenameTable(name: "dbo.Users", newName: "AbpUsers");
            RenameTable(name: "dbo.Permissions", newName: "AbpPermissions");
            RenameTable(name: "dbo.OrganizationUnits", newName: "AbpOrganizationUnits");
            RenameTable(name: "dbo.NotificationSubscriptions", newName: "AbpNotificationSubscriptions");
            RenameTable(name: "dbo.Notifications", newName: "AbpNotifications");
            RenameTable(name: "dbo.LanguageTexts", newName: "AbpLanguageTexts");
            RenameTable(name: "dbo.Languages", newName: "AbpLanguages");
            RenameTable(name: "dbo.Editions", newName: "AbpEditions");
            RenameTable(name: "dbo.Features", newName: "AbpFeatures");
            RenameTable(name: "dbo.BackgroundJobs", newName: "AbpBackgroundJobs");
            RenameTable(name: "dbo.AuditLogs", newName: "AbpAuditLogs");
        }
    }
}

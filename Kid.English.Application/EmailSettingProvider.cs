using System.Collections.Generic;
using Abp;
using Abp.Configuration;
using Abp.Localization;
using Abp.Net.Mail;
using Abp.Zero.Configuration;

namespace Kid.English
{
    internal class EmailSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(EmailSettingNames.Smtp.Host, "smtp.qq.com"),
                new SettingDefinition(EmailSettingNames.Smtp.Port, "25"),
                new SettingDefinition(EmailSettingNames.DefaultFromAddress, "66970551@qq.com"),
                new SettingDefinition(EmailSettingNames.Smtp.UserName, "66970551@qq.com"),
                new SettingDefinition(EmailSettingNames.Smtp.Password, "*******"),
                new SettingDefinition(EmailSettingNames.Smtp.UseDefaultCredentials, "false"),
                new SettingDefinition(EmailSettingNames.DefaultFromDisplayName, "kid"),
                new SettingDefinition(EmailSettingNames.Smtp.EnableSsl, "true"),
                new SettingDefinition(
                    AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin,
                    "true",
                    new FixedLocalizableString("Is email confirmation required for login."),
                    scopes: SettingScopes.Application | SettingScopes.Tenant
                )
            };
        }

        private static LocalizableString L(string name)
        {
            return new LocalizableString(name, AbpConsts.LocalizationSourceName);
        }
    }
}
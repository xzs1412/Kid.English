using System.Reflection;
using Abp.Localization;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Zero;
using Abp.Zero.Configuration;
using Kid.English.Authorization;
using Kid.English.Authorization.Roles;
using Kid.English.MultiTenancy;
using Kid.English.Users;


namespace Kid.English
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class EnglishCoreModule : AbpModule
    {
        /// <summary>
        /// 1.启用未登录用户的审计
        /// 2.把当前项目(Core)里的Uaser,Role,Tenant类型赋给Module Zero的EntityTypes
        /// //3.是否启用多租户
        /// 4.添加本地化源
        /// 5.用Module Zero的角色管理作为本应用的角色管理
        /// 6.把EnglishAuthorizationProvider类型添加到配置的授权供应器的类型列表(TypeList)中
        /// </summary>
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            //Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            //Remove the following line to disable multi-tenancy.
            //Configuration.MultiTenancy.IsEnabled = true;

           // 1010    NULL en  English famfamfam-flag - gb   False NULL    NULL NULL    NULL    2016 - 11 - 13 21:11:19.967 NULL
//1012    NULL zh-CN   简体中文 famfamfam-flag - cn   False NULL    NULL NULL    NULL    2016 - 11 - 13 21:11:19.967 NULL
            Configuration.Localization.Languages.Add(new LanguageInfo("en","tuerqi","famfamfam-flag-tr")); 

            //Add/remove localization sources here
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    EnglishConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "Kid.English.Localization.Source"
                        )
                    )
                );

            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Authorization.Providers.Add<EnglishAuthorizationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}

using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Hangfire;
using Abp.Hangfire.Configuration;
using Abp.Zero.Configuration;
using Abp.Modules;
using Abp.Web.Mvc;
using Abp.Web.SignalR;
using Kid.English.Api;
using Hangfire;
using Abp.Web;
using Abp.Configuration.Startup;
using Abp.Web.Configuration;


namespace Kid.English.Web
{
    [DependsOn(
        typeof(EnglishDataModule),
        typeof(EnglishApplicationModule),
        typeof(EnglishWebApiModule),
         typeof(AbpWebMvcModule),
        typeof(AbpWebSignalRModule),
        typeof(AbpHangfireModule) //- ENABLE TO USE HANGFIRE INSTEAD OF DEFAULT JOB MANAGER
       )]
    public class EnglishWebModule : AbpModule
    {
        /// <summary>
        /// 1.语言管理:允许数据库里的本地化
        /// 2.添加导航供应器
        /// </summary>
        public override void PreInitialize()
        {
            //Enable database based localization, 
            //Replaces all Abp.Localization.Dictionaries.IDictionaryBasedLocalizationSource
            //localization sources with database based localization source.
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            //Configuration.Modules.AbpWebCommon().SendAllExceptionsToClients = true;
           
            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<EnglishNavigationProvider>();

            //Configure Hangfire -ENABLE TO USE HANGFIRE INSTEAD OF DEFAULT JOB MANAGER
            Configuration.BackgroundJobs.UseHangfire(configuration =>
            {
                configuration.GlobalConfiguration.UseSqlServerStorage("Default");
            });
        }

        /// <summary>
        /// 1.根据IocManager中的所有约定注册器,按约定注册当前程序集
        /// 2.注册所有Areas
        /// 3.注册路由
        /// 4.注册Bundle
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public override void PostInitialize()
        {
            
        }
    }
}

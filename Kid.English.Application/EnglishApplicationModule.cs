using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;


namespace Kid.English
{
    [DependsOn(typeof(EnglishCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class EnglishApplicationModule : AbpModule
    {
        /// <summary>
        /// Do nothing by default
        /// </summary>
        public override void PreInitialize()
        {
            //disabled background job
            //Configuration.BackgroundJobs.IsJobExecutionEnabled = false;


            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                //Add your custom AutoMapper mappings here...
                //mapper.CreateMap<,>()
            });
         
        }

        /// <summary>
        /// 1.根据IocManager中的所有约定注册器,按约定注册当前程序集
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //kid:Add email settings to the configuration
            Configuration.Settings.Providers.Add<EmailSettingProvider>();
        }


        public override void PostInitialize()
        {
         
        }
 
    }
}

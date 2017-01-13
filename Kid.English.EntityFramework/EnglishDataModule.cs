using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using Kid.English.EntityFramework;

namespace Kid.English
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(EnglishCoreModule))]
    public class EnglishDataModule : AbpModule
    {
        /// <summary>
        /// 1.如果数据库不存在则创建
        /// 2.默认的连接字符串名为"Default"
        /// </summary>
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<EnglishDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        /// <summary>
        /// 1.根据IocManager中的所有约定注册器,按约定注册当前程序集
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

        }
    }
}

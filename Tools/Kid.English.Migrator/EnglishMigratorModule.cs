using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Kid.English.EntityFramework;

namespace Kid.English.Migrator
{
    [DependsOn(typeof(EnglishDataModule))]
    public class EnglishMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<EnglishDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
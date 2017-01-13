using System.Reflection;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using Swashbuckle.Application;
using System.Linq;

namespace Kid.English.Api
{
    [DependsOn(typeof(AbpWebApiModule), typeof(EnglishApplicationModule))]
    public class EnglishWebApiModule : AbpModule
    {
        /// <summary>
        /// 1.根据IocManager中的所有约定注册器,按约定注册当前程序集
        /// 2.添加动态Api控制器建造器
        /// 3.添加Bearer宿主授权过滤器
        /// 4.注册Swagger
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(EnglishApplicationModule).Assembly, "app")
                .Build();

            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));

            //SwaggerConfig使用了自注册[assembly: PreApplicationStartMethod(typeof(SwaggerConfig),  "Register")]
            //SwaggerConfig.Register();
        }

        //private void ConfigureSwaggerUi()
        //{
        //    Configuration.Modules.AbpWebApi().HttpConfiguration
        //        .EnableSwagger(c =>
        //        {
        //            c.SingleApiVersion("v1", "English.WebApi");
        //            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        //        })
        //        .EnableSwaggerUi(c =>
        //        {
        //            c.InjectJavaScript(Assembly.GetAssembly(typeof(EnglishWebApiModule)), "Kid.English.Api.Scripts.Swagger-Custom.js");
        //        });
        //}
    }
}

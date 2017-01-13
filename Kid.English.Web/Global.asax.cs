using System;
using Abp.Castle.Logging.Log4Net;
using Abp.Web;
using Castle.Facilities.Logging;

namespace Kid.English.Web
{
    /// <summary>
    /// EnglishWebModule作为启动模块
    /// </summary>
    public class MvcApplication : AbpWebApplication<EnglishWebModule>
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            // 一定要先调用这行，再调用基类的Start方法，因为在基类的Start方法里会解析这个日志记录器。
             AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(
                f => f.UseAbpLog4Net().WithConfig("log4net.config")
            );

            //会在这个基类里调用AbpBootstrapper.Initialize（另外，基类还有一个Application_BeginRequest方法，设置当前文化）
            base.Application_Start(sender, e);
        }
    }
}

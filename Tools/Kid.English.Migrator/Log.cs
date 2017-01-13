using System;
using Abp.Dependency;
using Abp.Timing;
using Castle.Core.Logging;

namespace Kid.English.Migrator
{
    public class Log : ITransientDependency
    {      
        // This is Castle.Core's ILogger,not log4net's
        public ILogger Logger { get; set; }

        public Log()
        {
            Logger = NullLogger.Instance;
        }

        public void Write(string text)
        {
            Console.WriteLine(Clock.Now.ToString("yyyy-MM-dd HH:mm:ss") + " | " + text);
            Logger.Info(text);
        }
    }
}
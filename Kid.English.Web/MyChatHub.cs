using Abp.Runtime.Session;
using Castle.Core.Logging;

namespace Kid.English.Web
{
    public class MyChatHub
    {
        public IAbpSession AbpSession { get; set; }
        public ILogger Logger { get; set; }

        public MyChatHub()
        {
            AbpSession = NullAbpSession.Instance;
            Logger = NullLogger.Instance;
        }

        public void SendMessage(string message)
        {
            //Clients.All.getMessage($"User {AbpSession.UserId}: {message}");
        }
    }
}
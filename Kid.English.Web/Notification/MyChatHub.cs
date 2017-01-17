using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Runtime.Session;
using Castle.Core.Logging;
using Kid.English.Users;
using Microsoft.AspNet.SignalR;

namespace Kid.English.Web.Notification
{
    public class MyChatHub:Hub,ITransientDependency
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
            Clients.All.getMessage($"User {AbpSession.UserId}: {message}");
        }

        public override async Task OnConnected()
        {
            await base.OnConnected();
            Clients.All.getMessage($"A client connected from MyChatHub:{Context.ConnectionId} and UserId:{AbpSession.UserId}");
            Logger.Debug($"A client connected from MyChatHub:{Context.ConnectionId} and UserId:{AbpSession.UserId}");
        }

        public override async Task OnDisconnected(bool stopCalled)
        {
            await base.OnDisconnected(stopCalled);
            Clients.All.getMessage($"A client disconnected from MyChatHub:{Context.ConnectionId} and UserId:{AbpSession.UserId}");
            Logger.Debug($"A client disconnected from MyChatHub:{Context.ConnectionId} and UserId:{AbpSession.UserId}");
        }
    }
}
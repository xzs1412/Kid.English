using System;
using System.Threading.Tasks;
using Abp;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Notifications;
using Kid.English.Phrases;
using Microsoft.AspNet.Identity;

namespace Kid.English.Notification
{
    public class SubscribeService:ITransientDependency
    {
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;

        public SubscribeService(INotificationSubscriptionManager notificationSubscriptionManager)
        {
            _notificationSubscriptionManager = notificationSubscriptionManager;
        }

        //Subscribe to a general notification
        public async Task Subscribe_SentFrendshipRequest(int? tenantId, long userId)
        {
            await _notificationSubscriptionManager.SubscribeAsync(new UserIdentifier(tenantId,userId),"SentFrndshipRequest");
        }

        //Subscribe to an entity notification
        public async Task Subscribe_PhraseUpdate(int? tenantId, long userId, Guid phraseId)
        {
            await
                _notificationSubscriptionManager.SubscribeAsync(new UserIdentifier(tenantId, userId), "UpdatePhrase",
                    new EntityIdentifier(typeof(Phrase), phraseId));
        }
    }
}
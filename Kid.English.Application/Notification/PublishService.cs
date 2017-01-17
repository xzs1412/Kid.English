using System;
using System.Threading.Tasks;
using Abp;
using Abp.Authorization;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Localization;
using Abp.Notifications;
using Abp.UI;
using Castle.Windsor.Configuration.Interpreters.XmlProcessor;
using Kid.English.Phrases;

namespace Kid.English.Notification
{
    [AbpAuthorize]
    public class PublishService: EnglishAppServiceBase,IPublishService
    {
        private readonly INotificationPublisher _notificationPublisher;

        public PublishService(INotificationPublisher notificationPublisher)
        {
            _notificationPublisher = notificationPublisher;
        }

        //Send a general notification to a specific user
        public async Task Publish_SentFrendshipRequest(string senderUserName, string friendshipMessage,
            UserIdentifier targerUserId)
        {
            await
                _notificationPublisher.PublishAsync("SentFrendshipRequest",
                    new SentFrendshipRequestNotificationData(senderUserName, friendshipMessage),
                    userIds: new[] {targerUserId});
        }

        //Send an entity notification to a pecific user
        public async Task Publish_UpdatePhrase(string updaterUserName, string englishSentence, Guid phraseId,
            UserIdentifier phraseOwnerUserId)
        {
            await
                _notificationPublisher.PublishAsync("UpdatePhrase",
                    new UpdatePhraseNotificationData(updaterUserName, englishSentence),
                    new EntityIdentifier(typeof(Phrase), phraseId), userIds: new[] {phraseOwnerUserId});
        }

        //Send a general notification to all subscribed users in current tenant(tenant in the session)
        public async Task Publish_LowDisk(int remainingDiskInMb)
        {
            //Example "LowDiskWarningMessage" content for English -> "Attention! only {remainingDiskInMb} MBs left on the disk!"
            //var data = new MessageNotificationData("LowDiskWarningMessage")
            //{
            //    ["remainingDiskInMb"] = remainingDiskInMb
            //};

            await _notificationPublisher.PublishAsync("System.LowDisk");//, data, severity: NotificationSeverity.Warn);
        }
        public async Task KidPublishMethod(string targetUserName)
        {
            var targetUser = await UserManager.FindByNameAsync(targetUserName);
            if (targetUser == null)
            {
                throw new UserFriendlyException("There is no such a user: " + targetUserName);
            }

            var currentUser = await GetCurrentUserAsync();


            var notificationData = new MessageNotificationData(
                $"{currentUser.UserName} sent you an email with subject: "
            );

            await
                _notificationPublisher.PublishAsync("App.KidPublishMethod", notificationData,userIds: new[] { new UserIdentifier(1,targetUser.Id )});
            //await _notificationPublisher.PublishAsync("Kid.PublishMethod");
        }
    }

    [Serializable]
    public class UpdatePhraseNotificationData : NotificationData
    {
        public string UpdaterUsername { get; set; }
        public string EnglishSentence { get; set; }

        public UpdatePhraseNotificationData(string updaterUserName, string englishSentence)
        {
            UpdaterUsername = updaterUserName;
            EnglishSentence = englishSentence;
        }
    }

    [Serializable]
    public class SentFrendshipRequestNotificationData : NotificationData
    {
        public string SenderUserName { get; set; }
        public string FriendshipMessage { get; set; }

        public SentFrendshipRequestNotificationData(string senderUserName, string friendshipMessage)
        {
            SenderUserName = senderUserName;
            FriendshipMessage = friendshipMessage;
        }
    }
}
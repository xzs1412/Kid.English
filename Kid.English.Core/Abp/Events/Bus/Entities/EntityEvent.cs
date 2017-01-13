using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Kid.English.Phrases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Net.Mail.Smtp;

namespace Kid.English.Abp.Events.Bus.Entities
{
    public class EntityEvent : IEventHandler<EntityDeletedEventData<Entity<Guid>>>,
         ITransientDependency
    {
        public void HandleEvent(EntityDeletedEventData<Entity<Guid>> eventData)
        {
           
            var emailSender = IocManager.Instance.Resolve<ISmtpEmailSender>();
            emailSender.Send("66970551@qq.com", "Message from Kid.English - Delete",$"There's an entity deleted, the entity information:{Environment.NewLine}{eventData.Entity.ToString()}", false);
        }

        
    }
}


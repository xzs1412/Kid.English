using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Kid.English.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kid.English
{
    /// <summary>
    /// My Entity's Base class which implements IEntity&lt;Guid&gt; and IFullAudited&lt;User&gt;
    /// </summary>
    public class EnglishEntityBase : Entity<Guid>, IFullAudited<User>
    {
        public long? CreatorUserId
        {
            get;
            set;
        }

        public DateTime CreationTime
        {
            get;
            set;
        }

        public long? LastModifierUserId
        {
            get;
            set;
        }

        public DateTime? LastModificationTime
        {
            get;
            set;
        }

        public User CreatorUser
        {
            get;
            set;
        }

        public User LastModifierUser
        {
            get;
            set;
        }

        public long? DeleterUserId
        {
            get;
            set;
        }

        public DateTime? DeletionTime
        {
            get;
            set;
        }

        public bool IsDeleted
        {
            get;
            set;
        }

        public User DeleterUser
        {
            get;
            set;
        }

 
    }
}

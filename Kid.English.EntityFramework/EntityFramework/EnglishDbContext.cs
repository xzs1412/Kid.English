using System.Data.Common;
using Abp.Zero.EntityFramework;
using Kid.English.Authorization.Roles;
using Kid.English.MultiTenancy;
using Kid.English.Users;
using Kid.English.Phrases;
using System.Data.Entity;

namespace Kid.English.EntityFramework
{
    public class EnglishDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...
        public virtual IDbSet<Phrase> Phrases { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public EnglishDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in EnglishDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of EnglishDbContext since ABP automatically handles it.
         */
        public EnglishDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

            //this.Database.Log = l => Logger.Debug(l);
        }

        //This constructor is used in tests
        public EnglishDbContext(DbConnection connection)
            : base(connection, true)
        {

            //this.Database.Log = l => Logger.Debug(l);
        }
    }
}

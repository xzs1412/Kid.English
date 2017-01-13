using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace Kid.English.EntityFramework.Repositories
{
    public abstract class EnglishRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<EnglishDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected EnglishRepositoryBase(IDbContextProvider<EnglishDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        /// <summary>
        /// ???这个是怎么来的,GitHub上下载的示例没有.
        /// Error	1	'Abp.EntityFramework.Repositories.EfRepositoryBase<Kid.English.EntityFramework.EnglishDbContext,TEntity,TPrimaryKey>' does not contain a constructor that takes 0 arguments	C:\Users\kid\Work\TFS\English\Kid.English.EntityFramework\EntityFramework\Repositories\EnglishRepositoryBase.cs	19	16	Kid.English.EntityFramework
        /// </summary>
        //public EnglishRepositoryBase()
        //{
        //    // TODO: Complete member initialization
        //}

        //add common methods for all repositories
    }

    public abstract class EnglishRepositoryBase<TEntity> : EnglishRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected EnglishRepositoryBase(IDbContextProvider<EnglishDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}

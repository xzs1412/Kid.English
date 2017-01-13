using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Domain.Repositories;

namespace Kid.English.Editions
{
    public class EditionManager : AbpEditionManager
    {
        public const string DefaultEditionName = "Standard";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="editionRepository"></param>
        /// <param name="featureValueStore"></param>
        public EditionManager(
            IRepository<Edition> editionRepository, 
            IAbpZeroFeatureValueStore featureValueStore)
            : base(
                editionRepository,
                featureValueStore
            )
        {
        }
    }
}

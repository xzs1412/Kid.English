using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using Kid.English.Editions;
using Kid.English.Users;

namespace Kid.English.MultiTenancy
{
    /// <summary>
    /// 多用户管理器
    /// </summary>
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore
            ) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore
            )
        {
        }
    }
}
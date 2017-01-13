using System.Linq;
using Kid.English.EntityFramework;
using Kid.English.MultiTenancy;

namespace Kid.English.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly EnglishDbContext _context;

        public DefaultTenantCreator(EnglishDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}

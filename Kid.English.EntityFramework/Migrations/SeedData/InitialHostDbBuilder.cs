using Kid.English.EntityFramework;
using EntityFramework.DynamicFilters;

namespace Kid.English.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly EnglishDbContext _context;

        public InitialHostDbBuilder(EnglishDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}

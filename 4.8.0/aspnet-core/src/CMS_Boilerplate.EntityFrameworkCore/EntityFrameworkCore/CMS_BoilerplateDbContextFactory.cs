using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using CMS_Boilerplate.Configuration;
using CMS_Boilerplate.Web;

namespace CMS_Boilerplate.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class CMS_BoilerplateDbContextFactory : IDesignTimeDbContextFactory<CMS_BoilerplateDbContext>
    {
        public CMS_BoilerplateDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CMS_BoilerplateDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            CMS_BoilerplateDbContextConfigurer.Configure(builder, configuration.GetConnectionString(CMS_BoilerplateConsts.ConnectionStringName));

            return new CMS_BoilerplateDbContext(builder.Options);
        }
    }
}

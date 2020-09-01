using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace CMS_Boilerplate.EntityFrameworkCore
{
    public static class CMS_BoilerplateDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<CMS_BoilerplateDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<CMS_BoilerplateDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using CMS_Boilerplate.Authorization.Roles;
using CMS_Boilerplate.Authorization.Users;
using CMS_Boilerplate.MultiTenancy;
using CMS_Boilerplate.CustomPages;

namespace CMS_Boilerplate.EntityFrameworkCore
{
    public class CMS_BoilerplateDbContext : AbpZeroDbContext<Tenant, Role, User, CMS_BoilerplateDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<CustomPage> CustomPages { get; set; }
        public CMS_BoilerplateDbContext(DbContextOptions<CMS_BoilerplateDbContext> options)
            : base(options)
        {
        }
    }
}

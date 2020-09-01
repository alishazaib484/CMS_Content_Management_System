using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CMS_Boilerplate.Authorization;

namespace CMS_Boilerplate
{
    [DependsOn(
        typeof(CMS_BoilerplateCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class CMS_BoilerplateApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<CMS_BoilerplateAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(CMS_BoilerplateApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}

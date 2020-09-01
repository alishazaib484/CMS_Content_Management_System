using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CMS_Boilerplate.Configuration;

namespace CMS_Boilerplate.Web.Host.Startup
{
    [DependsOn(
       typeof(CMS_BoilerplateWebCoreModule))]
    public class CMS_BoilerplateWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public CMS_BoilerplateWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CMS_BoilerplateWebHostModule).GetAssembly());
        }
    }
}

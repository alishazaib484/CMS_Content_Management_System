using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace CMS_Boilerplate.Controllers
{
    public abstract class CMS_BoilerplateControllerBase: AbpController
    {
        protected CMS_BoilerplateControllerBase()
        {
            LocalizationSourceName = CMS_BoilerplateConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}

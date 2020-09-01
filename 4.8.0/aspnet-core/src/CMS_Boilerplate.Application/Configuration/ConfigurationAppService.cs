using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using CMS_Boilerplate.Configuration.Dto;

namespace CMS_Boilerplate.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : CMS_BoilerplateAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}

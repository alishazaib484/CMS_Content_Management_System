using System.Threading.Tasks;
using CMS_Boilerplate.Configuration.Dto;

namespace CMS_Boilerplate.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}

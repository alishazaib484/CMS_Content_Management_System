using System.Threading.Tasks;
using Abp.Application.Services;
using CMS_Boilerplate.Sessions.Dto;

namespace CMS_Boilerplate.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}

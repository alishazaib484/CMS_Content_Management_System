using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CMS_Boilerplate.MultiTenancy.Dto;

namespace CMS_Boilerplate.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}


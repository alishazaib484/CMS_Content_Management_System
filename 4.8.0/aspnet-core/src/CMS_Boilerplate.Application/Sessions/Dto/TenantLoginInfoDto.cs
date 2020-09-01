using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CMS_Boilerplate.MultiTenancy;

namespace CMS_Boilerplate.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}

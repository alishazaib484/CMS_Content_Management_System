using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CMS_Boilerplate.CustomPages.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Boilerplate.CustomPages
{
    public interface ICustomPageAppService: IApplicationService
    {
       
        Task<CustomPageDto> GetCMSContent(int pageId);
        Task<string> DeleteCMSContent(int pageId);
        Task<ListResultDto<GetAllPagesOutputDto>> GetAll();
        Task<string> InsertOrUpdateCMSContent(CustomPageDto input);
    }
}

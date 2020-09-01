using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Json;
using AutoMapper;
using CMS_Boilerplate.Authorization;
using CMS_Boilerplate.CustomPages.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Boilerplate.CustomPages
{
    public class CustomPageAppService : CMS_BoilerplateAppServiceBase, ICustomPageAppService
    {
        public const string pageUpdateMessage  = "Page Updated Sccessfully";
        public const string pageInsertMessage = "Page Added Sccessfully";
        private readonly IRepository<CustomPage> _CustomPageRepository;
        
        public CustomPageAppService(IRepository<CustomPage> customPageRepository)
        {
            _CustomPageRepository = customPageRepository;           
        }

        //return list of pages(titles and ids), does not return page data
        //call GetCMSContent to fetch particular page data
        public async Task<ListResultDto<GetAllPagesOutputDto>> GetAll()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CustomPage, GetAllPagesOutputDto>());
            var mapper = config.CreateMapper();

            var Pages = await _CustomPageRepository.GetAllListAsync();
            var outputPagesList = mapper.Map<List<GetAllPagesOutputDto>>(Pages);
            return new ListResultDto<GetAllPagesOutputDto>(outputPagesList);
        }


        // retrieve page data if available otherwise page not found exception 404 
        public async Task<CustomPageDto> GetCMSContent(int pageId)
        {
            var page = await _CustomPageRepository.FirstOrDefaultAsync(pageId);

            if (page == null)
            {
                throw new EntityNotFoundException(typeof(CustomPage), pageId);
            }

            return MapToDto(page);
          
        }

        // Map CustomPage entity to CustomPage Dto 
        // AutoMapper can also be used instead of this
        private CustomPageDto MapToDto(CustomPage page)
        {
            CustomPageDto customPageDto = new CustomPageDto();
            customPageDto.Id = page.Id;
            customPageDto.Title = page.Title;
            customPageDto.Content = page.Content;

            return customPageDto;
        }

        //add of update page and return its ID we need id of newly created page in updation call,otherwise we will 
        public async Task<string> InsertOrUpdateCMSContent(CustomPageDto input)
        {

            if (input.Id > 0) // updation case, check if page exist first
            {
                var pageExists = await _CustomPageRepository.GetAll().AnyAsync(c => c.Id == input.Id);
                if (!pageExists)
                {
                    throw new EntityNotFoundException(typeof(CustomPage), input.Id);
                }
            }

           var TitleExisits = await _CustomPageRepository.GetAll().AnyAsync(c => c.Title == input.Title && c.Id != input.Id);
           if (TitleExisits)
           {
                return "Page with this title already exist."; 
           }
           

            var config = new MapperConfiguration(cfg => cfg.CreateMap<CustomPageDto, CustomPage>());
            var mapper = config.CreateMapper();
            var page = mapper.Map<CustomPage>(input);

            var pageId=await _CustomPageRepository.InsertOrUpdateAndGetIdAsync(page);
         
            return pageId.ToString(); 
 
        }

        [AbpAuthorize(PermissionNames.Pages_DeletePage)]
        public async Task<string> DeleteCMSContent(int pageId)
        {
            
            var exists = await _CustomPageRepository.GetAll().AnyAsync(c => c.Id == pageId);
            if (!exists) //if page doesn't exist throw an exception
            {
                throw new EntityNotFoundException(typeof(CustomPage), pageId);
            }
            else
            {
                await _CustomPageRepository.DeleteAsync(pageId);
                return "Page Deleted Successfully";
            }
            

        }
    }
}

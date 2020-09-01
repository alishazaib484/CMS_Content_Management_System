using Abp.Application.Services.Dto;
using CMS_Boilerplate.CustomPages;
using CMS_Boilerplate.CustomPages.Dto;
using CMS_Boilerplate.Users.Dto;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CMS_Boilerplate.Tests.CustomPages
{
    public class CustomPageAppService_Test : CMS_BoilerplateTestBase
    {

        private readonly ICustomPageAppService _customPageAppService;

        public CustomPageAppService_Test()
        {
            _customPageAppService = Resolve<ICustomPageAppService>();
        }

       
        [Fact]
        public async Task InsertOrUpdateCMSContent_Test()
        {
            var id = 0;
            //Testing create
            await _customPageAppService.InsertOrUpdateCMSContent(
                new CustomPageDto
                {
                    Title = "Test123",
                    Content = "",
                    Id = 0
                });

            await UsingDbContextAsync(async context =>
            {
                var page = await context.CustomPages.FirstOrDefaultAsync(x => x.Title == "Test123");
                page.ShouldNotBeNull();
                id = page.Id;
            });

            //Testing update
            var existingPage = await _customPageAppService.GetCMSContent(id);
            existingPage.Title = "TitleUpdated";
            await _customPageAppService.InsertOrUpdateCMSContent(existingPage);

            await UsingDbContextAsync(async context =>
            {
                var page = await context.CustomPages.FirstOrDefaultAsync(x => x.Title == "TitleUpdated");
                page.ShouldNotBeNull();
                id = page.Id;
            });

        }

        [Fact]
        public async Task GetAllPages_Test()
        {
           
            await _customPageAppService.InsertOrUpdateCMSContent(
               new CustomPageDto
               {
                   Title = "TestAllPages",
                   Content = "",
                   Id = 0
               });
            // Act
            var output = await _customPageAppService.GetAll();

            // Assert
            output.Items.Count.ShouldBeGreaterThan(0);
        }


        [Fact]
        public async Task GetCMSContent_Test()
        {
               await _customPageAppService.InsertOrUpdateCMSContent(
               new CustomPageDto
               {
                   Title = "TestGetCMSContent",
                   Content = "",
                   Id = 0
               });

            // Act
            var output = await _customPageAppService.GetCMSContent(1);

            // Assert
            output.ShouldNotBeNull();
        }

        [Fact]
        public async Task DeleteCMSContent_Test()
        {
            var pageId=await _customPageAppService.InsertOrUpdateCMSContent(
            new CustomPageDto
            {
                Title = "TestGetCMSContent",
                Content = "",
                Id = 0
            });

            // Act
            var output = await _customPageAppService.DeleteCMSContent(Int32.Parse(pageId));

            // Assert
            output.ShouldBe("Page Deleted Successfully");
        }


    }
}

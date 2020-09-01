using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS_Boilerplate.CustomPages.Dto
{
    [AutoMap(typeof(CustomPage))]
    public class CustomPageDto : EntityDto<int>
    {
        public const int MaxPageTitleLength = 50;
        public const int MaxPageContentLength = 5000;


        [Required]
        [StringLength(MaxPageTitleLength)]
        public string Title { get; set; }

        [StringLength(MaxPageContentLength)]
        public string Content { get; set; }

  
    }
}

using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS_Boilerplate.CustomPages.Dto
{
    [AutoMap(typeof(CustomPage))]
    public class GetAllPagesOutputDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}

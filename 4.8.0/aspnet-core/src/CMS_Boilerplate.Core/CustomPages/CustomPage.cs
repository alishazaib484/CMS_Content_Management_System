using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS_Boilerplate.CustomPages
{
    public class CustomPage : FullAuditedEntity<int>
    {
        public const int MaxPageTitleLength = 50;
        public const int MaxPageContentLength = 5000;


        [Required]
        [StringLength(MaxPageTitleLength)]
        public string Title { get; set; }

        [StringLength(MaxPageContentLength)]
        public string Content { get; set; }

        public int? TenantId { get; set; }
        
    }
}

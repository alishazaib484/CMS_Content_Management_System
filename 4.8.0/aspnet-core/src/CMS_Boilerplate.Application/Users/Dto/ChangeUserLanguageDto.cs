using System.ComponentModel.DataAnnotations;

namespace CMS_Boilerplate.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
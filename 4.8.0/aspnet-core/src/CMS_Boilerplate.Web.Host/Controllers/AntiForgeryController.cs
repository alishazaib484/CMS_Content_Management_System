using Microsoft.AspNetCore.Antiforgery;
using CMS_Boilerplate.Controllers;

namespace CMS_Boilerplate.Web.Host.Controllers
{
    public class AntiForgeryController : CMS_BoilerplateControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}

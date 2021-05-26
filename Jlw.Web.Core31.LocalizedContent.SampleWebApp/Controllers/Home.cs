using Microsoft.AspNetCore.Mvc;

namespace Jlw.Web.Core31.LocalizedContent.SampleWebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}
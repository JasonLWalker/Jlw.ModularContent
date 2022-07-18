using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Jlw.Web.LocalizedContent.SampleWebApp.Controllers
{
    [Route("~/SampleWizard/[controller]")]
    public class HomeController : Controller
    {
        [HttpGet("~/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [HttpPost]
        [Route("Wizard")]
        public virtual IActionResult Wizard()
        {
            var wizardData = new object();

            return View(wizardData);
        }

    }
}
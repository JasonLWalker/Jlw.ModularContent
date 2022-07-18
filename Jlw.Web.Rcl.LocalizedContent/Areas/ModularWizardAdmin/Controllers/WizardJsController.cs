using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Jlw.Web.Rcl.LocalizedContent.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace Jlw.Web.Rcl.LocalizedContent.Areas.ModularWizardAdmin.Controllers
{
    public class WizardJsController : Controller
    {
        protected IRazorViewEngine _razorViewEngine;
        public WizardJsController(IRazorViewEngine razorEngine)
        {
            _razorViewEngine = razorEngine;
        }

        [AllowAnonymous]
        [HttpGet("~/LocalizedContent/js/wizardLibScript.js")]
        public virtual IActionResult WizardLibScript()
        {
            // Create ActionContext to use with ViewContext
            var ctx = new ActionContext(HttpContext, ControllerContext.RouteData, ControllerContext.ActionDescriptor);
            // Retrieve View template from assembly
            var view = _razorViewEngine.FindView(ctx, "_WizardLibScript.js", true);
            // If view is not found, return 404
            if (view.View is null)
                return new NotFoundResult();

            // Initialize StringBuilder to process view data
            var sb = new StringBuilder();
            // Initialize StringWriter with StringBuilder
            var writer = new StringWriter(sb);

            // Initialize ViewContext with writer for processing
            var viewContext = new ViewContext(ctx, view.View, ViewData, TempData, writer, new HtmlHelperOptions());
            
            // Render view, and write to sb.
            viewContext.View.RenderAsync(viewContext).Wait();

            // Initialize regular expression
            var regEx = new Regex("<[/]?script[^>]*>", RegexOptions.IgnoreCase);

            // Replace script tags in rendered string and output as javascript file
            return new ContentResult()
            {
                ContentType = "application/javascript",
                Content = regEx.Replace(sb.ToString(), "")
            };
        }
    }
}

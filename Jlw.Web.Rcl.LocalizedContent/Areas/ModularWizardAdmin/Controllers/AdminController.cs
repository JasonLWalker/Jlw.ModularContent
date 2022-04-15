using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;

namespace Jlw.Web.Rcl.LocalizedContent.Areas.ModularWizardAdmin.Controllers
{
    public abstract class AdminController : Controller
    {
        protected WizardAdminSettings DefaultSettings { get; } = new WizardAdminSettings();


        /// <summary>Default route for admin</summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        // GET: Default 
        public virtual ActionResult Index()
        {
            //return View("Index");
            return GetViewResult("Index");
        }

        [HttpGet("Preview")]
        public virtual ActionResult Preview()
        {
            return GetViewResult("Preview");
        }



        /// <summary>Gets the view result.</summary>
        /// <param name="viewName">Name of the view</param>
        /// <param name="settings">Settings to customize the view</param>
        /// <returns>ActionResult.</returns>
        [NonAction]
        public ActionResult GetViewResult(string viewName = null, WizardAdminSettings settings = null)
        {
            return View(GetViewPath(viewName), settings ?? DefaultSettings);
        }

        [NonAction]
        public string GetViewPath(string viewName)
        {
            return $"~/Areas/ModularWizardAdmin/Views/Admin/{viewName ?? "Index"}.cshtml";
        }

        public class WizardAdminSettings
        {
            public bool IsAdmin { get; set; }
            public bool CanEdit { get; set; }
            public bool CanDelete { get; set; }
            public bool CanInsert { get; set; }

            public bool UseWysiwyg { get; set; } = true;
            public bool ShowSideNav { get; set; } = true;
            public bool ShowWireFrame { get; set; } = true;
            public bool SideNavDefault { get; set; } = true;
            public bool WireFrameDefault { get; set; } = false;

            public JToken TinyMceSettings { get; set; }
            public string PageTitle { get; set; }
            public string ExtraCss { get; set; }
            public string ExtraScript { get; set; }
            public string Area { get; set; }
            public string AdminUrl { get; set; }
            public string ApiOverrideUrl { get; set; }
            public string JsRoot { get; set; }

            public string ToolboxHeight { get; set; }

            public List<SelectListItem> LanguageList { get; } = new List<SelectListItem>() { new SelectListItem("English", "EN") };

        }


    }
}

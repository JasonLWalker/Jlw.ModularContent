using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.Web.Rcl.LocalizedContent.Areas.ModularWizardAdmin.Controllers
{
    public abstract class AdminController : Controller
    {
        /// <summary>Default route for admin</summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        // GET: Default 
        public virtual ActionResult Index()
        {
            //return View("Index");
            return View("~/Areas/ModularWizardAdmin/Views/Admin/Index.cshtml");
        }

        [HttpGet("Preview")]
        public virtual ActionResult Preview()
        {
            return View("~/Areas/ModularWizardAdmin/Views/Admin/Preview.cshtml");
            return View("~/Areas/ModularWizardAdmin/Views/Admin/Preview.cshtml");
        }



    }
}

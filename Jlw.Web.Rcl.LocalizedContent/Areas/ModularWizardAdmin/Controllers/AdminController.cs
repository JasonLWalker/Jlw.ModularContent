using System;
using System.Text;
using Jlw.Data.LocalizedContent;
using Jlw.Utilities.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Jlw.Web.Rcl.LocalizedContent.Areas.ModularWizardAdmin.Controllers
{
    public abstract class AdminController : Controller
    {
        protected string _groupFilter = null;
        protected WizardAdminSettings DefaultSettings { get; } = new WizardAdminSettings();
        protected IWizardFactoryRepository DataRepository;

        protected AdminController(IWizardAdminSettings settings, IWizardFactoryRepository repository)
        {
            DataRepository = repository;

            DefaultSettings.IsAdmin = settings.IsAdmin;
            DefaultSettings.CanEdit = settings.CanEdit;
            DefaultSettings.CanDelete = settings.CanDelete;
            DefaultSettings.CanInsert = settings.CanInsert;
            DefaultSettings.UseWysiwyg = settings.UseWysiwyg;
            DefaultSettings.ShowWireFrame = settings.ShowWireFrame;
            DefaultSettings.WireFrameDefault = settings.WireFrameDefault;
            DefaultSettings.TinyMceSettings = settings.TinyMceSettings;
            DefaultSettings.PageTitle = settings.PageTitle;
            DefaultSettings.ExtraCss = settings.ExtraCss;
            DefaultSettings.ExtraScript = settings.ExtraScript;
            DefaultSettings.Area = settings.Area;
            DefaultSettings.JsRoot = settings.JsRoot;
            DefaultSettings.ToolboxHeight = settings.ToolboxHeight;
            DefaultSettings.HiddenFilterPrefix = settings.HiddenFilterPrefix;
            DefaultSettings.PreviewRecordData = settings.PreviewRecordData;

            DefaultSettings.LanguageList.Clear();
            DefaultSettings.LanguageList.AddRange(settings.LanguageList);

            DefaultSettings.ShowSideNav = settings.ShowSideNav;
            DefaultSettings.SideNavDefault = settings.SideNavDefault;
            DefaultSettings.SideNav.Clear();
            DefaultSettings.SideNav.AddRange(settings.SideNav.Items);
            
            DefaultSettings.AdminUrl = settings.AdminUrl;
            DefaultSettings.ApiOverrideUrl = settings.ApiOverrideUrl;
            DefaultSettings.PreviewUrl = settings.PreviewUrl;
            DefaultSettings.PreviewScreenUrl = settings.PreviewScreenUrl;

        }

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
            var settings = new WizardPreviewSettings(DefaultSettings);
            settings.SideNav.Add(new WizardSideNavItem(new WizardContentField(new {Label = "Instructions", FieldType="SCREEN"})));
            return GetViewResult("Preview", settings);
        }

        [HttpGet("PreviewScreen/{wizardName?}/{screenName?}")]
        public virtual ActionResult PreviewScreen(string wizardName = null, string screenName = null)
        {
            return GetPreviewScreen(wizardName, screenName, DefaultSettings, new { });
        }

        [NonAction]
        public virtual ActionResult GetPreviewScreen(string wizardName, string screenName, IWizardAdminSettings settings, object recordData)
        {
            var fields = DataRepository?.GetWizardFields(wizardName, _groupFilter);
            var model = new WizardPreviewSettings(settings ?? DefaultSettings, fields, recordData ?? new {})
            {
                ShowWireFrame = DataUtility.ParseBool(Request.Query["wireframe"]),
                ShowSideNav = DataUtility.ParseBool(Request.Query["showNav"]),
                Wizard = wizardName ?? "",
                Screen = screenName ?? ""
            };

            return View(GetViewPath("PreviewScreen"), model);
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

    }
}

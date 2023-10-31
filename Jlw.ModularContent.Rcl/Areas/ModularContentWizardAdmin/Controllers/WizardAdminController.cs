using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jlw.Utilities.Data;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.ModularContent.Areas.ModularContentWizardAdmin.Controllers
{
    public abstract class WizardAdminController : Controller
    {
        protected string _groupFilter = null;
        protected ModularWizardAdminSettings DefaultSettings { get; } = new ModularWizardAdminSettings();
        protected IModularWizardFactoryRepository DataRepository;

        protected WizardAdminController(IModularWizardAdminSettings settings, IModularWizardFactoryRepository repository)
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
            DefaultSettings.ControllerName = settings.ControllerName;
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
            DefaultSettings.ExportUrl = settings.ExportUrl;

        }

        /// <summary>Default route for admin</summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        // GET: Default 
        public virtual ActionResult Index()
        {
            return GetViewResult("Index");
        }

        [HttpGet("Preview")]
        public virtual ActionResult Preview()
        {
	        var auth = TestAuthDenial(); // Check to see if user has permissions. (Method returns null if authorized, or an ActionResult if not authorized)
	        if (auth != null) return auth;

	        var settings = new ModularWizardPreviewSettings(DefaultSettings);
            settings.SideNav.Add(new ModularWizardSideNavItem(new ModularWizardContentField(new {Label = "Instructions", FieldType="SCREEN"})));
            return GetViewResult("Preview", settings);
        }

        [HttpGet("Export/{wizardName?}/{screenName?}")]
        public virtual ActionResult Export(string wizardName = null, string screenName = null)
        {
	        var auth = TestAuthDenial(); // Check to see if user has permissions. (Method returns null if authorized, or an ActionResult if not authorized)
	        if (auth != null) return auth;
	        if ((auth = TestAuthDenial(DefaultSettings.CanExport, DefaultSettings)) != null) return auth;

			StringBuilder sb = new StringBuilder();
            IEnumerable<IModularContentField> fieldData;
            if (string.IsNullOrWhiteSpace(screenName))
                fieldData = DataRepository.GetWizardFields(wizardName, _groupFilter);
            else
                fieldData = DataRepository.GetWizardFields(wizardName, screenName, "EN", _groupFilter);

            IEnumerable<IModularContentText> aLangValues = DataRepository.GetLanguageValues(wizardName);
            IEnumerable<IModularContentText> aErrorValues = DataRepository.GetLanguageValues(wizardName + "_Errors");
            List<IModularContentText> aLanguageFields = new List<IModularContentText>();

            foreach (var field in fieldData)
            {
                var langRange = aLangValues?.Where(x => (x.FieldKey.Equals(field.FieldKey) && x.ParentKey.Equals(field.ParentKey)))?.ToArray();

                if (langRange?.Any() ?? false)
                    aLanguageFields.AddRange(langRange);

                langRange = aErrorValues?.Where(x => (x.ParentKey.Equals(field.FieldKey)))?.ToArray();

                if (langRange?.Any() ?? false)
                    aLanguageFields.AddRange(langRange);

                sb.AppendLine($"INSERT INTO [dbo].[LocalizedContentFields] ([GroupKey], [FieldKey], [FieldType], [FieldData], [FieldClass], [ParentKey], [DefaultLabel], [WrapperClass], [WrapperHtmlStart], [WrapperHtmlEnd], [AuditChangeType], [AuditChangeBy], [AuditChangeDate], [Order]) VALUES (" +
                              $"{(field.GroupKey == null ? "null" : $"N'{field.GroupKey.Replace("'", "''")}'")}, " +
                              $"{(field.FieldKey == null ? "null" : $"N'{field.FieldKey.Replace("'", "''")}'")}, " +
                              $"{(field.FieldType == null ? "null" : $"N'{field.FieldType.Replace("'", "''")}'")}, " +
                              $"{(field.FieldData == null ? "null" : $"N'{field.FieldData.Replace("'", "''")}'")}, " +
                              $"{(field.FieldClass == null ? "null" : $"N'{field.FieldClass.Replace("'", "''")}'")}, " +
                              $"{(field.ParentKey == null ? "null" : $"N'{field.ParentKey.Replace("'", "''")}'")}, " +
                              $"{(field.DefaultLabel == null ? "null" : $"N'{field.DefaultLabel.Replace("'", "''")}'")}, " +
                              $"{(field.WrapperClass == null ? "null" : $"N'{field.WrapperClass.Replace("'", "''")}'")}, " +
                              $"{(field.WrapperHtmlStart == null ? "null" : $"N'{field.WrapperHtmlStart.Replace("'", "''")}'")}, " +
                              $"{(field.WrapperHtmlEnd == null ? "null" : $"N'{field.WrapperHtmlStart.Replace("'", "''")}'")}, " +
                              $"{(field.AuditChangeType == null ? "null" : $"N'{field.AuditChangeType.Replace("'", "''")}'")}, " +
                              $"{(field.AuditChangeBy == null ? "null" : $"N'{field.AuditChangeBy.Replace("'", "''")}'")}, " +
                              $"N'{field.AuditChangeDate.ToString("yyyy-MM-dd HH:mm:ss")}', " +
                              $"N'{field.Order}'" +
                              $");");
            }

            foreach (var field in aLanguageFields)
            {
                sb.AppendLine($"INSERT INTO [dbo].[LocalizedContentText] ([GroupKey], [FieldKey], [Language], [Text], [AuditChangeType], [AuditChangeBy], [AuditChangeDate], [ParentKey]) VALUES (" +
                              $"{(field.GroupKey == null ? "null" : $"N'{field.GroupKey.Replace("'", "''")}'")}, " +
                              $"{(field.FieldKey == null ? "null" : $"N'{field.FieldKey.Replace("'", "''")}'")}, " +
                              $"{(field.Language == null ? "null" : $"N'{field.Language.Replace("'", "''")}'")}, " +
                              $"{(field.Text == null ? "null" : $"N'{field.Text.Replace("'", "''")}'")}, " +
                              $"{(field.AuditChangeType == null ? "null" : $"N'{field.AuditChangeType.Replace("'", "''")}'")}, " +
                              $"{(field.AuditChangeBy == null ? "null" : $"N'{field.AuditChangeBy.Replace("'", "''")}'")}, " +
                              $"N'{field.AuditChangeDate.ToString("yyyy-MM-dd HH:mm:ss")}', " +
                              $"{(field.ParentKey == null ? "null" : $"N'{field.ParentKey.Replace("'", "''")}'")}" +
                              $");");
            }
                
            byte[] aBytes = Encoding.ASCII.GetBytes(sb.ToString());
            return new FileContentResult(aBytes, "text/plain")
            {
                FileDownloadName = $"{wizardName}-{DateTime.Now.Ticks}.sql"
            };
        }

        [HttpGet("PreviewScreen/{wizardName?}/{screenName?}")]
        public virtual ActionResult PreviewScreen(string wizardName = null, string screenName = null)
        {
            return GetPreviewScreen(wizardName, screenName, DefaultSettings, new { });
        }

        [NonAction]
        public virtual ActionResult GetPreviewScreen(string wizardName, string screenName, IModularWizardAdminSettings settings, object recordData)
        {
	        var auth = TestAuthDenial(); // Check to see if user has permissions. (Method returns null if authorized, or an ActionResult if not authorized)
	        if (auth != null) return auth;
	        if ((auth = TestAuthDenial(DefaultSettings.CanPreview, DefaultSettings)) != null) return auth;

			var fields = DataRepository?.GetWizardFields(wizardName, _groupFilter);
            var model = new ModularWizardPreviewSettings(settings ?? DefaultSettings, fields, recordData ?? new {})
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
        public ActionResult GetViewResult(string viewName = null, IModularWizardAdminSettings settings = null)
        {
	        var auth = TestAuthDenial(); // Check to see if user has permissions. (Method returns null if authorized, or an ActionResult if not authorized)
	        if (auth != null) return auth;

            return View(GetViewPath(viewName), settings ?? DefaultSettings);
        }

        [NonAction]
        public ActionResult TestAuthDenial(bool? testValue = true, IModularWizardAdminSettings settings = null)
        {
	        if (settings is null)
	        {
		        PopulateDefaultSettings();
		        settings = DefaultSettings;
	        }
	        if (!(testValue ?? false) || !settings.IsAuthorized) return View(GetViewPath("NotAuthorized"), settings ?? DefaultSettings);

			return null;
        }

		[NonAction]
        public string GetViewPath(string viewName)
        {
            return $"~/Areas/ModularContentWizardAdmin/Views/WizardAdmin/{viewName ?? "Index"}.cshtml";
        }

        [NonAction]
        protected virtual void PopulateDefaultSettings()
        {

        }
    }
}

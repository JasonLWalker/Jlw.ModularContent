using System;
using System.Collections.Generic;
using Jlw.Data.LocalizedContent;
using Jlw.Utilities.Data;
using Jlw.Utilities.Data.DbUtility;
using Jlw.Utilities.Testing;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using TModel = Jlw.Web.Rcl.LocalizedContent.Areas.ModularWizardAdmin.Controllers.AdminController.WizardAdminSettings;

namespace Jlw.Web.Rcl.LocalizedContent.Tests.ModelTests
{
    public class ModularWizardAdmin_WizardAdminSettingsTestSchema : BaseModelSchema<TModel>
    {
        void InitConstructors()
        {
        }

        void InitProperties()
        {
            AddProperty(typeof(bool), "IsAdmin", Public, Public);
            AddProperty(typeof(bool), "CanDelete", Public, Public);
            AddProperty(typeof(bool), "CanEdit", Public, Public);
            AddProperty(typeof(bool), "CanInsert", Public, Public);
            AddProperty(typeof(bool), "UseWysiwyg", Public, Public);
            AddProperty(typeof(bool), "ShowSideNav", Public, Public);
            AddProperty(typeof(bool), "SideNavDefault", Public, Public);
            AddProperty(typeof(bool), "ShowWireFrame", Public, Public);
            AddProperty(typeof(bool), "WireFrameDefault", Public, Public);
            AddProperty(typeof(JToken), "TinyMceSettings", Public, Public);
            AddProperty(typeof(string), "PageTitle", Public, Public);
            AddProperty(typeof(string), "ExtraCss", Public, Public);
            AddProperty(typeof(string), "ExtraScript", Public, Public);
            AddProperty(typeof(string), "Area", Public, Public);
            AddProperty(typeof(string), "AdminUrl", Public, Public);
            AddProperty(typeof(string), "ApiOverrideUrl", Public, Public);
            AddProperty(typeof(string), "JsRoot", Public, Public);
            AddProperty(typeof(string), "ToolboxHeight", Public, Public);
            AddProperty(typeof(string), "HiddenFilterPrefix", Public, Public);
            AddProperty(typeof(List<SelectListItem>), "LanguageList", Public, null);
        }

        void InitFields()
        {
        }

        void InitInterfaces()
        {
        }

        public ModularWizardAdmin_WizardAdminSettingsTestSchema()
        {
            InitConstructors();
            InitProperties();
            InitFields();
            InitInterfaces();
        }


    }
}

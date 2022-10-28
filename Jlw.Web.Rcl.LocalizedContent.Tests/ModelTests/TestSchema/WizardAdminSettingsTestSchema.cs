using System;
using System.Collections.Generic;
using Jlw.Data.LocalizedContent;
using Jlw.Utilities.Data;
using Jlw.Utilities.Data.DbUtility;
using Jlw.Utilities.Testing;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json.Linq;
using TModel = Jlw.Web.Rcl.LocalizedContent.WizardAdminSettings;

namespace Jlw.Web.Rcl.LocalizedContent.Tests.ModelTests
{
    public class WizardAdminSettingsTestSchema : BaseModelSchema<TModel>
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
            AddProperty(typeof(LinkGenerator), "LinkGenerator", Public, Public);
            AddProperty(typeof(string), "Area", Public, Public, false);
            AddProperty(typeof(string), "ControllerName", Public, Public);
            AddProperty(typeof(string), "ApiControllerName", Public, Public);
            AddProperty(typeof(string), "AdminUrl", Public, Public, false);
            AddProperty(typeof(string), "ApiOverrideUrl", Public, Public, false);
            AddProperty(typeof(string), "PreviewUrl", Public, Public, false);
            AddProperty(typeof(string), "PreviewScreenUrl", Public, Public, false);
            AddProperty(typeof(string), "ExportUrl", Public, Public, false);
            AddProperty(typeof(string), "JsRoot", Public, Public, false);
            AddProperty(typeof(string), "ToolboxHeight", Public, Public);
            AddProperty(typeof(string), "HiddenFilterPrefix", Public, Public);

            AddProperty(typeof(string), "Version", Public, Protected);
            AddProperty(typeof(string), "DefaultWizard", Public, Protected);
            AddProperty(typeof(string), "GroupFilter", Public, Protected);

            AddProperty(typeof(WizardSideNav), "SideNav", Public, null);
            AddProperty(typeof(Object), "PreviewRecordData", Public, Public);
            AddProperty(typeof(List<SelectListItem>), "LanguageList", Public, null);
        }

        void InitFields()
        {
        }

        void InitInterfaces()
        {
        }

        public WizardAdminSettingsTestSchema()
        {
            InitConstructors();
            InitProperties();
            InitFields();
            InitInterfaces();
        }


    }
}

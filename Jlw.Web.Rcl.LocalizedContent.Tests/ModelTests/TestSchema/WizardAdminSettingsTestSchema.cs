using System;
using System.Collections.Generic;
using Jlw.ModularContent;
using Jlw.Utilities.Data;
using Jlw.Utilities.Data.DbUtility;
using Jlw.Utilities.Testing;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json.Linq;

namespace Jlw.Web.Rcl.LocalizedContent.Tests.ModelTests
{
    public class WizardAdminSettingsTestSchema : BaseModelSchema<ModularWizardAdminSettings>
    {
        void InitConstructors()
        {
        }

        void InitProperties()
        {
            AddProperty(typeof(bool), "IsAuthorized", Public, Public);
            AddProperty(typeof(bool), "IsAdmin", Public, Public);

            AddProperty(typeof(bool), "CanRead", Public, Public);
            AddProperty(typeof(bool), "CanReadField", Public, Public);
            AddProperty(typeof(bool), "CanOrderField", Public, Public);

            AddProperty(typeof(bool), "CanRenameField", Public, Public);
            AddProperty(typeof(bool), "CanRenameWizard", Public, Public);
            AddProperty(typeof(bool), "CanRenameScreen", Public, Public);

            AddProperty(typeof(bool), "CanEdit", Public, Public);
            AddProperty(typeof(bool), "CanEditField", Public, Public);
            AddProperty(typeof(bool), "CanEditWizard", Public, Public);
            AddProperty(typeof(bool), "CanEditScreen", Public, Public);
            AddProperty(typeof(bool), "CanEditLabelText", Public, Public);
            AddProperty(typeof(bool), "CanEditError", Public, Public);
            AddProperty(typeof(bool), "CanEditErrorText", Public, Public);

            AddProperty(typeof(bool), "CanDuplicateField", Public, Public);
            AddProperty(typeof(bool), "CanDuplicateWizard", Public, Public);
            AddProperty(typeof(bool), "CanDuplicateScreen", Public, Public);

            AddProperty(typeof(bool), "CanDelete", Public, Public);
            AddProperty(typeof(bool), "CanDeleteField", Public, Public);
            AddProperty(typeof(bool), "CanDeleteWizard", Public, Public);
            AddProperty(typeof(bool), "CanDeleteScreen", Public, Public);
            AddProperty(typeof(bool), "CanDeleteLabel", Public, Public);
            AddProperty(typeof(bool), "CanDeleteLabelText", Public, Public);
            AddProperty(typeof(bool), "CanDeleteError", Public, Public);
            AddProperty(typeof(bool), "CanDeleteErrorText", Public, Public);

            AddProperty(typeof(bool), "CanInsert", Public, Public);
            AddProperty(typeof(bool), "CanInsertField", Public, Public);
            AddProperty(typeof(bool), "CanInsertWizard", Public, Public);
            AddProperty(typeof(bool), "CanInsertScreen", Public, Public);
            AddProperty(typeof(bool), "CanInsertLabel", Public, Public);
            AddProperty(typeof(bool), "CanInsertLabelText", Public, Public);
            AddProperty(typeof(bool), "CanInsertError", Public, Public);
            AddProperty(typeof(bool), "CanInsertErrorText", Public, Public);

            AddProperty(typeof(bool), "CanPreview", Public, Public);
            AddProperty(typeof(bool), "CanExport", Public, Public);


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

            AddProperty(typeof(ModularWizardSideNav), "SideNav", Public, null);
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

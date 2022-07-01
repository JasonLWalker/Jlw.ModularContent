using System.Collections.Generic;
using Jlw.Data.LocalizedContent;
using Jlw.Utilities.Data;
using Jlw.Web.Rcl.LocalizedContent.Areas.ModularWizardAdmin;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;

namespace Jlw.Web.Rcl.LocalizedContent;

public class WizardAdminSettings : IWizardAdminSettings
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

    public string HiddenFilterPrefix { get; set; }

    public List<SelectListItem> LanguageList { get; } = new List<SelectListItem>() { new SelectListItem("English", "EN") };


    public readonly WizardSideNav SideNav = new WizardSideNav();

    public WizardAdminSettings() : this(null) { }

    public WizardAdminSettings(object o)
    {
        IsAdmin = DataUtility.ParseBool(o, "IsAdmin");
        CanEdit = DataUtility.ParseBool(o, "CanEdit");
        CanDelete = DataUtility.ParseBool(o, "CanDelete");
        CanInsert = DataUtility.ParseBool(o, "CanInsert");
        UseWysiwyg = DataUtility.ParseBool(o, "UseWysiwyg");
        ShowSideNav = DataUtility.ParseBool(o, "ShowSideNav");
        ShowWireFrame = DataUtility.ParseBool(o, "ShowWireFrame");
        SideNavDefault = DataUtility.ParseBool(o, "SideNavDefault");
        WireFrameDefault = DataUtility.ParseBool(o, "WireFrameDefault");
        TinyMceSettings = DataUtility.ParseBool(o, "TinyMceSettings");
        PageTitle = DataUtility.ParseString(o, "PageTitle");
        ExtraCss = DataUtility.ParseString(o, "ExtraCss");
        ExtraScript = DataUtility.ParseString(o, "ExtraScript");
        Area = DataUtility.ParseString(o, "Area");
        AdminUrl = DataUtility.ParseString(o, "AdminUrl");
        ApiOverrideUrl = DataUtility.ParseString(o, "ApiOverrideUrl");
        JsRoot = DataUtility.ParseString(o, "JsRoot");
        ToolboxHeight = DataUtility.ParseString(o, "ToolboxHeight");
        HiddenFilterPrefix = DataUtility.ParseString(o, "HiddenFilterPrefix");
    }
}
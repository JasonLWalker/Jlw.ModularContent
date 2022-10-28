using System.Collections.Generic;
using Jlw.Utilities.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json.Linq;

namespace Jlw.Web.Rcl.LocalizedContent;

public class WizardAdminSettings : WizardSettings, IWizardAdminSettings
{
    #region Properties
    public bool IsAdmin { get; set; }
    public bool CanEdit { get; set; }
    public bool CanDelete { get; set; }
    public bool CanInsert { get; set; }

    public bool UseWysiwyg { get; set; } = true;
    public bool ShowWireFrame { get; set; } = true;
    public bool SideNavDefault { get; set; } = true;
    public bool WireFrameDefault { get; set; } = false;

    public JToken TinyMceSettings { get; set; }

    protected string _adminUrl = null;
    public virtual string AdminUrl
    {
        get => string.IsNullOrWhiteSpace(_adminUrl) ? LinkGenerator?.GetPathByAction("Index", ControllerName ?? "", new { Area = this.Area }) ?? "" : _adminUrl; 
        set => _adminUrl = value;
    }

    protected string _previewUrl = null;
    public virtual string PreviewUrl
    {
        get => string.IsNullOrWhiteSpace(_previewUrl) ? LinkGenerator?.GetPathByAction("Preview", ControllerName ?? "", new { Area = this.Area }) ?? "" : _previewUrl;
        set => _previewUrl = value;
    }

    protected string _previewScreenUrl = null;
    public virtual string PreviewScreenUrl
    {
        get => string.IsNullOrWhiteSpace(_previewScreenUrl) ? LinkGenerator?.GetPathByAction("PreviewScreen", ControllerName ?? "", new { Area = this.Area }) ?? "" : _previewScreenUrl;
        set => _previewScreenUrl = value;
    }

    protected string _exportUrl = null;
    public virtual string ExportUrl
    {
        get => string.IsNullOrWhiteSpace(_exportUrl) ? LinkGenerator?.GetPathByAction("Export", ControllerName ?? "", new { Area = this.Area }) ?? "" : _exportUrl;
        set => _exportUrl = value;
    }

    public string ToolboxHeight { get; set; }

    public string HiddenFilterPrefix { get; set; }
    public object PreviewRecordData { get; set; }

    public List<SelectListItem> LanguageList { get; } = new List<SelectListItem>() { new SelectListItem("English", "EN") };
    #endregion

    public WizardAdminSettings() : this(null) { }

    public WizardAdminSettings(object o) : base(o)
    {
        IsAdmin = DataUtility.ParseNullableBool(o, "IsAdmin") ?? false;
        CanEdit = DataUtility.ParseNullableBool(o, "CanEdit") ?? false;
        CanDelete = DataUtility.ParseBool(o, "CanDelete");
        CanInsert = DataUtility.ParseBool(o, "CanInsert");
        UseWysiwyg = DataUtility.ParseBool(o, "UseWysiwyg");

        ShowWireFrame = DataUtility.ParseBool(o, "ShowWireFrame");
        SideNavDefault = DataUtility.ParseBool(o, "SideNavDefault");
        WireFrameDefault = DataUtility.ParseBool(o, "WireFrameDefault");
        TinyMceSettings = DataUtility.ParseBool(o, "TinyMceSettings");

        _adminUrl = DataUtility.ParseString(o, "AdminUrl");
        _previewUrl = DataUtility.ParseString(o, "PreviewUrl");
        _previewScreenUrl = DataUtility.ParseString(o, "PreviewScreenUrl");
        _exportUrl = DataUtility.ParseString(o, "ExportUrl");

        ToolboxHeight = DataUtility.ParseString(o, "ToolboxHeight");
        HiddenFilterPrefix = DataUtility.ParseString(o, "HiddenFilterPrefix");
        PreviewRecordData = DataUtility.Parse<object>(o, "PreviewRecordData");
    }
}
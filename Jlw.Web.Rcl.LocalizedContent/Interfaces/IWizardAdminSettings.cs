using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json.Linq;

namespace Jlw.Web.Rcl.LocalizedContent;

public interface IWizardAdminSettings : IWizardSettings
{
    bool IsAdmin { get; set; }
    bool CanEdit { get; set; }
    bool CanDelete { get; set; }
    bool CanInsert { get; set; }
    bool UseWysiwyg { get; set; }
    bool ShowWireFrame { get; set; }
    bool SideNavDefault { get; set; }
    bool WireFrameDefault { get; set; }
    JToken TinyMceSettings { get; set; }

    string AdminUrl { get; set; }
    string PreviewUrl { get; set; }
    string PreviewScreenUrl { get; set; }
    string ExportUrl { get; set; }

    string ToolboxHeight { get; set; }
    string HiddenFilterPrefix { get; set; }
    public object PreviewRecordData { get; set; }
    List<SelectListItem> LanguageList { get; }
}
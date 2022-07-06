using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;

namespace Jlw.Web.Rcl.LocalizedContent;

public interface IWizardAdminSettings
{
    bool IsAdmin { get; set; }
    bool CanEdit { get; set; }
    bool CanDelete { get; set; }
    bool CanInsert { get; set; }
    bool UseWysiwyg { get; set; }
    bool ShowSideNav { get; set; }
    bool ShowWireFrame { get; set; }
    bool SideNavDefault { get; set; }
    bool WireFrameDefault { get; set; }
    JToken TinyMceSettings { get; set; }
    string PageTitle { get; set; }
    string ExtraCss { get; set; }
    string ExtraScript { get; set; }
    string Area { get; set; }
    string AdminUrl { get; set; }
    string ApiOverrideUrl { get; set; }
    string JsRoot { get; set; }
    string ToolboxHeight { get; set; }
    string HiddenFilterPrefix { get; set; }
    List<SelectListItem> LanguageList { get; }
}
using Jlw.LocalizedContent;
using Microsoft.AspNetCore.Routing;

namespace Jlw.Web.Rcl.LocalizedContent;

public interface IWizardSettings
{
    string DefaultWizard { get; }
    string Version { get; }
    string GroupFilter { get; }
    bool ShowSideNav { get; set; }
    WizardSideNav SideNav { get; }
    string PageTitle { get; set; }
    string ExtraCss { get; set; }
    string ExtraScript { get; set; }
    string Area { get; set; }
    string ApiControllerName { get; set; }
    string ControllerName { get; set; }

    public LinkGenerator LinkGenerator { get; set; }
    string ApiOverrideUrl { get; set; }
    string JsRoot { get; set; }
}
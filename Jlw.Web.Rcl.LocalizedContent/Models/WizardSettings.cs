using Jlw.Data.LocalizedContent;
using Jlw.Utilities.Data;

namespace Jlw.Web.Rcl.LocalizedContent;

public class WizardSettings : IWizardSettings
{
    public bool ShowSideNav { get; set; } = true;
    public string PageTitle { get; set; }
    public string ExtraCss { get; set; }
    public string ExtraScript { get; set; }
    public string Area { get; set; }

    public string ApiOverrideUrl { get; set; }
    public string JsRoot { get; set; }


    protected readonly WizardSideNav _sideNav = new WizardSideNav();
    public WizardSideNav SideNav => _sideNav;

    public WizardSettings() : this(null)
    {
    }

    public WizardSettings(object o)
    {
        ShowSideNav = DataUtility.ParseBool(o, "ShowSideNav");

        PageTitle = DataUtility.ParseString(o, "PageTitle");
        ExtraCss = DataUtility.ParseString(o, "ExtraCss");
        ExtraScript = DataUtility.ParseString(o, "ExtraScript");
        Area = DataUtility.ParseString(o, "Area");
        ApiOverrideUrl = DataUtility.ParseString(o, "ApiOverrideUrl");
        JsRoot = DataUtility.ParseString(o, "JsRoot");
    }
}
using System;
using Jlw.Data.LocalizedContent;
using Jlw.Utilities.Data;
using Microsoft.AspNetCore.Routing;

namespace Jlw.Web.Rcl.LocalizedContent;

public class WizardSettings : IWizardSettings
{
    public string Version { get; protected set; }
    public string GroupFilter { get; protected set; }
    public string DefaultWizard { get; protected set; }

    public bool ShowSideNav { get; set; } = true;
    public string PageTitle { get; set; }
    public string ExtraCss { get; set; }
    public string ExtraScript { get; set; }
    public string Area { get; set; }

    public string ApiControllerName { get; set; }
    public string ControllerName { get; set; }

    public LinkGenerator LinkGenerator { get; set; }

    protected string _apiOverrideUrl;
    public string ApiOverrideUrl
    {
        get => string.IsNullOrWhiteSpace(_apiOverrideUrl) ? LinkGenerator?.GetPathByAction("Index", ApiControllerName ?? "Api", new { Area }) ?? "" : _apiOverrideUrl;
        set => _apiOverrideUrl = value;
    }

    protected string _jsRoot;
    public string JsRoot 
    {
        get => string.IsNullOrWhiteSpace(_jsRoot) ? Area ?? "" : _jsRoot;
        set => _jsRoot = value;
    }



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
        ApiControllerName = DataUtility.ParseString(o, "ApiControllerName");
        ControllerName = DataUtility.ParseString(o, "ControllerName");
        ApiOverrideUrl = DataUtility.ParseString(o, "ApiOverrideUrl");
        JsRoot = DataUtility.ParseString(o, "JsRoot");

        DefaultWizard = DataUtility.ParseString(o, "DefaultWizard");
        GroupFilter = DataUtility.ParseString(o, "GroupFilter");

        Version = DataUtility.ParseString(o, "Version");
        if (string.IsNullOrWhiteSpace(Version))
        {
            Version = typeof(WizardSettings).Assembly.GetName().Version?.ToString() ?? "";
        }
        Version = string.IsNullOrWhiteSpace(Version) || Version == "0.0.0.1" ? DateTime.Now.Ticks.ToString() : Version;

    }
}
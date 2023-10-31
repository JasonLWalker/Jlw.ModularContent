using System;
using Jlw.Utilities.Data;
using Microsoft.AspNetCore.Routing;

namespace Jlw.ModularContent;

public class ModularWizardSettings : IModularWizardSettings
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



    protected readonly ModularWizardSideNav _sideNav = new ModularWizardSideNav();
    public ModularWizardSideNav SideNav => _sideNav;

    public ModularWizardSettings() : this(null)
    {
    }

    public ModularWizardSettings(object o)
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
            Version = typeof(ModularWizardSettings).Assembly.GetName().Version?.ToString() ?? "";
        }
        Version = string.IsNullOrWhiteSpace(Version) || Version == "0.0.0.1" ? DateTime.Now.Ticks.ToString() : Version;

    }
}
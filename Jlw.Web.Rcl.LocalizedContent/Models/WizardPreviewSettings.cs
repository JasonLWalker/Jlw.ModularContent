using System;
using System.Collections.Generic;
using System.Linq;
using Jlw.LocalizedContent;
using Jlw.Utilities.Data;

namespace Jlw.Web.Rcl.LocalizedContent;

public class WizardPreviewSettings : WizardAdminSettings
{
    public string Screen { get; set; }
    public string Wizard { get; set; }

    public WizardPreviewSettings(object settings, IEnumerable<IWizardContentField> fields = null, object recordData = null) : base(settings)
    {
        Screen = DataUtility.ParseString(settings, "Screen");
        Wizard = DataUtility.ParseString(settings, "Wizard");
        if (fields != null)
            SideNav.AddRange(fields.Where(o=>o.FieldType.Equals("SCREEN", StringComparison.InvariantCultureIgnoreCase)).Select(o=>new WizardSideNavItem(o)));

    }
}
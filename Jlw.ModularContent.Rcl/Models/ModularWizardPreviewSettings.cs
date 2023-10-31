using System;
using System.Collections.Generic;
using System.Linq;
using Jlw.Utilities.Data;

namespace Jlw.ModularContent;

public class ModularWizardPreviewSettings : ModularWizardAdminSettings
{
    public string Screen { get; set; }
    public string Wizard { get; set; }

    public ModularWizardPreviewSettings(object settings, IEnumerable<IModularWizardContentField> fields = null, object recordData = null) : base(settings)
    {
        Screen = DataUtility.ParseString(settings, "Screen");
        Wizard = DataUtility.ParseString(settings, "Wizard");
        if (fields != null)
            SideNav.AddRange(fields.Where(o=>o.FieldType.Equals("SCREEN", StringComparison.InvariantCultureIgnoreCase)).Select(o=>new WizardSideNavItem(o)));

    }
}
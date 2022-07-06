using System;
using System.Collections.Generic;
using System.Linq;
using Jlw.Data.LocalizedContent;
using Jlw.Utilities.Data;
using Newtonsoft.Json.Linq;

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

public class WizardSideNav
{
    protected readonly IEnumerable<IWizardContentField> _fields;
    protected readonly List<WizardSideNavItem> _items = new List<WizardSideNavItem>();
    public IEnumerable<WizardSideNavItem> Items => _items;

    public WizardSideNav(IEnumerable<IWizardContentField> fields = null)
    {
        if (fields != null)
        {
            _fields = fields?.Where(o =>
                          o.FieldType.Equals("SCREEN", StringComparison.InvariantCultureIgnoreCase) ||
                          o.FieldType.Equals("WIZARD", StringComparison.InvariantCultureIgnoreCase)) ??
                      new IWizardContentField[] { };
            _items.AddRange(_fields.Select(o => new WizardSideNavItem(o)));
        }
    }

    public void Clear() => _items.Clear();
    public void Add(WizardSideNavItem item) => _items.Add(item);
    public void AddRange(IEnumerable<WizardSideNavItem> items) => _items.AddRange(items);
}



public class WizardSideNavItem
{
    protected readonly IWizardContentField _field;
    protected readonly JToken _jsonData;
    public string Screen => (_field?.FieldType?.Equals("SCREEN", StringComparison.InvariantCultureIgnoreCase) ?? false) ? (_field?.FieldKey ?? "") : "";
    public string Wizard => (_field?.FieldType?.Equals("SCREEN", StringComparison.InvariantCultureIgnoreCase) ?? false) ? (_field?.ParentKey ?? "") : "";
    public int Section => DataUtility.ParseInt(_jsonData?["section"]);
    public int Step => DataUtility.ParseInt(_jsonData?["step"]);
    public string Label => (_field?.FieldType?.Equals("SCREEN", StringComparison.InvariantCultureIgnoreCase) ?? false) ? (_field?.Label ?? "") : "";
    public string FieldType => _field.FieldType;

    public WizardSideNavItem(IWizardContentField field)
    {
        _field = field;
        try
        {
            _jsonData = JToken.Parse(_field?.FieldData ?? "{}");
        }
        catch
        {
            // do nothing
        }
    }

    
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jlw.Data.LocalizedContent
{
    public class WizardSideNav : IWizardSideNav
    {
        protected readonly IEnumerable<IWizardContentField> _fields;
        protected readonly List<IWizardSideNavItem> _items = new List<IWizardSideNavItem>();
        public IEnumerable<IWizardSideNavItem> Items => _items;

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
        public void Add(IWizardSideNavItem item) => _items.Add(item);
        public void AddRange(IEnumerable<IWizardSideNavItem> items) => _items.AddRange(items);
    }
}
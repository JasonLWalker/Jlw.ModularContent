using System;
using System.Collections.Generic;
using System.Linq;

namespace Jlw.ModularContent
{
    /// <summary>
    /// Class to define a collection of screens for use in the side navigation of a wizard.
    /// </summary>
    public class WizardSideNav : IWizardSideNav
    {
        /// <summary>Internal list of Screen fields</summary>
        protected readonly IEnumerable<IWizardContentField> _fields;

        /// <summary>Internal list of individual items</summary>
        protected readonly List<IWizardSideNavItem> _items = new List<IWizardSideNavItem>();

        /// <summary>Public list to individual items</summary>
        public IEnumerable<IWizardSideNavItem> Items => _items;

        /// <summary>
        /// Constructor to initialize the list of items from an IEnumerable&lt;IWizardContentField&gt; list of fields
        /// </summary>
        /// <param name="fields"></param>
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

        /// <inheritdoc />
        public void Clear() => _items.Clear();

        /// <inheritdoc />
        public void Add(IWizardSideNavItem item) => _items.Add(item);

        /// <inheritdoc />
        public void AddRange(IEnumerable<IWizardSideNavItem> items) => _items.AddRange(items);
    }
}
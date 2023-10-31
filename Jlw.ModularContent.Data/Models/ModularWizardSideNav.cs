using System;
using System.Collections.Generic;
using System.Linq;

namespace Jlw.ModularContent
{
    /// <summary>
    /// Class to define a collection of screens for use in the side navigation of a wizard.
    /// </summary>
    public class ModularWizardSideNav : IModularWizardSideNav
    {
        /// <summary>Internal list of Screen fields</summary>
        protected readonly IEnumerable<IModularWizardContentField> _fields;

        /// <summary>Internal list of individual items</summary>
        protected readonly List<IModularWizardSideNavItem> _items = new List<IModularWizardSideNavItem>();

        /// <summary>Public list to individual items</summary>
        public IEnumerable<IModularWizardSideNavItem> Items => _items;

        /// <summary>
        /// Constructor to initialize the list of items from an IEnumerable&lt;IWizardContentField&gt; list of fields
        /// </summary>
        /// <param name="fields"></param>
        public ModularWizardSideNav(IEnumerable<IModularWizardContentField> fields = null)
        {
            if (fields != null)
            {
                _fields = fields?.Where(o =>
                              o.FieldType.Equals("SCREEN", StringComparison.InvariantCultureIgnoreCase) ||
                              o.FieldType.Equals("WIZARD", StringComparison.InvariantCultureIgnoreCase)) ??
                          new IModularWizardContentField[] { };
                _items.AddRange(_fields.Select(o => new ModularWizardSideNavItem(o)));
            }
        }

        /// <inheritdoc />
        public void Clear() => _items.Clear();

        /// <inheritdoc />
        public void Add(IModularWizardSideNavItem item) => _items.Add(item);

        /// <inheritdoc />
        public void AddRange(IEnumerable<IModularWizardSideNavItem> items) => _items.AddRange(items);
    }
}
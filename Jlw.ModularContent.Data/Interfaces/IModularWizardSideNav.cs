using System.Collections.Generic;

namespace Jlw.ModularContent
{
    /// <summary>
    /// Interface to define a collection of screens for use in the side navigation of a wizard.
    /// </summary>
    public interface IModularWizardSideNav
    {
        /// <summary>
        /// Collection of navigation screens
        /// </summary>
        IEnumerable<IModularWizardSideNavItem> Items { get; }

        /// <summary>
        /// Clear/Empty the list of items
        /// </summary>
        void Clear();

        /// <summary>
        /// Add a new item to the end of the list
        /// </summary>
        /// <param name="item"></param>
        void Add(IModularWizardSideNavItem item);

        /// <summary>
        /// Add multiple items to the end of the list
        /// </summary>
        /// <param name="items"></param>
        void AddRange(IEnumerable<IModularWizardSideNavItem> items);
    }
}
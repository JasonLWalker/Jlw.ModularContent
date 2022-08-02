using System.Collections.Generic;

namespace Jlw.Data.LocalizedContent
{
    public interface IWizardSideNav
    {
        IEnumerable<IWizardSideNavItem> Items { get; }
        void Clear();
        void Add(IWizardSideNavItem item);
        void AddRange(IEnumerable<IWizardSideNavItem> items);
    }
}
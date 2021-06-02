using System.Collections.Generic;

namespace Jlw.Data.LocalizedContent
{
    public interface IWizardFactory
    {
        IWizardContent CreateWizardContent(string groupKey, object formData = null);
    }
}
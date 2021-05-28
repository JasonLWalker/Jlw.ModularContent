using System.Collections.Generic;
using Jlw.Utilities.Data.DbUtility;

namespace Jlw.Data.LocalizedContent
{
    public interface IWizardFactoryRepository : IModularDataRepositoryBase<ILocalizedContentField, LocalizedContentField>
    {
        IEnumerable<WizardContentField> GetFieldData(string groupKey);
    }
}
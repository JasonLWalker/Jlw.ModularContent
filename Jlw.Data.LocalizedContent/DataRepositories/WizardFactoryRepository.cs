using System.Collections.Generic;
using System.Data;
using Jlw.Utilities.Data.DbUtility;

namespace Jlw.Data.LocalizedContent
{
    public class WizardFactoryRepository : ModularDataRepositoryBase<ILocalizedContentField, LocalizedContentField>, IWizardFactoryRepository
    {
        public WizardFactoryRepository(IModularDbClient dbClient, string connString) : base(dbClient, connString)
        {
        }

        public IEnumerable<WizardContentField> GetFieldData(string groupKey)
        {
            return _dbClient.GetRecordList<WizardContentField>(groupKey, _connString, new RepositoryMethodDefinition("sp_GetFormFields", CommandType.StoredProcedure, new[] { "groupKey" }));
        }
    }
}
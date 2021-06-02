using Jlw.Utilities.Data.DbUtility;

namespace Jlw.Data.LocalizedContent 
{
    public interface ILocalizedContentFieldRepository : IModularDataRepositoryBase<ILocalizedContentField, LocalizedContentField>
    {
        object GetDataTableList(LocalizedContentFieldDataTablesInput o);
    }
} 

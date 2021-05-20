using Jlw.Utilities.Data.DbUtility;

namespace Jlw.Data.LocalizedContent 
{ 
    public interface ILocalizedContentTextRepository : IModularDataRepositoryBase<ILocalizedContentText, LocalizedContentText>
    {
        object GetDataTableList(LocalizedContentTextDataTablesInput o);
    }
} 

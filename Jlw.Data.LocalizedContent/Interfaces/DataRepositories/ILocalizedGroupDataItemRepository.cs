using Jlw.Utilities.Data.DbUtility;

namespace Jlw.Data.LocalizedContent 
{ 
    public interface ILocalizedGroupDataItemRepository : IModularDataRepositoryBase<ILocalizedGroupDataItem, LocalizedGroupDataItem>
    {
        object GetDataTableList(LocalizedGroupDataItemDataTablesInput o);
    }
} 

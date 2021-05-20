using Jlw.Utilities.Data.DbUtility;

namespace Jlw.Data.LocalizedContent
{
    public class LocalizedGroupDataItemRepositoryOptions
    {
        public IModularDbClient DbClient { get; set; }
        public string ConnectionString { get; set; }
    }
}
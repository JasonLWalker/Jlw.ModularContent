using Jlw.Utilities.Data.DbUtility;

namespace Jlw.Data.LocalizedContent
{
    public class LocalizedContentTextRepositoryOptions
    {
        public IModularDbClient DbClient { get; set; }
        public string ConnectionString { get; set; }
    }
}
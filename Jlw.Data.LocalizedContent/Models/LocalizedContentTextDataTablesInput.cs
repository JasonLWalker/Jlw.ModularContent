using Jlw.Utilities.Data.DataTables;

namespace Jlw.Data.LocalizedContent
{
    public class LocalizedContentTextDataTablesInput : DataTablesInput
    {
        public string GroupKey { get; set; }
        public string FieldKey { get; set; }
        public string Language { get; set; }
    }
}
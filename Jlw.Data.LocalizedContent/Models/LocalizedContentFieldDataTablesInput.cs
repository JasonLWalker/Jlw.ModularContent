using Jlw.Utilities.Data;
using Jlw.Utilities.Data.DataTables;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Jlw.Data.LocalizedContent
{
    public class LocalizedContentFieldDataTablesInput : DataTablesInput
    {
        public long Id { get; set; }
        public string FieldType { get; set; }
        public string FieldKey { get; set; }
        public string GroupKey { get; set; }
        public string ParentKey { get; set; }
        public string GroupFilter { get; set; }
    }
}
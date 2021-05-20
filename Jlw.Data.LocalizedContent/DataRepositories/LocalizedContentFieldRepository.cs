using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Jlw.Utilities.Data;
using Jlw.Utilities.Data.DataTables;
using Jlw.Utilities.Data.DbUtility;

namespace Jlw.Data.LocalizedContent 
{
    public class LocalizedContentFieldRepository : ModularDataRepositoryBase<ILocalizedContentField, LocalizedContentField>, ILocalizedContentFieldRepository 
    {
        protected const string SpGetRecord = "sp_GetLocalizedContentFieldRecord"; 
        protected const string SpSaveRecord = "sp_SaveLocalizedContentFieldRecord"; 
        protected const string SpDeleteRecord = "sp_DeleteLocalizedContentFieldRecord"; 
        protected const string SpListRecord = "sp_GetLocalizedContentFieldList"; 
 
        public LocalizedContentFieldRepository(IModularDbClient dbClient, string connString) : base(dbClient, connString) 
        { 
            _sGetRecord = SpGetRecord; 
            _sDeleteRecord = SpDeleteRecord; 
            _sSaveRecord = SpSaveRecord; 
            _sGetAllRecords = SpListRecord; 
        } 
 
        protected override IEnumerable<KeyValuePair<string, object>> GetParamsForSql(ILocalizedContentField o, string sSql) 
        { 
            switch (sSql) 
            {
                case SpSaveRecord:
                    return new KeyValuePair<string, object>[] {
                        new KeyValuePair<string, object>("@id", "Id"),
                        new KeyValuePair<string, object>("@groupkey", "GroupKey"),
                        new KeyValuePair<string, object>("@fieldkey", "FieldKey"),
                        new KeyValuePair<string, object>("@fieldtype", "FieldType"),
                        new KeyValuePair<string, object>("@fielddata", "FieldData"),
                        new KeyValuePair<string, object>("@fieldclass", "FieldClass"),
                        new KeyValuePair<string, object>("@parentkey", "ParentKey"),
                        new KeyValuePair<string, object>("@defaultlabel", "DefaultLabel"),
                        new KeyValuePair<string, object>("@wrapperclass", "WrapperClass"),
                        new KeyValuePair<string, object>("@wrapperhtmlstart", "WrapperHtmlStart"),
                        new KeyValuePair<string, object>("@wrapperhtmlend", "WrapperHtmlEnd"),
                        new KeyValuePair<string, object>("@auditchangeby", "AuditChangeBy"),
                        new KeyValuePair<string, object>("@order", "Order"),
                    };
                case SpDeleteRecord:
                    return new KeyValuePair<string, object>[] {
                        new KeyValuePair<string, object>("@id", "Id"),
                        new KeyValuePair<string, object>("@auditchangeby", "AuditChangeBy"),
                    };
                case SpGetRecord: 
                    return new KeyValuePair<string, object>[] { 
                        new KeyValuePair<string, object>("@id", "Id"), 
                    }; 
                //case SpListRecord: 
                //    return new KeyValuePair<string, object>[] { }; 
            }

            var t = new StackTrace();
            var m = t.GetFrame(1).GetMethod();
            string typename = DataUtility.GetTypeName(m.ReflectedType).Trim(',', ' ');
            throw new NotImplementedException($"The method {typename}.{m.Name}({DataUtility.GetTypeArgs(m.GetParameters().Select(p => p.ParameterType).ToArray())}) is not implemented at this time.");
        }

        public object GetDataTableList(LocalizedContentFieldDataTablesInput o)
        {
            string sQuery = $"EXEC [dbo].[sp_GetLocalizedContentFieldsDt] @sSearch=@sSearch, @nRowStart=@nRowStart, @nPageSize=@nPageSize, @sSortCol=@sSortCol, @sSortDir=@sSortDir, @sFieldType = @sFieldType, @sFieldKey=@sFieldKey, @sGroupKey=@sGroupKey, @sParentKey=@sParentKey";
            var dt = new DataTablesBase(o, _dbClient);
            if (!dt.UseOrderedPaging)
            {
                sQuery = $"EXEC [dbo].[sp_GetLocalizedContentFieldsDt] @sSearch=@sSearch, @nRowStart=0, @nPageSize=1000, @sSortCol=@sSortCol, @sSortDir=@sSortDir, @sFieldType = @sFieldType, @sFieldKey=@sFieldKey, @sGroupKey=@sGroupKey, @sParentKey=@sParentKey";
            }

            dt.AddExtraParams("sSortCol", o?.columns?.ElementAtOrDefault(o?.order?.FirstOrDefault()?.column ?? 0)?.data);
            dt.AddExtraParams("sSortDir", o?.order?.FirstOrDefault()?.dir);

            dt.AddExtraParams("sFieldType", o.FieldType);
            dt.AddExtraParams("sFieldKey", o.FieldKey);
            dt.AddExtraParams("sGroupKey", o.GroupKey);
            dt.AddExtraParams("sParentKey", o.ParentKey);

            dt.SetDebug(false);

            return dt.FetchQuery(_connString, sQuery);
        }


    }
} 

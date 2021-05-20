using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Jlw.Utilities.Data;
using Jlw.Utilities.Data.DataTables;
using Jlw.Utilities.Data.DbUtility;

namespace Jlw.Data.LocalizedContent 
{ 
    public class LocalizedGroupDataItemRepository : ModularDataRepositoryBase<ILocalizedGroupDataItem, LocalizedGroupDataItem>, ILocalizedGroupDataItemRepository 
    { 
        protected const string SpGetRecord = "sp_GetLocalizedGroupDataItemRecord"; 
        protected const string SpSaveRecord = "sp_SaveLocalizedGroupDataItemRecord"; 
        protected const string SpDeleteRecord = "sp_DeleteLocalizedGroupDataItemRecord"; 
        protected const string SpListRecord = "sp_GetLocalizedGroupDataItemList"; 
 
        public LocalizedGroupDataItemRepository(IModularDbClient dbClient, string connString) : base(dbClient, connString) 
        { 
            _sGetRecord = SpGetRecord; 
            _sDeleteRecord = SpDeleteRecord; 
            _sSaveRecord = SpSaveRecord; 
            _sGetAllRecords = SpListRecord;
        } 
 
        protected override IEnumerable<KeyValuePair<string, object>> GetParamsForSql(ILocalizedGroupDataItem o, string sSql) 
        { 
            switch (sSql) 
            { 
                case SpSaveRecord:
                    return new KeyValuePair<string, object>[] {
                        new KeyValuePair<string, object>("@id", "Id"),
                        new KeyValuePair<string, object>("@language", "Language"),
                        new KeyValuePair<string, object>("@groupkey", "GroupKey"),
                        new KeyValuePair<string, object>("@key", "Key"),
                        new KeyValuePair<string, object>("@value", "Value"),
                        new KeyValuePair<string, object>("@order", "Order"),
                        new KeyValuePair<string, object>("@description", "Description"),
                        new KeyValuePair<string, object>("@data", "Data"),
                        new KeyValuePair<string, object>("@auditchangeby", "AuditChangeBy"),
                    };
                case SpDeleteRecord:
                    return new KeyValuePair<string, object>[] {
                        new KeyValuePair<string, object>("@id", "Id"),
                        new KeyValuePair<string, object>("@auditchangeby", "AuditChangeBy")
                    };
                case SpGetRecord: 
                    return new KeyValuePair<string, object>[] { 
                        new KeyValuePair<string, object>("@id", "Id"), 
                    }; 
//                case SpListRecord: 
//                    return new KeyValuePair<string, object>[] { }; 
            }

            var t = new StackTrace();
            var m = t.GetFrame(1).GetMethod();
            string typename = DataUtility.GetTypeName(m.ReflectedType).Trim(',', ' ');
            throw new NotImplementedException($"The method {typename}.{m.Name}({DataUtility.GetTypeArgs(m.GetParameters().Select(p => p.ParameterType).ToArray())}) is not implemented at this time.");
            //return base.GetParamsForSql(o, sSql); 
        }

        public object GetDataTableList(LocalizedGroupDataItemDataTablesInput o)
        {
            string sQuery = $"EXEC [dbo].[sp_GetLocalizedGroupDataItemsDt] @sSearch=@sSearch, @nRowStart=@nRowStart, @nPageSize=@nPageSize, @sSortCol=@sSortCol, @sSortDir=@sSortDir";
            var dt = new DataTablesBase(o, _dbClient);
            dt.AddExtraParams("sSortCol", o?.columns?.ElementAtOrDefault(o?.order?.FirstOrDefault()?.column ?? 0)?.data);
            dt.AddExtraParams("sSortDir", o?.order?.FirstOrDefault()?.dir);
            dt.SetDebug(false);
            return dt.FetchQuery(_connString, sQuery);
        }

    }
} 

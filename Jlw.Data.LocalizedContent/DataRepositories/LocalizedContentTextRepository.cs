using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Jlw.Utilities.Data;
using Jlw.Utilities.Data.DataTables;
using Jlw.Utilities.Data.DbUtility;

namespace Jlw.Data.LocalizedContent 
{ 
    public class LocalizedContentTextRepository : ModularDataRepositoryBase<ILocalizedContentText, LocalizedContentText>, ILocalizedContentTextRepository 
    { 
        protected const string SpGetRecord = "sp_GetLocalizedContentTextRecord"; 
        protected const string SpSaveRecord = "sp_SaveLocalizedContentTextRecord"; 
        protected const string SpDeleteRecord = "sp_DeleteLocalizedContentTextRecord"; 
        protected const string SpListRecord = "sp_GetLocalizedContentTextList"; 
 
        public LocalizedContentTextRepository(IModularDbClient dbClient, string connString) : base(dbClient, connString) 
        { 
            _sGetRecord = SpGetRecord; 
            _sDeleteRecord = SpDeleteRecord; 
            _sSaveRecord = SpSaveRecord; 
            _sGetAllRecords = SpListRecord; 
        } 
 
        protected override IEnumerable<KeyValuePair<string, object>> GetParamsForSql(ILocalizedContentText o, string sSql) 
        { 
            switch (sSql) 
            { 
                case SpSaveRecord:
                    return new KeyValuePair<string, object>[] {
                        new KeyValuePair<string, object>("@groupKey", "GroupKey"),
                        new KeyValuePair<string, object>("@fieldKey", "FieldKey"),
                        new KeyValuePair<string, object>("@language", "Language"),
                        new KeyValuePair<string, object>("@text", "Text"),
                        new KeyValuePair<string, object>("@auditchangeby", "AuditChangeBy"),
                    };

                case SpDeleteRecord:
                    return new KeyValuePair<string, object>[] {
                        new KeyValuePair<string, object>("@groupKey", "GroupKey"),
                        new KeyValuePair<string, object>("@fieldKey", "FieldKey"),
                        new KeyValuePair<string, object>("@language", "language"),
                        new KeyValuePair<string, object>("@auditchangeby", "AuditChangeBy"),
                    };
                case SpGetRecord: 
                    return new KeyValuePair<string, object>[] { 
                        new KeyValuePair<string, object>("@groupKey", "GroupKey"),
                        new KeyValuePair<string, object>("@fieldKey", "FieldKey"),
                        new KeyValuePair<string, object>("@language", "language"),
                    }; 
                //case SpListRecord: 
                //    return new KeyValuePair<string, object>[] { }; 
            }

            //return base.GetParamsForSql(o, sSql); 

            var t = new StackTrace();
            var m = t.GetFrame(1).GetMethod();
            string typename = DataUtility.GetTypeName(m.ReflectedType).Trim(',', ' ');
            throw new NotImplementedException($"The method {typename}.{m.Name}({DataUtility.GetTypeArgs(m.GetParameters().Select(p => p.ParameterType).ToArray())}) is not implemented at this time.");
        }

        public object GetDataTableList(LocalizedContentTextDataTablesInput o)
        {
            string sQuery = $"EXEC [dbo].[sp_GetLocalizedContentTextDt] @sSearch=@sSearch, @nRowStart=@nRowStart, @nPageSize=@nPageSize, @sSortCol=@sSortCol, @sSortDir=@sSortDir, @sGroupKey = @sGroupKey, @sFieldKey = @sFieldKey, @sLanguage = @sLanguage";
            var dt = new DataTablesBase(o, _dbClient);
            if (!dt.UseOrderedPaging)
            {
                sQuery = $"EXEC [dbo].[sp_GetLocalizedContentTextDt] @sSearch=@sSearch, @nRowStart=0, @nPageSize=1000, @sSortCol=@sSortCol, @sSortDir=@sSortDir, @sGroupKey = @sGroupKey, @sFieldKey = @sFieldKey, @sLanguage = @sLanguage";
            }

            dt.AddExtraParams("sSortCol", o?.columns?.ElementAtOrDefault(o?.order?.FirstOrDefault()?.column ?? 0)?.data);
            dt.AddExtraParams("sSortDir", o?.order?.FirstOrDefault()?.dir);

            dt.AddExtraParams("sFieldKey", o?.FieldKey);
            dt.AddExtraParams("sGroupKey", o?.GroupKey);
            dt.AddExtraParams("sLanguage", o?.Language);

            dt.SetDebug(false);
            return dt.FetchQuery(_connString, sQuery);
        }


    }
} 

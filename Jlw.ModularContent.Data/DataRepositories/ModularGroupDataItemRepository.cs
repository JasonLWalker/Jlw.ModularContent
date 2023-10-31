// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-20-2021
//
// Last Modified By : jlwalker
// Last Modified On : 05-15-2023
// ***********************************************************************
// <copyright file="LocalizedGroupDataItemRepository.cs" company="Jason L. Walker">
//     Copyright ©2012-2023 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Jlw.Utilities.Data;
using Jlw.Utilities.Data.DataTables;
using Jlw.Utilities.Data.DbUtility;

namespace Jlw.ModularContent 
{
    /// <summary>
    /// Class LocalizedGroupDataItemRepository.
    /// Implements the <see cref="T:Jlw.Utilities.Data.DbUtility.ModularDataRepositoryBase{Jlw.Data.LocalizedContent.ILocalizedGroupDataItem, Jlw.Data.LocalizedContent.LocalizedGroupDataItem}" />
    /// Implements the <see cref="Jlw.Data.LocalizedContent.ILocalizedGroupDataItemRepository" />
    /// </summary>
    /// <seealso cref="T:Jlw.Utilities.Data.DbUtility.ModularDataRepositoryBase{Jlw.Data.LocalizedContent.ILocalizedGroupDataItem, Jlw.Data.LocalizedContent.LocalizedGroupDataItem}" />
    /// <seealso cref="Jlw.Data.LocalizedContent.ILocalizedGroupDataItemRepository" />
    public class ModularGroupDataItemRepository : ModularDataRepositoryBase<IModularGroupDataItem, ModularGroupDataItem>, IModularGroupDataItemRepository 
    {
        /// <summary>
        /// The stored procedure name for the get record
        /// </summary>
        protected const string SpGetRecord = "sp_GetLocalizedGroupDataItemRecord";
        /// <summary>
        /// The stored procedure name for the save record
        /// </summary>
        protected const string SpSaveRecord = "sp_SaveLocalizedGroupDataItemRecord";
        /// <summary>
        /// The stored procedure name for the delete record
        /// </summary>
        protected const string SpDeleteRecord = "sp_DeleteLocalizedGroupDataItemRecord";
        /// <summary>
        /// The stored procedure name for the list record
        /// </summary>
        protected const string SpListRecord = "sp_GetLocalizedGroupDataItemList";

        /// <summary>
        /// Initializes a new instance of the <see cref="ModularGroupDataItemRepository"/> class.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        /// <param name="connString">The connection string.</param>
        public ModularGroupDataItemRepository(IModularDbClient dbClient, string connString) : base(dbClient, connString) 
        { 
            _sGetRecord = SpGetRecord; 
            _sDeleteRecord = SpDeleteRecord; 
            _sSaveRecord = SpSaveRecord; 
            _sGetAllRecords = SpListRecord;
        }

        /// <summary>
        /// Gets the parameters for SQL.
        /// </summary>
        /// <param name="o">The reference object.</param>
        /// <param name="sSql">The s SQL.</param>
        /// <returns>IEnumerable&lt;KeyValuePair&lt;System.String, System.Object&gt;&gt;.</returns>
        /// <exception cref="System.NotImplementedException">The method {typename}.{m.Name}({DataUtility.GetTypeArgs(m.GetParameters().Select(p => p.ParameterType).ToArray())}) is not implemented at this time.</exception>
        protected override IEnumerable<KeyValuePair<string, object>> GetParamsForSql(IModularGroupDataItem o, string sSql) 
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
            }

            var t = new StackTrace();
            var m = t.GetFrame(1).GetMethod();
            string typename = DataUtility.GetTypeName(m.ReflectedType).Trim(',', ' ');
            throw new NotImplementedException($"The method {typename}.{m.Name}({DataUtility.GetTypeArgs(m.GetParameters().Select(p => p.ParameterType).ToArray())}) is not implemented at this time.");
        }

        /// <inheritdoc />
        public object GetDataTableList(ModularGroupDataItemDataTablesInput o)
        {
            string sQuery = $"EXEC [dbo].[sp_GetLocalizedGroupDataItemsDt] @sSearch=@sSearch, @nRowStart=@nRowStart, @nPageSize=@nPageSize, @sSortCol=@sSortCol, @sSortDir=@sSortDir, @sGroupKey=@sGroupKey, @sGroupFilter=@sGroupFilter";
            var dt = new DataTablesBase(o, _dbClient);
            dt.AddExtraParams("sSortCol", o?.columns?.ElementAtOrDefault(o?.order?.FirstOrDefault()?.column ?? 0)?.data);
            dt.AddExtraParams("sSortDir", o?.order?.FirstOrDefault()?.dir);
            dt.AddExtraParams("sGroupKey", o.GroupKey);
            dt.AddExtraParams("sGroupFilter", string.IsNullOrWhiteSpace(o.GroupFilter) ? null : o.GroupFilter);
            dt.SetDebug(false);
            return dt.FetchQuery(_connString, sQuery);
        }

        /// <inheritdoc />
        public T GetItemValue<T>(string groupKey, string key)
        {
            
            if (_dbClient != null)
            {
                return _dbClient.GetRecordObject<T>(
                    null,
                    this._connString,
                    new RepositoryMethodDefinition<T>("sp_GetLocalizedGroupDataItemValue",
                        CommandType.StoredProcedure,
                        new KeyValuePair<string, object>[]
                        {
                            new KeyValuePair<string, object>("groupKey", groupKey),
                            new KeyValuePair<string, object>("key", key)
                        }, o => DataUtility.Parse<T>(o, "Value")
                    ));
            }

            return default;
        }

        /// <inheritdoc />
        public IEnumerable<IModularGroupDataItem> GetItems(string groupKey, string language=null)
        {
            if (_dbClient != null)
            {
                return _dbClient.GetRecordList<IModularGroupDataItem>(
                    null,
                    this._connString,
                    new RepositoryMethodDefinition<IModularGroupDataItem>("sp_GetLocalizedGroupDataItems",
                        CommandType.StoredProcedure,
                        new KeyValuePair<string, object>[]
                        {
                            new KeyValuePair<string, object>("groupKey", groupKey ?? ""),
                            new KeyValuePair<string, object>("lang", language ?? "")
                        }, o =>
                        {
                            return new ModularGroupDataItem(o);
                        }));
            }

            return new List<IModularGroupDataItem>();
        }
    }
} 

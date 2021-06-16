// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-20-2021
//
// Last Modified By : jlwalker
// Last Modified On : 06-15-2021
// ***********************************************************************
// <copyright file="LocalizedGroupDataItemRepository.cs" company="Jason L. Walker">
//     Copyright �2012-2021 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Jlw.Utilities.Data;
using Jlw.Utilities.Data.DataTables;
using Jlw.Utilities.Data.DbUtility;

namespace Jlw.Data.LocalizedContent 
{
    /// <summary>
    /// Class LocalizedGroupDataItemRepository.
    /// Implements the <see cref="T:Jlw.Utilities.Data.DbUtility.ModularDataRepositoryBase{Jlw.Data.LocalizedContent.ILocalizedGroupDataItem, Jlw.Data.LocalizedContent.LocalizedGroupDataItem}" />
    /// Implements the <see cref="Jlw.Data.LocalizedContent.ILocalizedGroupDataItemRepository" />
    /// </summary>
    /// <seealso cref="T:Jlw.Utilities.Data.DbUtility.ModularDataRepositoryBase{Jlw.Data.LocalizedContent.ILocalizedGroupDataItem, Jlw.Data.LocalizedContent.LocalizedGroupDataItem}" />
    /// <seealso cref="Jlw.Data.LocalizedContent.ILocalizedGroupDataItemRepository" />
    /// TODO Edit XML Comment Template for LocalizedGroupDataItemRepository
    public class LocalizedGroupDataItemRepository : ModularDataRepositoryBase<ILocalizedGroupDataItem, LocalizedGroupDataItem>, ILocalizedGroupDataItemRepository 
    {
        /// <summary>
        /// The sp get record
        /// </summary>
        /// TODO Edit XML Comment Template for SpGetRecord
        protected const string SpGetRecord = "sp_GetLocalizedGroupDataItemRecord";
        /// <summary>
        /// The sp save record
        /// </summary>
        /// TODO Edit XML Comment Template for SpSaveRecord
        protected const string SpSaveRecord = "sp_SaveLocalizedGroupDataItemRecord";
        /// <summary>
        /// The sp delete record
        /// </summary>
        /// TODO Edit XML Comment Template for SpDeleteRecord
        protected const string SpDeleteRecord = "sp_DeleteLocalizedGroupDataItemRecord";
        /// <summary>
        /// The sp list record
        /// </summary>
        /// TODO Edit XML Comment Template for SpListRecord
        protected const string SpListRecord = "sp_GetLocalizedGroupDataItemList";

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedGroupDataItemRepository"/> class.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        /// <param name="connString">The connection string.</param>
        /// TODO Edit XML Comment Template for #ctor
        public LocalizedGroupDataItemRepository(IModularDbClient dbClient, string connString) : base(dbClient, connString) 
        { 
            _sGetRecord = SpGetRecord; 
            _sDeleteRecord = SpDeleteRecord; 
            _sSaveRecord = SpSaveRecord; 
            _sGetAllRecords = SpListRecord;
        }

        /// <summary>
        /// Gets the parameters for SQL.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="sSql">The s SQL.</param>
        /// <returns>IEnumerable&lt;KeyValuePair&lt;System.String, System.Object&gt;&gt;.</returns>
        /// <exception cref="System.NotImplementedException">The method {typename}.{m.Name}({DataUtility.GetTypeArgs(m.GetParameters().Select(p => p.ParameterType).ToArray())}) is not implemented at this time.</exception>
        /// TODO Edit XML Comment Template for GetParamsForSql
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

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for GetDataTableList
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

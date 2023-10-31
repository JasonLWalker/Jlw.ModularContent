// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-20-2021
//
// Last Modified By : jlwalker
// Last Modified On : 05-15-2023
// ***********************************************************************
// <copyright file="LocalizedContentFieldRepository.cs" company="Jason L. Walker">
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
    /// Class LocalizedContentFieldRepository.
    /// Implements the <see cref="T:Jlw.Utilities.Data.DbUtility.ModularDataRepositoryBase{Jlw.Data.LocalizedContent.ILocalizedContentField, Jlw.Data.LocalizedContent.LocalizedContentField}" />
    /// Implements the <see cref="Jlw.Data.LocalizedContent.ILocalizedContentFieldRepository" />
    /// </summary>
    /// <seealso cref="T:Jlw.Utilities.Data.DbUtility.ModularDataRepositoryBase{Jlw.Data.LocalizedContent.ILocalizedContentField, Jlw.Data.LocalizedContent.LocalizedContentField}" />
    /// <seealso cref="Jlw.Data.LocalizedContent.ILocalizedContentFieldRepository" />
    /// TODO Edit XML Comment Template for LocalizedContentFieldRepository
    public class ModularContentFieldRepository : ModularDataRepositoryBase<IModularContentField, ModularContentField>, IModularContentFieldRepository 
    {
        /// <summary>
        /// The sp get record
        /// </summary>
        /// TODO Edit XML Comment Template for SpGetRecord
        protected const string SpGetRecord = "sp_GetLocalizedContentFieldRecord";
        /// <summary>
        /// The sp save record
        /// </summary>
        /// TODO Edit XML Comment Template for SpSaveRecord
        protected const string SpSaveRecord = "sp_SaveLocalizedContentFieldRecord";
        /// <summary>
        /// The sp delete record
        /// </summary>
        /// TODO Edit XML Comment Template for SpDeleteRecord
        protected const string SpDeleteRecord = "sp_DeleteLocalizedContentFieldRecord";
        /// <summary>
        /// The sp list record
        /// </summary>
        /// TODO Edit XML Comment Template for SpListRecord
        protected const string SpListRecord = "sp_GetLocalizedContentFieldList";

        /// <summary>
        /// Initializes a new instance of the <see cref="ModularContentFieldRepository"/> class.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        /// <param name="connString">The connection string.</param>
        /// TODO Edit XML Comment Template for #ctor
        public ModularContentFieldRepository(IModularDbClient dbClient, string connString) : base(dbClient, connString) 
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
        protected override IEnumerable<KeyValuePair<string, object>> GetParamsForSql(IModularContentField o, string sSql) 
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
                        new KeyValuePair<string, object>("@groupfilter", "GroupFilter"),
                    };
                case SpDeleteRecord:
                    return new KeyValuePair<string, object>[] {
                        new KeyValuePair<string, object>("@id", "Id"),
                        new KeyValuePair<string, object>("@auditchangeby", "AuditChangeBy"),
                        new KeyValuePair<string, object>("@groupfilter", "GroupFilter"),
                    };
                case SpGetRecord: 
                    return new KeyValuePair<string, object>[] { 
                        new KeyValuePair<string, object>("@id", "Id"),
                        new KeyValuePair<string, object>("@groupfilter", "GroupFilter"),
                    }; 
                //case SpListRecord: 
                //    return new KeyValuePair<string, object>[] { }; 
            }

            var t = new StackTrace();
            var m = t.GetFrame(1).GetMethod();
            string typename = DataUtility.GetTypeName(m.ReflectedType).Trim(',', ' ');
            throw new NotImplementedException($"The method {typename}.{m.Name}({DataUtility.GetTypeArgs(m.GetParameters().Select(p => p.ParameterType).ToArray())}) is not implemented at this time.");
        }

        /// <inheritdoc />
        public IModularContentField GetRecordByName(IModularContentField o)
        {
            return _dbClient.GetRecordObject<ModularContentField>(null, _connString, new RepositoryMethodDefinition("sp_GetLocalizedContentFieldRecordByName", CommandType.StoredProcedure, new[]
            {
                new KeyValuePair<string, object>("fieldKey", o?.FieldKey ?? ""),
                new KeyValuePair<string, object>("fieldType", o?.FieldType ?? ""),
                new KeyValuePair<string, object>("groupKey", o?.GroupKey ?? ""),
                new KeyValuePair<string, object>("parentKey", o?.ParentKey ?? ""),
                new KeyValuePair<string, object>("groupFilter", o?.GroupFilter ?? ""),
            }));
        }

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for GetDataTableList
        public object GetDataTableList(ModularContentFieldDataTablesInput o)
        {
            string sQuery = $"EXEC [dbo].[sp_GetLocalizedContentFieldsDt] @sSearch=@sSearch, @nRowStart=@nRowStart, @nPageSize=@nPageSize, @sSortCol=@sSortCol, @sSortDir=@sSortDir, @sFieldType = @sFieldType, @sFieldKey=@sFieldKey, @sGroupKey=@sGroupKey, @sParentKey=@sParentKey, @sLanguage=@sLanguage, @sGroupFilter=@sGroupFilter";
            var dt = new DataTablesBase(o, _dbClient);
//            if (!dt.UseOrderedPaging)
//            {
//                sQuery = $"EXEC [dbo].[sp_GetLocalizedContentFieldsDt] @sSearch=@sSearch, @nRowStart=0, @nPageSize=1000, @sSortCol=@sSortCol, @sSortDir=@sSortDir, @sFieldType = @sFieldType, @sFieldKey=@sFieldKey, @sGroupKey=@sGroupKey, @sParentKey=@sParentKey";
//            }

            dt.AddExtraParams("sSortCol", o?.columns?.ElementAtOrDefault(o?.order?.FirstOrDefault()?.column ?? 0)?.data);
            dt.AddExtraParams("sSortDir", o?.order?.FirstOrDefault()?.dir);

            dt.AddExtraParams("sFieldType", o.FieldType);
            dt.AddExtraParams("sFieldKey", o.FieldKey);
            dt.AddExtraParams("sGroupKey", o.GroupKey);
            dt.AddExtraParams("sParentKey", o.ParentKey);
            dt.AddExtraParams("sLanguage", o.Language);
            dt.AddExtraParams("sGroupFilter", string.IsNullOrWhiteSpace(o.GroupFilter) ? null : o.GroupFilter);

            dt.SetDebug(false);

            return dt.FetchQuery(_connString, sQuery);
        }


    }
} 

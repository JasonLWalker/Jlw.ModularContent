// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-20-2021
//
// Last Modified By : jlwalker
// Last Modified On : 05-15-2023
// ***********************************************************************
// <copyright file="LocalizedContentTextRepository.cs" company="Jason L. Walker">
//     Copyright ©2012-2023 Jason L. Walker
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

namespace Jlw.ModularContent 
{
    /// <summary>
    /// Class LocalizedContentTextRepository.
    /// Implements the <see cref="T:Jlw.Utilities.Data.DbUtility.ModularDataRepositoryBase{Jlw.Data.LocalizedContent.ILocalizedContentText, Jlw.Data.LocalizedContent.LocalizedContentText}" />
    /// Implements the <see cref="Jlw.Data.LocalizedContent.ILocalizedContentTextRepository" />
    /// </summary>
    /// <seealso cref="T:Jlw.Utilities.Data.DbUtility.ModularDataRepositoryBase{Jlw.Data.LocalizedContent.ILocalizedContentText, Jlw.Data.LocalizedContent.LocalizedContentText}" />
    /// <seealso cref="Jlw.Data.LocalizedContent.ILocalizedContentTextRepository" />
    /// TODO Edit XML Comment Template for LocalizedContentTextRepository
    public class LocalizedContentTextRepository : ModularDataRepositoryBase<IModularContentText, ModularContentText>, IModularContentTextRepository 
    {
        /// <summary>
        /// The sp get record
        /// </summary>
        /// TODO Edit XML Comment Template for SpGetRecord
        protected const string SpGetRecord = "sp_GetLocalizedContentTextRecord";
        /// <summary>
        /// The sp save record
        /// </summary>
        /// TODO Edit XML Comment Template for SpSaveRecord
        protected const string SpSaveRecord = "sp_SaveLocalizedContentTextRecord";
        /// <summary>
        /// The sp delete record
        /// </summary>
        /// TODO Edit XML Comment Template for SpDeleteRecord
        protected const string SpDeleteRecord = "sp_DeleteLocalizedContentTextRecord";
        /// <summary>
        /// The sp list record
        /// </summary>
        /// TODO Edit XML Comment Template for SpListRecord
        protected const string SpListRecord = "sp_GetLocalizedContentTextList";

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedContentTextRepository"/> class.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        /// <param name="connString">The connection string.</param>
        /// TODO Edit XML Comment Template for #ctor
        public LocalizedContentTextRepository(IModularDbClient dbClient, string connString) : base(dbClient, connString) 
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
        protected override IEnumerable<KeyValuePair<string, object>> GetParamsForSql(IModularContentText o, string sSql) 
        { 
            switch (sSql) 
            { 
                case SpSaveRecord:
                    return new KeyValuePair<string, object>[] {
                        new KeyValuePair<string, object>("@groupKey", "GroupKey"),
                        new KeyValuePair<string, object>("@fieldKey", "FieldKey"),
                        new KeyValuePair<string, object>("@parentKey", "ParentKey"),
                        new KeyValuePair<string, object>("@language", "Language"),
                        new KeyValuePair<string, object>("@text", "Text"),
                        new KeyValuePair<string, object>("@auditchangeby", "AuditChangeBy"),
                    };

                case SpDeleteRecord:
                    return new KeyValuePair<string, object>[] {
                        new KeyValuePair<string, object>("@groupKey", "GroupKey"),
                        new KeyValuePair<string, object>("@fieldKey", "FieldKey"),
                        new KeyValuePair<string, object>("@parentKey", "ParentKey"),
                        new KeyValuePair<string, object>("@language", "language"),
                        new KeyValuePair<string, object>("@auditchangeby", "AuditChangeBy"),
                    };
                case SpGetRecord: 
                    return new KeyValuePair<string, object>[] { 
                        new KeyValuePair<string, object>("@groupKey", "GroupKey"),
                        new KeyValuePair<string, object>("@fieldKey", "FieldKey"),
                        new KeyValuePair<string, object>("@parentKey", "ParentKey"),
                        new KeyValuePair<string, object>("@language", "language"),
                    }; 
            }

            var t = new StackTrace();
            var m = t.GetFrame(1).GetMethod();
            string typename = DataUtility.GetTypeName(m.ReflectedType).Trim(',', ' ');
            throw new NotImplementedException($"The method {typename}.{m.Name}({DataUtility.GetTypeArgs(m.GetParameters().Select(p => p.ParameterType).ToArray())}) is not implemented at this time.");
        }

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for GetDataTableList
        public object GetDataTableList(LocalizedContentTextDataTablesInput o)
        {
            string sQuery = $"EXEC [dbo].[sp_GetLocalizedContentTextDt] @sSearch=@sSearch, @nRowStart=@nRowStart, @nPageSize=@nPageSize, @sSortCol=@sSortCol, @sSortDir=@sSortDir, @sGroupKey = @sGroupKey, @sFieldKey = @sFieldKey, @sLanguage = @sLanguage, @sParentKey=@sParentKey";
            var dt = new DataTablesBase(o, _dbClient);
            if (!dt.UseOrderedPaging)
            {
                sQuery = $"EXEC [dbo].[sp_GetLocalizedContentTextDt] @sSearch=@sSearch, @nRowStart=0, @nPageSize=1000, @sSortCol=@sSortCol, @sSortDir=@sSortDir, @sGroupKey = @sGroupKey, @sFieldKey = @sFieldKey, @sLanguage = @sLanguage, @sParentKey=@sParentKey";
            }

            dt.AddExtraParams("sSortCol", o?.columns?.ElementAtOrDefault(o?.order?.FirstOrDefault()?.column ?? 0)?.data);
            dt.AddExtraParams("sSortDir", o?.order?.FirstOrDefault()?.dir);

            dt.AddExtraParams("sFieldKey", o?.FieldKey);
            dt.AddExtraParams("sParentKey", o?.ParentKey);
            dt.AddExtraParams("sGroupKey", o?.GroupKey);
            dt.AddExtraParams("sLanguage", o?.Language);

            dt.SetDebug(false);
            return dt.FetchQuery(_connString, sQuery);
        }


    }
} 

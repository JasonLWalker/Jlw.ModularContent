// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-27-2021
//
// Last Modified By : jlwalker
// Last Modified On : 05-15-2023
// ***********************************************************************
// <copyright file="WizardFactoryRepository.cs" company="Jason L. Walker">
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
using Jlw.Utilities.Data.DbUtility;

namespace Jlw.ModularContent
{
    /// <summary>
    /// Class WizardFactoryRepository.
    /// Implements the <see cref="T:Jlw.Utilities.Data.DbUtility.ModularDataRepositoryBase{Jlw.Data.LocalizedContent.ILocalizedContentField, Jlw.Data.LocalizedContent.LocalizedContentField}" />
    /// Implements the <see cref="Jlw.Data.LocalizedContent.IWizardFactoryRepository" />
    /// </summary>
    /// <seealso cref="T:Jlw.Utilities.Data.DbUtility.ModularDataRepositoryBase{Jlw.Data.LocalizedContent.ILocalizedContentField, Jlw.Data.LocalizedContent.LocalizedContentField}" />
    /// <seealso cref="Jlw.Data.LocalizedContent.IWizardFactoryRepository" />
    /// TODO Edit XML Comment Template for WizardFactoryRepository
    public class WizardFactoryRepository : ModularDataRepositoryBase<IModularWizardContentField, WizardContentField>, IWizardFactoryRepository
    {
        /// <summary> Stored procedure to retrieve a single record </summary>
        protected const string SpGetRecord = "sp_GetWizardContentFieldRecord";


        /// <summary>
        /// Initializes a new instance of the <see cref="WizardFactoryRepository"/> class.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        /// <param name="connString">The connection string.</param>
        public WizardFactoryRepository(IModularDbClient dbClient, string connString) : base(dbClient, connString)
        {
            _sGetRecord = SpGetRecord;
        }


        /// <inheritdoc />
        protected override IEnumerable<KeyValuePair<string, object>> GetParamsForSql(IModularWizardContentField o, string sSql)
        {
            switch (sSql)
            {
                case SpGetRecord:
                    return new KeyValuePair<string, object>[] {
                        new KeyValuePair<string, object>("@id", "Id"),
                        new KeyValuePair<string, object>("@language", "Language"),
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
        public IEnumerable<WizardContentField> GetFieldData(string groupKey)
        {
            if (string.IsNullOrWhiteSpace(groupKey))
                return new WizardContentField[] { };

            return _dbClient.GetRecordList<WizardContentField>(groupKey, _connString, new RepositoryMethodDefinition("sp_GetFormFields", CommandType.StoredProcedure, new[] { "groupKey" }));
        }


        /// <inheritdoc />
        public IModularWizardContentField SaveFieldParentOrder(WizardContentField fieldData)
        {
            return _dbClient.GetRecordObject<WizardContentField>(fieldData, _connString, new RepositoryMethodDefinition("sp_SaveLocalizedContentFieldParentOrder", CommandType.StoredProcedure, new[] { "Id", "ParentKey", "Order", "AuditChangeBy" }));
        }

        /// <inheritdoc />
        public IModularWizardContentField SaveFieldData(WizardFieldUpdateData fieldData)
        {

            return _dbClient.GetRecordObject<WizardContentField>(fieldData, _connString, new RepositoryMethodDefinition("sp_SaveLocalizedContentFieldData", CommandType.StoredProcedure, new[] { "Id", "FieldName", "FieldValue", "AuditChangeBy", "GroupFilter" }));
        }

        /// <inheritdoc />
        public IEnumerable<WizardContentField> GetWizardFields(string groupKey, string parentKey, string language, string groupFilter)
        {
            return _dbClient.GetRecordList<WizardContentField>(new {groupKey = groupKey ?? "", parentKey, groupFilter, language }, _connString, new RepositoryMethodDefinition("sp_GetWizardFields", CommandType.StoredProcedure, new[] { "groupKey", "parentKey", "language", "groupFilter" }));
        }

        /// <inheritdoc />
        public IEnumerable<WizardContentField> GetWizardFields(string groupKey, string groupFilter = null) => GetWizardFields(groupKey, null, null, groupFilter);

        /// <inheritdoc />
        public IEnumerable<WizardContentField> GetWizardFields(string groupKey, string language, string groupFilter) => GetWizardFields(groupKey, null, language, groupFilter);

        public IEnumerable<IModularContentText> GetLanguageValues(string groupKey)
        {
            return _dbClient.GetRecordList<ModularContentText>(new { nRowStart=0, nPageSize=100000, sGroupKey=groupKey}, _connString, new RepositoryMethodDefinition("sp_GetLocalizedContentTextDt", CommandType.StoredProcedure, new[] { "nRowStart", "nPageSize", "sGroupKey" }));

        }

        /// <inheritdoc />
        public IEnumerable<IWizardSideNavItem> GetWizardSideNavData(string groupKey, string language=null, string groupFilter=null)
        {
            return _dbClient.GetRecordList<WizardSideNavItem>(null, _connString, new RepositoryMethodDefinition(
                "sp_GetWizardSideNavData",
                CommandType.StoredProcedure,
                new KeyValuePair<string, object>[] {
                    new KeyValuePair<string, object>("groupKey", groupKey ?? ""),
                    new KeyValuePair<string, object>("language", language?? ""),
                    new KeyValuePair<string, object>("groupFilter", groupFilter ?? ""),
                }));

        }

        /// <inheritdoc />
        public IEnumerable<string> GetWizardModelFields(string groupKey, string groupFilter)
        {
            return _dbClient.GetRecordList<string>(null, _connString, new RepositoryMethodDefinition(
                "sp_GetWizardModelFields", 
                CommandType.StoredProcedure, 
                new KeyValuePair<string, object>[] {
                    new KeyValuePair<string, object>("groupKey", groupKey),
                    new KeyValuePair<string, object>("groupFilter", groupFilter),
                }, 
                o => DataUtility.ParseString(o, "value")));
        }


        /// <inheritdoc />
        public IEnumerable<WizardComponentField> GetComponentList(string groupKey)
        {
            return _dbClient.GetRecordList<WizardComponentField>(groupKey ?? "", _connString, new RepositoryMethodDefinition("sp_GetComponentList", CommandType.StoredProcedure, new[] { "groupKey" }));
        }

        /// <inheritdoc />
        public WizardContentField DeleteWizardFieldRecursive(WizardContentField fieldData, int recurseDepth = 5, string langFilter = null)
        {
            return _dbClient.GetRecordObject<WizardContentField>(
                null, 
                _connString, 
                new RepositoryMethodDefinition(
                    "sp_DeleteWizardFieldRecursive", 
                    CommandType.StoredProcedure, 
                    new KeyValuePair<string, object>[]
                    {
                        new KeyValuePair<string, object>("id", fieldData.Id),
                        new KeyValuePair<string, object>("baseType", fieldData.FieldType),
                        new KeyValuePair<string, object>("auditchangeby", fieldData.AuditChangeBy),
                        new KeyValuePair<string, object>("groupfilter", fieldData.GroupFilter),
                        new KeyValuePair<string, object>("recurseDepth", recurseDepth),
                        new KeyValuePair<string, object>("langFilter", langFilter ?? "")
                    }
                )
            );
        }

        /// <inheritdoc />
        public WizardContentField RenameWizardFieldRecursive(WizardContentField fieldData, string newFieldKey, int recurseDepth = 5, string langFilter = null)
        {
            return _dbClient.GetRecordObject<WizardContentField>(
                null,
                _connString,
                new RepositoryMethodDefinition(
                    "sp_RenameWizardFieldRecursive",
                    CommandType.StoredProcedure,
                    new KeyValuePair<string, object>[]
                    {
                        new KeyValuePair<string, object>("id", fieldData.Id),
                        new KeyValuePair<string, object>("newFieldKey", newFieldKey),
                        new KeyValuePair<string, object>("baseType", fieldData.FieldType),
                        new KeyValuePair<string, object>("auditchangeby", fieldData.AuditChangeBy),
                        new KeyValuePair<string, object>("groupfilter", fieldData.GroupFilter),
                        new KeyValuePair<string, object>("recurseDepth", recurseDepth),
                        new KeyValuePair<string, object>("langFilter", langFilter ?? "")
                    }
                )
            );
        }

        /// <inheritdoc />
        public WizardContentField DuplicateWizardFieldRecursive(WizardContentField fieldData, string newFieldKey, int recurseDepth = 5, string langFilter = null)
        {
            return _dbClient.GetRecordObject<WizardContentField>(
                null,
                _connString,
                new RepositoryMethodDefinition(
                    "sp_DuplicateWizardFieldRecursive",
                    CommandType.StoredProcedure,
                    new KeyValuePair<string, object>[]
                    {
                        new KeyValuePair<string, object>("id", fieldData.Id),
                        new KeyValuePair<string, object>("setFieldKey", newFieldKey),
                        new KeyValuePair<string, object>("baseType", fieldData.FieldType),
                        new KeyValuePair<string, object>("auditchangeby", fieldData.AuditChangeBy),
                        new KeyValuePair<string, object>("groupfilter", fieldData.GroupFilter),
                        new KeyValuePair<string, object>("recurseDepth", recurseDepth),
                        new KeyValuePair<string, object>("langFilter", langFilter ?? "")
                    }
                )
            );
        }


    }
} 
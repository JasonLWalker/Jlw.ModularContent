// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-27-2021
//
// Last Modified By : jlwalker
// Last Modified On : 06-15-2021
// ***********************************************************************
// <copyright file="WizardFactoryRepository.cs" company="Jason L. Walker">
//     Copyright �2012-2021 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Data;
using Jlw.Utilities.Data.DbUtility;

namespace Jlw.Data.LocalizedContent
{
    /// <summary>
    /// Class WizardFactoryRepository.
    /// Implements the <see cref="T:Jlw.Utilities.Data.DbUtility.ModularDataRepositoryBase{Jlw.Data.LocalizedContent.ILocalizedContentField, Jlw.Data.LocalizedContent.LocalizedContentField}" />
    /// Implements the <see cref="Jlw.Data.LocalizedContent.IWizardFactoryRepository" />
    /// </summary>
    /// <seealso cref="T:Jlw.Utilities.Data.DbUtility.ModularDataRepositoryBase{Jlw.Data.LocalizedContent.ILocalizedContentField, Jlw.Data.LocalizedContent.LocalizedContentField}" />
    /// <seealso cref="Jlw.Data.LocalizedContent.IWizardFactoryRepository" />
    /// TODO Edit XML Comment Template for WizardFactoryRepository
    public class WizardFactoryRepository : ModularDataRepositoryBase<ILocalizedContentField, LocalizedContentField>, IWizardFactoryRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WizardFactoryRepository"/> class.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        /// <param name="connString">The connection string.</param>
        /// TODO Edit XML Comment Template for #ctor
        public WizardFactoryRepository(IModularDbClient dbClient, string connString) : base(dbClient, connString)
        {
        }

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for GetFieldData
        public IEnumerable<WizardContentField> GetFieldData(string groupKey)
        {
            if (string.IsNullOrWhiteSpace(groupKey))
                return new WizardContentField[] { };

            return _dbClient.GetRecordList<WizardContentField>(groupKey, _connString, new RepositoryMethodDefinition("sp_GetFormFields", CommandType.StoredProcedure, new[] { "groupKey" }));
        }

        public IWizardContentField SaveFieldParentOrder(WizardContentField fieldData)
        {
            return _dbClient.GetRecordObject<WizardContentField>(fieldData, _connString, new RepositoryMethodDefinition("sp_SaveLocalizedContentFieldParentOrder", CommandType.StoredProcedure, new[] { "Id", "ParentKey", "Order", "AuditChangeBy" }));
        }

        public IWizardContentField SaveFieldData(WizardFieldUpdateData fieldData)
        {

            return _dbClient.GetRecordObject<WizardContentField>(fieldData, _connString, new RepositoryMethodDefinition("sp_SaveLocalizedContentFieldData", CommandType.StoredProcedure, new[] { "Id", "FieldName", "FieldValue", "AuditChangeBy", "GroupFilter" }));
        }

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for GetFieldData
        public IEnumerable<WizardContentField> GetWizardFields(string groupKey)
        {
            return _dbClient.GetRecordList<WizardContentField>(groupKey ?? "", _connString, new RepositoryMethodDefinition("sp_GetWizardFields", CommandType.StoredProcedure, new[] { "groupKey" }));
        }

    }
} 
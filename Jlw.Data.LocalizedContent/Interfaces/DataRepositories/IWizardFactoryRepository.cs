// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-27-2021
//
// Last Modified By : jlwalker
// Last Modified On : 06-15-2021
// ***********************************************************************
// <copyright file="IWizardFactoryRepository.cs" company="Jason L. Walker">
//     Copyright ©2012-2021 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using Jlw.Utilities.Data.DbUtility;

namespace Jlw.Data.LocalizedContent
{

    /// <summary>
    /// Interface IWizardFactoryRepository
    /// Implements the <see cref="T:Jlw.Utilities.Data.DbUtility.IModularDataRepositoryBase{Jlw.Data.LocalizedContent.ILocalizedContentField, Jlw.Data.LocalizedContent.LocalizedContentField}" />
    /// </summary>
    /// <seealso cref="T:Jlw.Utilities.Data.DbUtility.IModularDataRepositoryBase{Jlw.Data.LocalizedContent.ILocalizedContentField, Jlw.Data.LocalizedContent.LocalizedContentField}" />
    /// TODO Edit XML Comment Template for IWizardFactoryRepository
    public interface IWizardFactoryRepository : IModularDataRepositoryBase<IWizardContentField, WizardContentField>
    {
        /// <summary>
        /// Gets the field data.
        /// </summary>
        /// <param name="groupKey">The group key.</param>
        /// <returns>IEnumerable&lt;WizardContentField&gt;.</returns>
        /// TODO Edit XML Comment Template for GetFieldData
        IEnumerable<WizardContentField> GetFieldData(string groupKey);

        IWizardContentField SaveFieldParentOrder(WizardContentField fieldData);

        IWizardContentField SaveFieldData(WizardFieldUpdateData fieldData);

        IEnumerable<WizardContentField> GetWizardFields(string groupKey, string groupFilter = null);

        IEnumerable<WizardContentField> GetWizardFields(string groupKey, string language, string groupFilter);

        IEnumerable<WizardContentField> GetWizardFields(string groupKey, string parentKey, string language, string groupFilter);

        IEnumerable<WizardContentField> GetComponentList(string groupKey);

        WizardContentField DeleteWizardFieldRecursive(WizardContentField fieldData, int recurseDepth = 5, string langFilter = null);
    }
}
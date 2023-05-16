// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-27-2021
//
// Last Modified By : jlwalker
// Last Modified On : 05-15-2023
// ***********************************************************************
// <copyright file="IWizardFactoryRepository.cs" company="Jason L. Walker">
//     Copyright ©2012-2023 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using Jlw.Utilities.Data.DbUtility;

namespace Jlw.LocalizedContent
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
        IEnumerable<WizardContentField> GetFieldData(string groupKey);

        /// <summary>
        /// Sets the [ParentKey] and [Order] for the field specified by the [Id] column.
        /// </summary>
        /// <param name="fieldData"></param>
        /// <returns></returns>
        IWizardContentField SaveFieldParentOrder(WizardContentField fieldData);

        /// <summary>
        /// Sets the value of the column specified by the fieldData object
        /// </summary>
        /// <param name="fieldData"></param>
        /// <returns></returns>
        IWizardContentField SaveFieldData(WizardFieldUpdateData fieldData);

        IEnumerable<ILocalizedContentText> GetLanguageValues(string groupKey);

        /// <summary>
        /// Retrieves a list of all fields that match the groupKey in the default language.
        /// </summary>
        /// <param name="groupKey"></param>
        /// <param name="groupFilter"></param>
        /// <returns></returns>
        IEnumerable<WizardContentField> GetWizardFields(string groupKey, string groupFilter = null);

        /// <summary>
        /// Retrieves a list of all fields that match the groupKey, localized into the language specified if it is available, or the default language if not.
        /// </summary>
        /// <param name="groupKey"></param>
        /// <param name="language"></param>
        /// <param name="groupFilter"></param>
        /// <returns></returns>
        IEnumerable<WizardContentField> GetWizardFields(string groupKey, string language, string groupFilter);

        /// <summary>
        /// Retrieves a list of fields that match the groupKey and parentKey, localized into the language specified if it is available, or the default language if not.
        /// </summary>
        /// <param name="groupKey"></param>
        /// <param name="parentKey"></param>
        /// <param name="language"></param>
        /// <param name="groupFilter"></param>
        /// <returns></returns>
        IEnumerable<WizardContentField> GetWizardFields(string groupKey, string parentKey, string language, string groupFilter);

        /// <summary>
        /// Retrieves a list of fields that are defined in the database as components for the associated groupKey
        /// </summary>
        /// <param name="groupKey"></param>
        /// <returns></returns>
        IEnumerable<WizardComponentField> GetComponentList(string groupKey);

        /// <summary>
        /// Retrieves a list of field names that are to be used as the WizardModel for the matching groupKey.
        /// This is the list of fields that will be posted to each wizard screen regardless of whether they appear on the screen or not.
        /// If the screen does not specify all of these fields, then the wizard client engine will add them to the page as hidden form inputs.
        /// </summary>
        /// <param name="groupKey"></param>
        /// <param name="groupFilter"></param>
        /// <returns></returns>
        IEnumerable<string> GetWizardModelFields(string groupKey, string groupFilter);

        /// <summary>
        /// Retrieves a list of side navigation items that will be used to build the list of screens that can be jumped to in the wizard.
        /// </summary>
        /// <param name="groupKey"></param>
        /// <param name="language"></param>
        /// <param name="groupFilter"></param>
        /// <returns></returns>
        public IEnumerable<IWizardSideNavItem> GetWizardSideNavData(string groupKey, string language = null, string groupFilter = null);

        /// <summary>
        /// Recursively delete wizard fields starting with the field that matches fieldData, and descending a maximum depth specified in recurseDepth.
        /// </summary>
        /// <param name="fieldData"></param>
        /// <param name="recurseDepth"></param>
        /// <param name="langFilter"></param>
        /// <returns></returns>
        WizardContentField DeleteWizardFieldRecursive(WizardContentField fieldData, int recurseDepth = 5, string langFilter = null);

        /// <summary>
        /// Recursively duplicate the wizard fields starting with the field that matches fieldData, and descending a maximum depth specified in recurseDepth.
        /// </summary>
        /// <param name="fieldData"></param>
        /// <param name="newFieldKey"></param>
        /// <param name="recurseDepth"></param>
        /// <param name="langFilter"></param>
        /// <returns></returns>
        WizardContentField DuplicateWizardFieldRecursive(WizardContentField fieldData, string newFieldKey, int recurseDepth = 5, string langFilter = null);

        /// <summary>
        /// Rename the field that matches fieldData, and recursively update the parentKey of any child elements, descending a maximum depth specified in recurseDepth.
        /// </summary>
        /// <param name="fieldData"></param>
        /// <param name="newFieldKey"></param>
        /// <param name="recurseDepth"></param>
        /// <param name="langFilter"></param>
        /// <returns></returns>
        WizardContentField RenameWizardFieldRecursive(WizardContentField fieldData, string newFieldKey, int recurseDepth = 5, string langFilter = null);
    }
}
// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-20-2021
//
// Last Modified By : jlwalker
// Last Modified On : 06-15-2021
// ***********************************************************************
// <copyright file="ILocalizedGroupDataItemRepository.cs" company="Jason L. Walker">
//     Copyright ©2012-2021 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using Jlw.Utilities.Data.DbUtility;

namespace Jlw.Data.LocalizedContent 
{
    /// <summary>
    /// Interface ILocalizedGroupDataItemRepository
    /// Implements the <see cref="T:Jlw.Utilities.Data.DbUtility.IModularDataRepositoryBase{Jlw.Data.LocalizedContent.ILocalizedGroupDataItem, Jlw.Data.LocalizedContent.LocalizedGroupDataItem}" />
    /// </summary>
    /// <seealso cref="T:Jlw.Utilities.Data.DbUtility.IModularDataRepositoryBase{Jlw.Data.LocalizedContent.ILocalizedGroupDataItem, Jlw.Data.LocalizedContent.LocalizedGroupDataItem}" />
    /// TODO Edit XML Comment Template for ILocalizedGroupDataItemRepository
    public interface ILocalizedGroupDataItemRepository : IModularDataRepositoryBase<ILocalizedGroupDataItem, LocalizedGroupDataItem>
    {
        /// <summary>
        /// Gets the data table list.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns>System.Object.</returns>
        /// TODO Edit XML Comment Template for GetDataTableList
        object GetDataTableList(LocalizedGroupDataItemDataTablesInput o);

        T GetItemValue<T>(string groupKey, string key);
        IEnumerable<ILocalizedGroupDataItem> GetItems(string groupKey, string language = null);
    }
} 

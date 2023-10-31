// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-20-2021
//
// Last Modified By : jlwalker
// Last Modified On : 05-15-2023
// ***********************************************************************
// <copyright file="ILocalizedGroupDataItemRepository.cs" company="Jason L. Walker">
//     Copyright ©2012-2023 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using Jlw.Utilities.Data.DbUtility;

namespace Jlw.ModularContent 
{
    /// <summary>
    /// Interface ILocalizedGroupDataItemRepository
    /// Implements the <see cref="T:Jlw.Utilities.Data.DbUtility.IModularDataRepositoryBase{Jlw.Data.LocalizedContent.ILocalizedGroupDataItem, Jlw.Data.LocalizedContent.LocalizedGroupDataItem}" />
    /// </summary>
    /// <seealso cref="T:Jlw.Utilities.Data.DbUtility.IModularDataRepositoryBase{Jlw.Data.LocalizedContent.ILocalizedGroupDataItem, Jlw.Data.LocalizedContent.LocalizedGroupDataItem}" />
    public interface IModularGroupDataItemRepository : IModularDataRepositoryBase<ILocalizedGroupDataItem, LocalizedGroupDataItem>
    {
        /// <summary>
        /// Gets the data table list data.
        /// </summary>
        /// <param name="o">The refence object.</param>
        /// <returns>System.Object.</returns>
        object GetDataTableList(LocalizedGroupDataItemDataTablesInput o);

        /// <summary>
        /// Retrieves the Value of matching item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="groupKey"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetItemValue<T>(string groupKey, string key);

        /// <summary>
        /// Retrieves multiple matching items
        /// </summary>
        /// <param name="groupKey"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        IEnumerable<ILocalizedGroupDataItem> GetItems(string groupKey, string language = null);
    }
} 

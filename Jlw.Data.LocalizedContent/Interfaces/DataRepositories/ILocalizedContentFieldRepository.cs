// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-20-2021
//
// Last Modified By : jlwalker
// Last Modified On : 06-15-2021
// ***********************************************************************
// <copyright file="ILocalizedContentFieldRepository.cs" company="Jason L. Walker">
//     Copyright ©2012-2021 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************
using Jlw.Utilities.Data.DbUtility;

namespace Jlw.Data.LocalizedContent 
{
    /// <summary>
    /// Interface ILocalizedContentFieldRepository
    /// Implements the <see cref="T:Jlw.Utilities.Data.DbUtility.IModularDataRepositoryBase{Jlw.Data.LocalizedContent.ILocalizedContentField, Jlw.Data.LocalizedContent.LocalizedContentField}" /> interface
    /// </summary>
    /// <seealso cref="T:Jlw.Utilities.Data.DbUtility.IModularDataRepositoryBase{Jlw.Data.LocalizedContent.ILocalizedContentField, Jlw.Data.LocalizedContent.LocalizedContentField}" />
    /// TODO Edit XML Comment Template for ILocalizedContentFieldRepository
    public interface ILocalizedContentFieldRepository : IModularDataRepositoryBase<ILocalizedContentField, LocalizedContentField>
    {
        /// <summary>
        /// Gets the data table list.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns>System.Object.</returns>
        /// TODO Edit XML Comment Template for GetDataTableList
        object GetDataTableList(LocalizedContentFieldDataTablesInput o);
    }
} 

// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-20-2021
//
// Last Modified By : jlwalker
// Last Modified On : 05-15-2023
// ***********************************************************************
// <copyright file="ILocalizedContentTextRepository.cs" company="Jason L. Walker">
//     Copyright ©2012-2023 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************

using Jlw.Utilities.Data.DbUtility;

namespace Jlw.ModularContent 
{
    /// <summary>
    /// Interface ILocalizedContentTextRepository
    /// Implements the <see cref="T:Jlw.Utilities.Data.DbUtility.IModularDataRepositoryBase{Jlw.Data.LocalizedContent.ILocalizedContentText, Jlw.Data.LocalizedContent.LocalizedContentText}" />
    /// </summary>
    /// <seealso cref="T:Jlw.Utilities.Data.DbUtility.IModularDataRepositoryBase{Jlw.Data.LocalizedContent.ILocalizedContentText, Jlw.Data.LocalizedContent.LocalizedContentText}" />
    /// TODO Edit XML Comment Template for ILocalizedContentTextRepository
    public interface IModularContentTextRepository : IModularDataRepositoryBase<IModularContentText, ModularContentText>
    {
        /// <summary>
        /// Gets the data table list.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns>System.Object.</returns>
        /// TODO Edit XML Comment Template for GetDataTableList
        object GetDataTableList(ModularContentTextDataTablesInput o);
    }
} 

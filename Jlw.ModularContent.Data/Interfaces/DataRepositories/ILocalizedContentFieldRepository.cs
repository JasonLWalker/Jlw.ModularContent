// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-20-2021
//
// Last Modified By : jlwalker
// Last Modified On : 05-15-2023
// ***********************************************************************
// <copyright file="ILocalizedContentFieldRepository.cs" company="Jason L. Walker">
//     Copyright ©2012-2023 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************

using Jlw.Utilities.Data.DbUtility;

namespace Jlw.ModularContent 
{
    /// <summary>
    /// Interface ILocalizedContentFieldRepository
    /// Implements the <see cref="T:Jlw.Utilities.Data.DbUtility.IModularDataRepositoryBase{Jlw.Data.LocalizedContent.ILocalizedContentField, Jlw.Data.LocalizedContent.LocalizedContentField}" /> interface
    /// </summary>
    /// <seealso cref="T:Jlw.Utilities.Data.DbUtility.IModularDataRepositoryBase{Jlw.Data.LocalizedContent.ILocalizedContentField, Jlw.Data.LocalizedContent.LocalizedContentField}" />
    /// TODO Edit XML Comment Template for ILocalizedContentFieldRepository
    public interface ILocalizedContentFieldRepository : IModularDataRepositoryBase<IModularContentField, ModularContentField>
    {
        /// <summary>
        /// Retrieves a field by matching properties rather than by Id number.
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        IModularContentField GetRecordByName(IModularContentField o);

        /// <summary>
        /// Gets the data table list.
        /// </summary>
        /// <param name="o">The input object.</param>
        /// <returns>System.Object.</returns>
        /// TODO Edit XML Comment Template for GetDataTableList
        object GetDataTableList(LocalizedContentFieldDataTablesInput o);
    }
} 

// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-20-2021
//
// Last Modified By : jlwalker
// Last Modified On : 06-15-2021
// ***********************************************************************
// <copyright file="LocalizedContentFieldRepositoryOptions.cs" company="Jason L. Walker">
//     Copyright ©2012-2021 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************
using Jlw.Utilities.Data.DbUtility;

namespace Jlw.Data.LocalizedContent
{
    /// <summary>
    /// Class LocalizedContentFieldRepositoryOptions.
    /// </summary>
    /// TODO Edit XML Comment Template for LocalizedContentFieldRepositoryOptions
    public class LocalizedContentFieldRepositoryOptions
    {
        /// <summary>
        /// Gets or sets the database client.
        /// </summary>
        /// <value>The database client.</value>
        /// TODO Edit XML Comment Template for DbClient
        public IModularDbClient DbClient { get; set; }
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        /// TODO Edit XML Comment Template for ConnectionString
        public string ConnectionString { get; set; }
    }
}
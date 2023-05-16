// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-20-2021
//
// Last Modified By : jlwalker
// Last Modified On : 05-15-2023
// ***********************************************************************
// <copyright file="LocalizedContentFieldDataTablesInput.cs" company="Jason L. Walker">
//     Copyright ©2012-2023 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************

using Jlw.Utilities.Data.DataTables;

namespace Jlw.LocalizedContent
{
    /// <summary>
    /// Class LocalizedContentFieldDataTablesInput.
    /// Implements the <see cref="Jlw.Utilities.Data.DataTables.DataTablesInput" />
    /// </summary>
    /// <seealso cref="Jlw.Utilities.Data.DataTables.DataTablesInput" />
    /// TODO Edit XML Comment Template for LocalizedContentFieldDataTablesInput
    public class LocalizedContentFieldDataTablesInput : DataTablesInput
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        /// TODO Edit XML Comment Template for Id
        public long Id { get; set; }
        /// <summary>
        /// Gets or sets the type of the field.
        /// </summary>
        /// <value>The type of the field.</value>
        /// TODO Edit XML Comment Template for FieldType
        public string FieldType { get; set; }
        /// <summary>
        /// Gets or sets the field key.
        /// </summary>
        /// <value>The field key.</value>
        /// TODO Edit XML Comment Template for FieldKey
        public string FieldKey { get; set; }
        /// <summary>
        /// Gets or sets the group key.
        /// </summary>
        /// <value>The group key.</value>
        /// TODO Edit XML Comment Template for GroupKey
        public string GroupKey { get; set; }
        /// <summary>
        /// Gets or sets the parent key.
        /// </summary>
        /// <value>The parent key.</value>
        /// TODO Edit XML Comment Template for ParentKey
        public string ParentKey { get; set; }
        /// <summary>
        /// Gets or sets the Language.
        /// </summary>
        /// <value>The Language.</value>
        /// TODO Edit XML Comment Template for Language
        public string Language { get; set; }
        /// <summary>
        /// Gets or sets the group filter.
        /// </summary>
        /// <value>The group filter.</value>
        /// TODO Edit XML Comment Template for GroupFilter
        public string GroupFilter { get; set; }
    }
}
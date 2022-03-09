// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-20-2021
//
// Last Modified By : jlwalker
// Last Modified On : 06-15-2021
// ***********************************************************************
// <copyright file="LocalizedContentTextDataTablesInput.cs" company="Jason L. Walker">
//     Copyright ©2012-2021 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************
using Jlw.Utilities.Data.DataTables;

namespace Jlw.Data.LocalizedContent
{
    /// <summary>
    /// Class LocalizedContentTextDataTablesInput.
    /// Implements the <see cref="Jlw.Utilities.Data.DataTables.DataTablesInput" />
    /// </summary>
    /// <seealso cref="Jlw.Utilities.Data.DataTables.DataTablesInput" />
    /// TODO Edit XML Comment Template for LocalizedContentTextDataTablesInput
    public class LocalizedContentTextDataTablesInput : DataTablesInput
    {
        /// <summary>
        /// Gets or sets the group key.
        /// </summary>
        /// <value>The group key.</value>
        /// TODO Edit XML Comment Template for GroupKey
        public string GroupKey { get; set; }
        /// <summary>
        /// Gets or sets the field key.
        /// </summary>
        /// <value>The field key.</value>
        /// TODO Edit XML Comment Template for FieldKey
        public string FieldKey { get; set; }
        public string ParentKey { get; set; }
        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        /// TODO Edit XML Comment Template for Language
        public string Language { get; set; }

        public string GroupFilter { get; set; }
    }
}
// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-20-2021
//
// Last Modified By : jlwalker
// Last Modified On : 05-15-2023
// ***********************************************************************
// <copyright file="LocalizedContentTextDataTablesInput.cs" company="Jason L. Walker">
//     Copyright ©2012-2023 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************

using Jlw.Utilities.Data.DataTables;

namespace Jlw.ModularContent
{
    /// <summary>
    /// Class LocalizedContentTextDataTablesInput.
    /// Implements the <see cref="Jlw.Utilities.Data.DataTables.DataTablesInput" />
    /// </summary>
    /// <seealso cref="Jlw.Utilities.Data.DataTables.DataTablesInput" />
    /// TODO Edit XML Comment Template for LocalizedContentTextDataTablesInput
    public class ModularContentTextDataTablesInput : DataTablesInput
    {
        /// <summary>
        /// Gets or sets the group key.
        /// </summary>
        /// <value>The group key.</value>
        public string GroupKey { get; set; }

        /// <summary>
        /// Gets or sets the field key.
        /// </summary>
        /// <value>The field key.</value>
        public string FieldKey { get; set; }

        /// <summary>
        /// Gets or sets the ParentKey
        /// </summary>
        public string ParentKey { get; set; }
        
        
        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the Group filter used to ensure that the group being retrieved are not part of another group.
        /// </summary>
        public string GroupFilter { get; set; }
    }
}
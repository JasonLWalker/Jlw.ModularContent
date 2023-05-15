// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-20-2021
//
// Last Modified By : jlwalker
// Last Modified On : 05-15-2023
// ***********************************************************************
// <copyright file="LocalizedContentText.cs" company="Jason L. Walker">
//     Copyright ©2012-2023 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Jlw.Utilities.Data;

namespace Jlw.LocalizedContent 
{
    /// <summary>
    /// Class to encapsulate a row from the [LocalizedContentText] database table
    /// </summary>
    public class LocalizedContentText : ILocalizedContentText 
	{

        /// <inheritdoc />
        public string GroupKey { get; protected set; }

        /// <inheritdoc />
        public string FieldKey { get; protected set; }

        /// <inheritdoc />
        public string ParentKey { get; protected set; }

        /// <inheritdoc />
        public string Language { get; protected set; }

        /// <inheritdoc />
        public string Text { get; protected set; }

        /// <inheritdoc />
        public string AuditChangeType { get; protected set; }

        /// <inheritdoc />
        public string AuditChangeBy { get; protected set; }

        /// <inheritdoc />
        public DateTime AuditChangeDate { get; protected set; }

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for #ctor
        public LocalizedContentText() => Initialize(null);
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedContentText"/> class.
        /// </summary>
        /// <param name="o">The o.</param>
        /// TODO Edit XML Comment Template for #ctor
        public LocalizedContentText (object o) => Initialize(o);

        /// <summary>
        /// Initializes the specified o.
        /// </summary>
        /// <param name="o">The o.</param>
        /// TODO Edit XML Comment Template for Initialize
        public void Initialize(object o)
        {
            GroupKey = DataUtility.Parse<string>(o, "GroupKey");
			FieldKey = DataUtility.Parse<string>(o, "FieldKey");
            ParentKey = DataUtility.Parse<string>(o, "ParentKey");
			Language = DataUtility.Parse<string>(o, "Language");
			Text = DataUtility.Parse<string>(o, "Text");
			AuditChangeType = DataUtility.Parse<string>(o, "AuditChangeType");
			AuditChangeBy = DataUtility.Parse<string>(o, "AuditChangeBy");
			AuditChangeDate = DataUtility.Parse<DateTime>(o, "AuditChangeDate");
        }
	} 
} 
 

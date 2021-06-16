// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-20-2021
//
// Last Modified By : jlwalker
// Last Modified On : 06-15-2021
// ***********************************************************************
// <copyright file="LocalizedGroupDataItem.cs" company="Jason L. Walker">
//     Copyright ©2012-2021 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Jlw.Utilities.Data;

namespace Jlw.Data.LocalizedContent 
{
    /// <summary>
    /// Class to encapsulate a row from the [LocalizedGroupDataItems] database table
    /// </summary>
    public class LocalizedGroupDataItem : ILocalizedGroupDataItem 
	{
        /// <inheritdoc />
		public long Id { get; protected set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        /// Member for [Language] Database Column
        public string Language { get; protected set; }

        /// <summary>
        /// Gets or sets the group key.
        /// </summary>
        /// <value>The group key.</value>
        /// Member for [GroupKey] Database Column
        public string GroupKey { get; protected set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        /// Member for [Key] Database Column
        public string Key { get; protected set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        /// Member for [Value] Database Column
        public string Value { get; protected set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        /// Member for [Order] Database Column
        public int Order { get; protected set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        /// Member for [Description] Database Column
        public string Description { get; protected set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        /// Member for [Data] Database Column
        public string Data { get; protected set; }

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for AuditChangeType
        public string AuditChangeType { get; protected set; }
        /// <inheritdoc />
        /// TODO Edit XML Comment Template for AuditChangeBy
        public string AuditChangeBy { get; protected set; }
        /// <inheritdoc />
        /// TODO Edit XML Comment Template for AuditChangeDate
        public DateTime AuditChangeDate { get; protected set; }

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for #ctor
        public LocalizedGroupDataItem() => Initialize(null);

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedGroupDataItem"/> class.
        /// </summary>
        /// <param name="o">The o.</param>
        /// TODO Edit XML Comment Template for #ctor
        public LocalizedGroupDataItem (object o) => Initialize(o);

        /// <summary>
        /// Initializes the specified o.
        /// </summary>
        /// <param name="o">The o.</param>
        /// TODO Edit XML Comment Template for Initialize
        public void Initialize(object o)
        {
            Id = DataUtility.Parse<long>(o, "Id");
			Language = DataUtility.Parse<string>(o, "Language");
			GroupKey = DataUtility.Parse<string>(o, "GroupKey");
			Key = DataUtility.Parse<string>(o, "Key");
			Value = DataUtility.Parse<string>(o, "Value");
			Order = DataUtility.Parse<int>(o, "Order");
			Description = DataUtility.Parse<string>(o, "Description");
			Data = DataUtility.Parse<string>(o, "Data");
			AuditChangeType = DataUtility.Parse<string>(o, "AuditChangeType");
			AuditChangeBy = DataUtility.Parse<string>(o, "AuditChangeBy");
			AuditChangeDate = DataUtility.ParseDateTime(o, "AuditChangeDate");
        }
	}
}


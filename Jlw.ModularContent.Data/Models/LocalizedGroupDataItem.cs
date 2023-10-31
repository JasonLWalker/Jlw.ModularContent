// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-20-2021
//
// Last Modified By : jlwalker
// Last Modified On : 05-15-2023
// ***********************************************************************
// <copyright file="LocalizedGroupDataItem.cs" company="Jason L. Walker">
//     Copyright ©2012-2023 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Jlw.Utilities.Data;

namespace Jlw.ModularContent 
{
    /// <summary>
    /// Class to encapsulate a row from the [LocalizedGroupDataItems] database table
    /// </summary>
    public class LocalizedGroupDataItem : IModularGroupDataItem 
	{
        /// <inheritdoc />
		public long Id { get; protected set; }

        /// <inheritdoc />
        public string Language { get; protected set; }

        /// <inheritdoc />
        public string GroupKey { get; protected set; }

        /// <inheritdoc />
        public string Key { get; protected set; }

        /// <inheritdoc />
        public string Value { get; protected set; }

        /// <inheritdoc />
        public int Order { get; protected set; }

        /// <inheritdoc />
        public string Description { get; protected set; }

        /// <inheritdoc />
        public string Data { get; protected set; }

        /// <inheritdoc />
        public string AuditChangeType { get; protected set; }
        /// <inheritdoc />
        public string AuditChangeBy { get; protected set; }
        /// <inheritdoc />
        public DateTime AuditChangeDate { get; protected set; }

        /// <summary>
        /// Default Constructor. Initializes a new instance of the <see cref="LocalizedGroupDataItem"/> class.
        /// </summary>
        public LocalizedGroupDataItem() => Initialize(null);

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedGroupDataItem"/> class.
        /// </summary>
        /// <param name="o">The o.</param>
        public LocalizedGroupDataItem (object o) => Initialize(o);

        /// <summary>
        /// Initializes the class from the values in the reference object.
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


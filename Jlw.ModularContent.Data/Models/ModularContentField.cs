// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-20-2021
//
// Last Modified By : jlwalker
// Last Modified On : 05-15-2023
// ***********************************************************************
// <copyright file="LocalizedContentField.cs" company="Jason L. Walker">
//     Copyright �2012-2023 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Text.RegularExpressions;
using Jlw.Utilities.Data;
using Newtonsoft.Json.Linq;

namespace Jlw.ModularContent 
{
    /// <summary>
    /// Class to encapsulate a row from the [LocalizedContentFields] database table.
    /// This class is used as a structure to represent a single container, field, or attribute in a Form, Wizard, or Email.
    /// </summary>
    public class ModularContentField : IModularContentField
	{
        /// <inheritdoc />
        /// TODO Edit XML Comment Template for Id
        public long Id { get; protected set; }
        /// <inheritdoc />
        /// TODO Edit XML Comment Template for GroupKey
        public string GroupKey { get; protected set; }
        /// <inheritdoc />
        /// TODO Edit XML Comment Template for FieldKey
        public string FieldKey { get; protected set; }

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for FieldType
        public string FieldType { get; protected set; }
        /// <inheritdoc />
        /// TODO Edit XML Comment Template for FieldData
        public string FieldData { get; protected set; }
        /// <inheritdoc />
        /// TODO Edit XML Comment Template for FieldClass
        public string FieldClass { get; protected set; }
        /// <inheritdoc />
        /// TODO Edit XML Comment Template for ParentKey
        public string ParentKey { get; protected set; }
        /// <inheritdoc />
        /// TODO Edit XML Comment Template for DefaultLabel
        public string DefaultLabel { get; protected set; }

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for WrapperClass
        public string WrapperClass { get; protected set; }

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for WrapperHtmlStart
        public string WrapperHtmlStart { get; set; }

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for WrapperHtmlEnd
        public string WrapperHtmlEnd { get; set; }

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
        /// TODO Edit XML Comment Template for GroupFilter
        public string GroupFilter { get; set; }

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for Order
        public int Order { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModularContentField" /> class. Members are set to their default values.
        /// </summary>
        /// <remarks>The default constructor will initialize all properties to their default values. All properties of type <c>string</c> will be initialized as empty strings instead of null.</remarks>
        public ModularContentField() => Initialize(null);

        /// <summary>
        /// Initializes a new instance of the <see cref="ModularContentField" /> class.
        /// </summary>
        /// <param name="o">Object used to initialize the class members.</param>
        /// <remarks><para>The constructor will attempt to initialize properties with values parsed from matching fields in the parameter <paramref name="o">o</paramref>. </para>
        /// <para>If <paramref name="o">o</paramref> is some form of <c>KeyValuePair</c> collection, then the constructor will attempt to find keys within the collection that match the names of the class properties. </para>
        /// <para>If <paramref name="o">o</paramref> is a non-collection type object, the constructor will attempt to find a field or property whose name matches the class member, and parse the data using the member from <paramref name="o">o</paramref>.</para></remarks>
        public ModularContentField(object o) => Initialize(o);


        /// <summary>
        /// Initializes and sets the properties of the <see cref="ModularContentField" /> class.
        /// </summary>
        /// <param name="o">Object used to initialize the class members.</param>
        /// <remarks><para>The method will attempt to initialize properties with values parsed from matching fields in the parameter <paramref name="o" />. </para>
        /// <para>If <paramref name="o" /> is some form of <c>KeyValuePair</c> collection, then the method will attempt to find keys within the collection that match the names of the class properties. </para>
        /// <para>If <paramref name="o" /> is a non-collection type object, the method will attempt to find a field or property whose name matches the class member, and parse the data using the member from <paramref name="o" />.</para>
        /// <note>
        ///   <para>As a rule, no exceptions will be thrown if the method is unable find matching data to parse, or if the data is not in a compatible format. If this is not the desired behavior consider overriding this method in a descendant class.</para>
        /// </note></remarks>
        public virtual void Initialize(object o)
        {
            Id = DataUtility.Parse<long>(o, "Id");
			GroupKey = DataUtility.Parse<string>(o, "GroupKey");
			FieldKey = DataUtility.Parse<string>(o, "FieldKey");
			FieldType = DataUtility.Parse<string>(o, "FieldType");
			FieldData = DataUtility.Parse<string>(o, "FieldData");
			FieldClass = DataUtility.Parse<string>(o, "FieldClass");
			ParentKey = DataUtility.Parse<string>(o, "ParentKey");
			DefaultLabel = DataUtility.Parse<string>(o, "DefaultLabel");
			WrapperClass = DataUtility.Parse<string>(o, "WrapperClass");
			WrapperHtmlStart = DataUtility.Parse<string>(o, "WrapperHtmlStart");
			WrapperHtmlEnd = DataUtility.Parse<string>(o, "WrapperHtmlEnd");
			AuditChangeType = DataUtility.Parse<string>(o, "AuditChangeType");
			AuditChangeBy = DataUtility.Parse<string>(o, "AuditChangeBy");
			AuditChangeDate = DataUtility.Parse<DateTime>(o, "AuditChangeDate");
			Order = DataUtility.Parse<int>(o, "Order");
			GroupFilter = DataUtility.Parse<string>(o, "GroupFilter");
		}

        /// <inheritdoc/>
        public virtual string ResolvePlaceholders(string sourceString, object replacementObject)
        {
            if (replacementObject is null) return sourceString;

            JToken data = JToken.FromObject(replacementObject ?? new object());
            string s = sourceString;
            var re = new Regex(@"\[%([\w\d\-_\s]*)%\]", RegexOptions.CultureInvariant);

            return re.Replace(s, (match) =>
            {
                string key;
                if (match?.Groups.Count > 1 && data?.SelectToken(key = match?.Groups[1].Value.Trim()) != null)
                {
                    return data.SelectToken(key)?.ToString();
                }

                return match?.Value;
            });
        }

	}
} 
 

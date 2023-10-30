// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-20-2021
//
// Last Modified By : jlwalker
// Last Modified On : 05-15-2023
// ***********************************************************************
// <copyright file="ILocalizedContentText.cs" company="Jason L. Walker">
//     Copyright ©2012-2023 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Jlw.Utilities.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Jlw.ModularContent 
{
    /// <summary>
    /// Class to encapsulate a row from the LocalizedContentText database table
    /// </summary>
    public interface ILocalizedContentText 
    {

        /// <summary>
        /// Gets the group key.
        /// </summary>
        /// <value>The group key.</value>
        /// Member for GroupKey Database Column

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))] 
        [JsonConverter(typeof(JlwJsonConverter<string>))] 
        string GroupKey { get; }

        /// <summary>
        /// Gets the field key.
        /// </summary>
        /// <value>The field key.</value>
        /// Member for FieldKey Database Column

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))] 
        [JsonConverter(typeof(JlwJsonConverter<string>))] 
        string FieldKey { get; }

        /// <summary>
        /// Gets the parent key.
        /// </summary>
        /// <value>The parent key.</value>
        /// Member for ParentKey Database Column

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<string>))]
        string ParentKey { get; }

        /// <summary>
        /// Gets the language.
        /// </summary>
        /// <value>The language.</value>
        /// Member for Language Database Column

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))] 
        [JsonConverter(typeof(JlwJsonConverter<string>))] 
        string Language { get; }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>The text.</value>
        /// Member for Text Database Column

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))] 
        [JsonConverter(typeof(JlwJsonConverter<string>))] 
        string Text { get; }

        /// <summary>
        /// The type of change that was last made to the record data.
        /// </summary>
        /// <value><para>
        /// Values for this field are usually one of the following:</para>
        /// <list type="bullet">
        ///   <item>IMPORT</item>
        ///   <item>INSERT</item>
        ///   <item>UPDATE</item>
        ///   <item>DELETE</item>
        /// </list></value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<string>))]
        string AuditChangeType { get; }

        /// <summary>
        /// The username of the user that last made changes to the record data.
        /// </summary>
        /// <value>The audit change by.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<string>))]
        string AuditChangeBy { get; }

        /// <summary>
        /// The date and time of the last change made to this data.
        /// </summary>
        /// <value>The audit change date.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<DateTime>))]
        DateTime AuditChangeDate { get; }


    }
} 

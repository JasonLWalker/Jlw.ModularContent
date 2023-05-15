using System;
using Jlw.Utilities.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Jlw.LocalizedContent 
{ 
    /// <summary> 
    /// Class to encapsulate a row from the LocalizedGroupDataItems database table 
    /// </summary> 
    public interface ILocalizedGroupDataItem 
    {

        /// <summary>Member for Id Database Column</summary>

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))] 
        [JsonConverter(typeof(JlwJsonConverter<long>))] 
        long Id { get; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language key for this record.</value>
        /// Member for [Language] Database Column
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))] 
        [JsonConverter(typeof(JlwJsonConverter<string>))] 
        string Language { get; }

        /// <summary>
        /// Gets or sets the group key.
        /// </summary>
        /// <value>The group key.</value>
        /// Member for [GroupKey] Database Column
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))] 
        [JsonConverter(typeof(JlwJsonConverter<string>))] 
        string GroupKey { get; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        /// Member for [Key] Database Column
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))] 
        [JsonConverter(typeof(JlwJsonConverter<string>))] 
        string Key { get; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        /// Member for [Value] Database Column
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))] 
        [JsonConverter(typeof(JlwJsonConverter<string>))] 
        string Value { get; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        /// Member for [Order] Database Column
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))] 
        [JsonConverter(typeof(JlwJsonConverter<int>))] 
        int Order { get; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        /// Member for [Description] Database Column
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))] 
        [JsonConverter(typeof(JlwJsonConverter<string>))] 
        string Description { get; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        /// Member for [Data] Database Column
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))] 
        [JsonConverter(typeof(JlwJsonConverter<string>))] 
        string Data { get; }

        /// <summary>The type of the change that was last made to the record data.</summary>
        /// <value>
        ///   <para>
        /// Values for this field are usually one of the following:</para>
        ///   <list type="bullet">
        ///     <item>IMPORT</item>
        ///     <item>INSERT</item>
        ///     <item>UPDATE</item>
        ///     <item>DELETE</item>
        ///   </list>
        /// </value>
        /// <autogeneratedoc />
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<string>))]
        string AuditChangeType { get; }

        /// <summary>The username of the user that last made changes to the record data.</summary>
        /// <autogeneratedoc />
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<string>))]
        string AuditChangeBy { get; }

        /// <summary>The date and time of the last change made to this data.</summary>
        /// <autogeneratedoc />
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<DateTime>))]
        DateTime AuditChangeDate { get; }


    }
} 

// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-27-2021
//
// Last Modified By : jlwalker
// Last Modified On : 05-15-2023
// ***********************************************************************
// <copyright file="WizardButtonData.cs" company="Jason L. Walker">
//     Copyright ©2012-2023 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************

using Jlw.Utilities.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Jlw.ModularContent
{
    /// <summary>
    /// Class WizardButtonData.
    /// </summary>
    /// TODO Edit XML Comment Template for WizardButtonData
    public class ModularWizardButtonData : ModularWizardContentField
    {
        /*
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>The label.</value>
        /// TODO Edit XML Comment Template for Label
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<string>))]
        public string Label { get; set; }
        */

        /// <summary>
        /// Gets or sets the class.
        /// </summary>
        /// <value>The class.</value>
        /// TODO Edit XML Comment Template for Class
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<string>))]
        public string Class { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        /// TODO Edit XML Comment Template for Icon
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<string>))]
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>The action.</value>
        /// TODO Edit XML Comment Template for Action
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        public object Action { get; set; }

        /// <summary>
        /// Gets or sets the wrapper.
        /// </summary>
        /// <value>The wrapper.</value>
        /// TODO Edit XML Comment Template for Wrapper
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<string>))]
        public string Wrapper { get; set; }

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for #ctor
        public ModularWizardButtonData() : this(null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModularWizardButtonData"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// TODO Edit XML Comment Template for #ctor
        public ModularWizardButtonData(IModularWizardContentField data) : base(data)
        {
            Label = data?.Label ?? "";
            Class = data?.FieldClass ?? "";
            Wrapper = data?.WrapperClass ?? "";

            string strInput = data?.FieldData?.Trim() ?? "{}";
            if (string.IsNullOrWhiteSpace(strInput)) return;

            if (strInput.StartsWith("{") && strInput.EndsWith("}"))
            {
                try
                {
                    var jObj = JToken.Parse(strInput);
                    Icon = jObj["icon"]?.ToString() ?? "";
                    Action = jObj["action"] ?? "";
                }
                catch
                {
                    // Do Nothing
                }

            }




        }
    }
}
// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-27-2021
//
// Last Modified By : jlwalker
// Last Modified On : 05-15-2023
// ***********************************************************************
// <copyright file="IWizardContent.cs" company="Jason L. Walker">
//     Copyright ©2012-2023 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using Jlw.Utilities.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Jlw.ModularContent
{
    /// <summary>
    /// Interface IWizardContent
    /// </summary>
    /// TODO Edit XML Comment Template for IWizardContent
    public interface IWizardContent
    {
        /// <summary>
        /// Gets or sets the form data.
        /// </summary>
        /// <value>The form data.</value>
        /// TODO Edit XML Comment Template for FormData
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        object FormData { get; set; }

        /// <summary>
        /// Gets or sets the field data.
        /// </summary>
        /// <value>The field data.</value>
        /// TODO Edit XML Comment Template for FieldData
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        object FieldData { get; set; }

        /// <summary>
        /// Gets or sets the group key.
        /// </summary>
        /// <value>The group key.</value>
        /// TODO Edit XML Comment Template for GroupKey
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<string>))]
        string GroupKey { get; set; }

        /// <summary>
        /// Gets or sets the heading.
        /// </summary>
        /// <value>The heading.</value>
        /// TODO Edit XML Comment Template for Heading
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<string>))]
        string Heading { get; set; }

        /// TODO Edit XML Comment Template for Heading
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        IWizardContentField HeadingData { get; set; }


        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>The body.</value>
        /// TODO Edit XML Comment Template for Body
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<string>))]
        string Body { get; set; }

        /// TODO Edit XML Comment Template for Heading
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        IWizardContentField BodyData { get; set; }


        /// <summary>
        /// Gets or sets the buttons.
        /// </summary>
        /// <value>The buttons.</value>
        /// TODO Edit XML Comment Template for Buttons
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        IEnumerable<WizardButtonData> Buttons { get; set; }

        /// <summary>
        /// Gets or sets the forms.
        /// </summary>
        /// <value>The forms.</value>
        /// TODO Edit XML Comment Template for Forms
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        IEnumerable<WizardFormData> Forms { get; set; }


    }
}
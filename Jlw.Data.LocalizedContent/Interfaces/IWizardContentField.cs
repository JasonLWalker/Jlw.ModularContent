// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-27-2021
//
// Last Modified By : jlwalker
// Last Modified On : 05-15-2023
// ***********************************************************************
// <copyright file="IWizardContentField.cs" company="Jason L. Walker">
//     Copyright ©2012-2023 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************

using Jlw.Utilities.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Jlw.Data.LocalizedContent
{
    /// <summary>
    /// Interface IWizardContentField
    /// Implements the <see cref="ILocalizedContentField" />
    /// </summary>
    /// <seealso cref="ILocalizedContentField" />
    /// TODO Edit XML Comment Template for IWizardContentField
    public interface IWizardContentField : ILocalizedContentField
    {
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>The label.</value>
        /// TODO Edit XML Comment Template for Label
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<string>))]
        string Label { get; set; }
    }
}
// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-27-2021
//
// Last Modified By : jlwalker
// Last Modified On : 06-15-2021
// ***********************************************************************
// <copyright file="IWizardContentField.cs" company="Jason L. Walker">
//     Copyright ©2012-2021 Jason L. Walker
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
    /// Implements the <see cref="Jlw.Data.LocalizedContent.ILocalizedContentField" />
    /// </summary>
    /// <seealso cref="Jlw.Data.LocalizedContent.ILocalizedContentField" />
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
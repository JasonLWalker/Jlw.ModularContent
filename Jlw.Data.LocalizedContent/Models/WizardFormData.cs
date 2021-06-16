// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-27-2021
//
// Last Modified By : jlwalker
// Last Modified On : 06-15-2021
// ***********************************************************************
// <copyright file="WizardFormData.cs" company="Jason L. Walker">
//     Copyright ©2012-2021 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using Jlw.Utilities.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Jlw.Data.LocalizedContent
{
    /// <summary>
    /// Class WizardFormData.
    /// Implements the <see cref="Jlw.Data.LocalizedContent.WizardContentField" />
    /// </summary>
    /// <seealso cref="Jlw.Data.LocalizedContent.WizardContentField" />
    /// TODO Edit XML Comment Template for WizardFormData
    public class WizardFormData : WizardContentField
    {
        /// <summary>
        /// Gets or sets a value indicating whether [use card layout].
        /// </summary>
        /// <value><c>true</c> if [use card layout]; otherwise, <c>false</c>.</value>
        /// TODO Edit XML Comment Template for UseCardLayout
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<bool>))]
        public bool UseCardLayout { get; set; }
        /// <summary>
        /// Gets or sets the edit button.
        /// </summary>
        /// <value>The edit button.</value>
        /// TODO Edit XML Comment Template for EditButton
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        public WizardButtonData EditButton { get; set; }
        /// <summary>
        /// Gets or sets the fields.
        /// </summary>
        /// <value>The fields.</value>
        /// TODO Edit XML Comment Template for Fields
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        public IEnumerable<JToken> Fields { get; set; } = new List<JToken>();

        /// <summary>
        /// Initializes a new instance of the <see cref="WizardFormData"/> class.
        /// </summary>
        /// <param name="formKey">The form key.</param>
        /// <param name="fieldData">The field data.</param>
        /// <param name="editButton">The edit button.</param>
        /// TODO Edit XML Comment Template for #ctor
        public WizardFormData(string formKey, IEnumerable<WizardContentField> fieldData, WizardButtonData editButton = null)
        {
            var data = fieldData?.ToList();
            var form = data?.FirstOrDefault(o => o.FieldType.Equals("Form", StringComparison.InvariantCultureIgnoreCase) && o.FieldKey.Equals(formKey, StringComparison.InvariantCultureIgnoreCase));
            if (form == null)
            {
                return;
            }

            Initialize(form);
            EditButton = editButton;

        }
    }
}
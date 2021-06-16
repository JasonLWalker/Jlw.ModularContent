// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-27-2021
//
// Last Modified By : jlwalker
// Last Modified On : 06-15-2021
// ***********************************************************************
// <copyright file="WizardContent.cs" company="Jason L. Walker">
//     Copyright ©2012-2021 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using Jlw.Utilities.Data;
using Newtonsoft.Json.Linq;

namespace Jlw.Data.LocalizedContent
{
    /// <summary>
    /// Class WizardContent.
    /// Implements the <see cref="Jlw.Data.LocalizedContent.IWizardContent" />
    /// </summary>
    /// <seealso cref="Jlw.Data.LocalizedContent.IWizardContent" />
    /// TODO Edit XML Comment Template for WizardContent
    public class WizardContent : IWizardContent
    {
        /// <inheritdoc />
        /// TODO Edit XML Comment Template for FormData
        public object FormData { get; set; }
        /// <inheritdoc />
        /// TODO Edit XML Comment Template for GroupKey
        public string GroupKey { get; set; }

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for Heading
        public string Heading { get; set; }
        /// <inheritdoc />
        /// TODO Edit XML Comment Template for Body
        public string Body { get; set; }

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for FieldData
        public object FieldData { get; set; }

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for Buttons
        public IEnumerable<WizardButtonData> Buttons { get; set; } = new List<WizardButtonData>();

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for Forms
        public IEnumerable<WizardFormData> Forms { get; set; } = new List<WizardFormData>();


        /// <summary>
        /// Initializes a new instance of the <see cref="WizardContent"/> class.
        /// </summary>
        /// <param name="fieldData">The field data.</param>
        /// <param name="formData">The form data.</param>
        /// TODO Edit XML Comment Template for #ctor
        public WizardContent(IEnumerable<IWizardContentField> fieldData, object formData = null)
        {
            var data = fieldData?.ToList();
            var wizard = data?.FirstOrDefault(o => o.FieldType.Equals("Wizard", StringComparison.InvariantCultureIgnoreCase));
            FormData = formData ?? new object();
            FieldData = null;
            GroupKey = wizard?.GroupKey ?? data?.FirstOrDefault()?.GroupKey ?? "";
            if (wizard == null)
            {
                Heading = "Oops!";
                Body = "I'm sorry, but I was unable to retrieve the wizard content at this time. Please try again later.";
                return;
            }
            var fields = data.Where(o => o.ParentKey.Equals(wizard.FieldKey, StringComparison.CurrentCultureIgnoreCase)).OrderBy(o => o.Order).ToList();
            Heading = fields.FirstOrDefault(o => o.FieldKey.Equals("Heading", StringComparison.InvariantCultureIgnoreCase))?.Label ?? "";
            Body = fields.FirstOrDefault(o => o.FieldKey.Equals("Body", StringComparison.InvariantCultureIgnoreCase))?.Label ?? "";

            if (!string.IsNullOrWhiteSpace(wizard.FieldData))
            {
                try
                {
                    FieldData = JObject.Parse(wizard.FieldData);
                }
                catch
                {
                    // Do Nothing
                }
            }

            var buttons = fields.Where(o => o.FieldType.Equals("Button", StringComparison.InvariantCultureIgnoreCase)).OrderBy(o => o.Order).ToList();
            foreach (var btnField in buttons)
            {
                ((List<WizardButtonData>)Buttons).Add(new WizardButtonData(btnField));
            }

        }

    }
}
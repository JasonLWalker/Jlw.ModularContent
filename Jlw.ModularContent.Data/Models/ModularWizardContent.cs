// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-27-2021
//
// Last Modified By : jlwalker
// Last Modified On : 05-15-2023
// ***********************************************************************
// <copyright file="WizardContent.cs" company="Jason L. Walker">
//     Copyright ©2012-2023 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Jlw.ModularContent
{
    /// <summary>
    /// Class WizardContent.
    /// Implements the <see cref="IModularWizardContent" />
    /// </summary>
    /// <seealso cref="IModularWizardContent" />
    /// TODO Edit XML Comment Template for WizardContent
    public class ModularWizardContent : ModularWizardContentField, IModularWizardContent
    {
        /// <inheritdoc />
        /// TODO Edit XML Comment Template for FormData
        public object FormData { get; set; }

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for GroupKey
        public new string GroupKey
        {
            get => base.GroupKey; 
            set => base.GroupKey =  value;
        }

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for Heading
        public string Heading { get; set; }

        /// <inheritdoc />
        public IModularWizardContentField HeadingData { get; set; }

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for Body
        public string Body { get; set; }

        /// <inheritdoc />
        public IModularWizardContentField BodyData { get; set; }

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for FieldData
        public new object FieldData { get; set; }

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for Buttons
        public IEnumerable<ModularWizardButtonData> Buttons { get; set; } = new List<ModularWizardButtonData>();

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for Forms
        public IEnumerable<ModularWizardFormData> Forms { get; set; } = new List<ModularWizardFormData>();


        /// <summary>
        /// Initializes a new instance of the <see cref="ModularWizardContent"/> class.
        /// </summary>
        /// <param name="fieldData">The field data.</param>
        /// <param name="formData">The form data.</param>
        /// TODO Edit XML Comment Template for #ctor
        public ModularWizardContent(IEnumerable<IModularWizardContentField> fieldData, object formData = null) :base(null)
        {
            var data = fieldData?.ToList();
            var wizard = data?.FirstOrDefault(o => o.FieldType.Equals("Wizard", StringComparison.InvariantCultureIgnoreCase));
            Initialize(wizard);
            FormData = formData ?? new object();
            FieldData = null;
            GroupKey = wizard?.GroupKey ?? data?.FirstOrDefault()?.GroupKey ?? "";
            if (wizard == null)
            {
                Heading = "Oops!";
                Body = "I'm sorry, but I was unable to retrieve the wizard content at this time. Please try again later.";
                return;
            }

            Label = wizard.Label;
            WrapperHtmlStart = wizard.WrapperHtmlStart;
            WrapperHtmlEnd = wizard.WrapperHtmlEnd;

            var fields = data.Where(o => o.ParentKey.Equals(wizard.FieldKey, StringComparison.CurrentCultureIgnoreCase)).OrderBy(o => o.Order).ToList();

            HeadingData = fields.FirstOrDefault(o => o.FieldKey.Equals("Heading", StringComparison.InvariantCultureIgnoreCase));
            Heading = HeadingData?.Label ?? "";
            BodyData = fields.FirstOrDefault(o => o.FieldKey.Equals("Body", StringComparison.InvariantCultureIgnoreCase));
            Body = BodyData?.Label ?? "";

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
                ((List<ModularWizardButtonData>)Buttons).Add(new ModularWizardButtonData(btnField));

            }

        }

    }
}
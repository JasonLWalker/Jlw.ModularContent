// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-27-2021
//
// Last Modified By : jlwalker
// Last Modified On : 06-15-2021
// ***********************************************************************
// <copyright file="WizardContentField.cs" company="Jason L. Walker">
//     Copyright ©2012-2021 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************
using Jlw.Utilities.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Jlw.Data.LocalizedContent
{
    /// <summary>
    /// Class WizardContentField.
    /// Implements the <see cref="Jlw.Data.LocalizedContent.LocalizedContentField" />
    /// Implements the <see cref="Jlw.Data.LocalizedContent.IWizardContentField" />
    /// </summary>
    /// <seealso cref="Jlw.Data.LocalizedContent.LocalizedContentField" />
    /// <seealso cref="Jlw.Data.LocalizedContent.IWizardContentField" />
    /// TODO Edit XML Comment Template for WizardContentField
    public class WizardContentField : LocalizedContentField, IWizardContentField
    {
        /// <inheritdoc />
        /// TODO Edit XML Comment Template for Label
        public string Label { get; set; }

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for #ctor
        public WizardContentField() : this(null)
        {

        }

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for #ctor
        public WizardContentField(object o) : base(o)
        {
            this.Initialize(o);
        }

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for Initialize
        public override void Initialize(object o)
        {
            base.Initialize(o);
            Label = DataUtility.ParseString(o, nameof(Label));
        }

        /// <summary>
        /// Sets the disabled flag.
        /// </summary>
        /// <param name="isDisabled">if set to <c>true</c> [is disabled].</param>
        /// TODO Edit XML Comment Template for SetDisabledFlag
        public void SetDisabledFlag(bool isDisabled = true)
        {
            JToken data;
            try
            {
                string jScript = string.IsNullOrWhiteSpace(FieldData) ? "{}" : FieldData;
                data = JToken.Parse(jScript);
                data["props"] += " disabled=\"disabled\"";
                FieldData = JsonConvert.SerializeObject(data);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Gets the field data.
        /// </summary>
        /// <returns>JToken.</returns>
        /// TODO Edit XML Comment Template for GetFieldData
        public JToken GetFieldData()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(FieldData))
                    return JToken.Parse(FieldData);
            }
            catch
            {
                // ignored
            }
            return new JObject();
        }

    }
}
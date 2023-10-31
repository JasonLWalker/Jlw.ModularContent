using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Jlw.ModularContent
{
    /// <summary>
    /// Class to encapsulate the content of each screen in the Wizard
    /// </summary>
    public class WizardScreenContent : WizardContent
    {
        /// <summary>
        /// Constructor to initialize the screen data
        /// </summary>
        /// <param name="screenId"></param>
        /// <param name="fieldData"></param>
        /// <param name="formData"></param>
        public WizardScreenContent(string screenId, IEnumerable<IModularWizardContentField> fieldData, object formData = null) : base(fieldData, formData)
        {

            var data = fieldData?.ToList();
            var wizard = data?.FirstOrDefault(o => o.FieldType.Equals("Screen", StringComparison.InvariantCultureIgnoreCase) && o.FieldKey.Equals(screenId, StringComparison.InvariantCultureIgnoreCase));
            Initialize(wizard);

            ((List<WizardButtonData>)Buttons).Clear();
            ((List<WizardFormData>)Forms).Clear();

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

            /*
            // Resolve Placeholders for field labels
            fields.ForEach(o=>
            {
                o.Label = o.ResolvePlaceholders(o.Label, formData);
                o.WrapperHtmlStart = o.ResolvePlaceholders(o.WrapperHtmlStart, formData);
                o.WrapperHtmlEnd = o.ResolvePlaceholders(o.WrapperHtmlEnd, formData);
            });
            */
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
                ((List<WizardButtonData>)Buttons).Add(new WizardButtonData(btnField));
            }

        }
    }
}
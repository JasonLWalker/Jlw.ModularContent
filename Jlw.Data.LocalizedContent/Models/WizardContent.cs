using System;
using System.Collections.Generic;
using System.Linq;
using Jlw.Utilities.Data;
using Newtonsoft.Json.Linq;

namespace Jlw.Data.LocalizedContent
{
    public class WizardContent : IWizardContent
    {
        public object FormData { get; set; }
        public string GroupKey { get; set; }

        public string Heading { get; set; }
        public string Body { get; set; }
        public IEnumerable<WizardButtonData> Buttons { get; set; } = new List<WizardButtonData>();

        public IEnumerable<WizardFormData> Forms { get; set; } = new List<WizardFormData>();


        public WizardContent(IEnumerable<IWizardContentField> fieldData, object formData = null)
        {
            var data = fieldData?.ToList();
            var wizard = data?.FirstOrDefault(o => o.FieldType.Equals("Wizard", StringComparison.InvariantCultureIgnoreCase));
            FormData = formData ?? new object();
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


            var buttons = fields.Where(o => o.FieldType.Equals("Button", StringComparison.InvariantCultureIgnoreCase)).OrderBy(o => o.Order).ToList();
            foreach (var btnField in buttons)
            {
                ((List<WizardButtonData>)Buttons).Add(new WizardButtonData(btnField));
            }

        }

    }
}
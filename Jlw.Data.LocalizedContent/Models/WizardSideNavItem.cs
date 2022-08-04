using System;
using Jlw.Utilities.Data;
using Newtonsoft.Json.Linq;

namespace Jlw.Data.LocalizedContent
{
    public class WizardSideNavItem : IWizardSideNavItem
    {
        protected readonly IWizardContentField _field;
        protected readonly JToken _jsonData;

        public string Screen => (_field?.FieldType?.Equals("SCREEN", StringComparison.InvariantCultureIgnoreCase) ?? false)
            ? (_field?.FieldKey ?? "")
            : "";

        public string Wizard => (_field?.FieldType?.Equals("SCREEN", StringComparison.InvariantCultureIgnoreCase) ?? false)
            ? (_field?.ParentKey ?? "")
            : "";

        public int Section => DataUtility.ParseInt(_jsonData?["section"]);
        public int Step => DataUtility.ParseInt(_jsonData?["step"]);

        public string Label => (_field?.FieldType?.Equals("SCREEN", StringComparison.InvariantCultureIgnoreCase) ?? false) ? (_field?.Label ?? "") : "";

        public string FieldKey => _field.FieldKey;

        public string FieldType => _field.FieldType;
        public string ParentKey => _field.ParentKey;
        public string FieldData => _field.FieldData;
        public string JsonData { get; set; }
        public string ClassName { get; set; }
        public bool IsHidden { get; set; }
        public string Parent { get; set; }

        public WizardSideNavItem(object o)
        {
            _field = new WizardContentField(o);
            try
            {
                _jsonData = JToken.Parse(_field?.FieldData ?? "{}");
            }
            catch
            {
                // do nothing
            }

            JsonData = DataUtility.ParseString(o, "JsonData");
            ClassName = DataUtility.ParseString(o, "Class");
            IsHidden = DataUtility.ParseBool(o, "Hidden");

        }


    }
}
using System;
using Jlw.Utilities.Data;
using Newtonsoft.Json.Linq;

namespace Jlw.LocalizedContent
{
    /// <inheritdoc />
    public class WizardSideNavItem : IWizardSideNavItem
    {
        /// <summary> Internal field record used by some properties </summary>
        protected readonly IWizardContentField _field;

        /// <summary> Internal JToken object used to parse JSON data for some properties </summary>
        protected readonly JToken _jsonData;

        /// <inheritdoc />
        public string Screen => (_field?.FieldType?.Equals("SCREEN", StringComparison.InvariantCultureIgnoreCase) ?? false)
            ? (_field?.FieldKey ?? "")
            : "";

        /// <inheritdoc />
        public string Wizard => (_field?.FieldType?.Equals("SCREEN", StringComparison.InvariantCultureIgnoreCase) ?? false)
            ? (_field?.ParentKey ?? "")
            : (_field?.GroupKey ?? "");

        /// <inheritdoc />
        public int Section => DataUtility.ParseInt(_jsonData?["section"]);

        /// <inheritdoc />
        public int Step => DataUtility.ParseInt(_jsonData?["step"]);

        /// <inheritdoc />
        public string Label => (_field?.FieldType?.Equals("SCREEN", StringComparison.InvariantCultureIgnoreCase) ?? false) ? (_field?.Label ?? "") : "";

        /// <inheritdoc />
        public string FieldKey => _field.FieldKey;

        /// <inheritdoc />
        public string FieldType => _field.FieldType;
        /// <inheritdoc />
        public string ParentKey => _field.ParentKey;
        /// <inheritdoc />
        public string FieldData => _field.FieldData;
        /// <inheritdoc />
        public string JsonData { get; set; }
        /// <inheritdoc />
        public string ClassName { get; set; }
        /// <inheritdoc />
        public bool IsHidden { get; set; }
        /// <inheritdoc />
        public string Parent { get; set; }

        /// <summary>
        /// Constructor to initialize item from the reference object
        /// </summary>
        /// <param name="o">Reference object</param>
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
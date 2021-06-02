using Jlw.Utilities.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Jlw.Data.LocalizedContent
{
    public class WizardContentField : LocalizedContentField, IWizardContentField
    {
        public string Label { get; set; }

        public WizardContentField() : this(null)
        {

        }

        public WizardContentField(object o) : base(o)
        {
            this.Initialize(o);
        }

        public override void Initialize(object o)
        {
            base.Initialize(o);
            Label = DataUtility.ParseString(o, nameof(Label));
        }

        public void SetDisabledFlag(bool isDisabled = true)
        {
            JToken data;
            try
            {
                data = JToken.Parse(FieldData);
                data["props"] += " disabled=\"disabled\"";
                FieldData = JsonConvert.SerializeObject(data);
            }
            catch
            {
                // ignored
            }
        }

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
using Newtonsoft.Json.Linq;

namespace Jlw.Data.LocalizedContent
{
    public class WizardButtonData
    {
        public string Label { get; set; }
        public string Class { get; set; }
        public string Icon { get; set; }
        public object Action { get; set; }

        public string Wrapper { get; set; }

        public WizardButtonData() : this(null) { }

        public WizardButtonData(IWizardContentField data)
        {
            Label = data?.Label ?? "";
            Class = data?.FieldClass ?? "";
            Wrapper = data?.WrapperClass ?? "";

            string strInput = data?.FieldData?.Trim() ?? "{}";
            if (string.IsNullOrWhiteSpace(strInput)) return;

            if (strInput.StartsWith("{") && strInput.EndsWith("}"))
            {
                try
                {
                    var jObj = JToken.Parse(strInput);
                    Icon = jObj["icon"]?.ToString() ?? "";
                    Action = jObj["action"] ?? "";
                }
                catch
                {
                    // Do Nothing
                }

            }




        }
    }
}
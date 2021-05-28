using Jlw.Utilities.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Jlw.Data.LocalizedContent
{
    public class WizardButtonData
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<string>))]
        public string Label { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<string>))]
        public string Class { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<string>))]
        public string Icon { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        public object Action { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<string>))]
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
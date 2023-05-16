using Jlw.Utilities.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Jlw.LocalizedContent
{
    /// ToDo: add XMLDoc comments
    public class WizardComponentField : WizardContentField
    {
        /// The number of times this component has been used in this wizard
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<int>))]
        public int UseCount { get; set; }

        /// ToDo: add XMLDoc comments
        public WizardComponentField() : this (null) {}

        /// ToDo: add XMLDoc comments
        public WizardComponentField(object o) : base(o)
        {
            if (o is null)
                return;

            UseCount = DataUtility.ParseInt(o, "UseCount");
        }
    }
}
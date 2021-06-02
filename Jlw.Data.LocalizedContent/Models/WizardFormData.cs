using System;
using System.Collections.Generic;
using System.Linq;
using Jlw.Utilities.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Jlw.Data.LocalizedContent
{
    public class WizardFormData : WizardContentField
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<bool>))]
        public bool UseCardLayout { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        public WizardButtonData EditButton { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        public IEnumerable<JToken> Fields { get; set; } = new List<JToken>();

        public WizardFormData(string formKey, IEnumerable<WizardContentField> fieldData, WizardButtonData editButton = null)
        {
            var data = fieldData?.ToList();
            var form = data?.FirstOrDefault(o => o.FieldType.Equals("Form", StringComparison.InvariantCultureIgnoreCase) && o.FieldKey.Equals(formKey, StringComparison.InvariantCultureIgnoreCase));
            if (form == null)
            {
                return;
            }

            Initialize(form);
            EditButton = editButton;

        }
    }
}
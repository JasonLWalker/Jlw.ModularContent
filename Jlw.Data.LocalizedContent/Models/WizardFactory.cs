using System;
using System.Collections.Generic;
using System.Linq;
using Jlw.Utilities.Data;
using Newtonsoft.Json.Linq;

namespace Jlw.Data.LocalizedContent
{
    public class WizardFactory : IWizardFactory
    {
        public object FormData { get; set; }


        protected IWizardFactoryRepository DataRepository;

        public WizardFactory(IWizardFactoryRepository repository)
        {
            DataRepository = repository;
        }

        public IWizardContent CreateWizardContent(string groupKey, object formData = null)
        {
            var fieldData = DataRepository.GetFieldData(groupKey)?.ToList() ?? new List<WizardContentField>();
            var wizard = fieldData?.FirstOrDefault(o => o.FieldType.Equals("Wizard", StringComparison.InvariantCultureIgnoreCase));
            var model = formData ?? new object();
            var content = new WizardContent(fieldData, formData);

            var fields = fieldData.Where(o => o.ParentKey.Equals(wizard.FieldKey, StringComparison.CurrentCultureIgnoreCase)).OrderBy(o => o.Order).ToList();
            var formList = fields.Where(o => o.FieldType.Equals("Form", StringComparison.InvariantCultureIgnoreCase)).OrderBy(o => o.Order).ToList();
            string embedData = fields.FirstOrDefault(o => o.FieldType.Equals("Embed", StringComparison.InvariantCultureIgnoreCase))?.FieldData ?? "{}";
            foreach (var form in formList)
            {
                ((List<WizardFormData>)content.Forms).Add(CreateWizardFormData(form.FieldKey, fieldData));
            }

            if (!string.IsNullOrWhiteSpace(embedData))
            {
                try
                {
                    JToken dta = JToken.Parse(embedData);
                    JToken embedWiz = dta["embedWizard"];
                    if (embedWiz is JArray)
                    {
                        foreach (var key in dta["embedWizard"])
                        {
                            AddEmbeddedForm(key.Value<string>(), content, null, DataUtility.ParseBool(dta["disabled"]?.ToString()), DataUtility.ParseBool(dta["editButton"]));

                        }
                    }
                }
                catch { }

            }

            return content;
        }

        public void AddEmbeddedForm(string key, IWizardContent content, IEnumerable<WizardContentField> extraFields = null, bool isDisabled = false, bool hasEditButton = false)
        {
            List<WizardContentField> embed = (List<WizardContentField>)DataRepository.GetFieldData(key);
            var wizard = embed?.FirstOrDefault(o => o.FieldType.Equals("Wizard", StringComparison.InvariantCultureIgnoreCase));
            if (extraFields != null)
            {
                foreach (var field in extraFields)
                {
                    embed?.Add(field);
                }
            }

            if (isDisabled)
            {
                foreach (var field in embed)
                {
                    field.SetDisabledFlag();
                }
            }
            var fields = embed?.Where(o => o.ParentKey.Equals(wizard?.FieldKey, StringComparison.CurrentCultureIgnoreCase)).OrderBy(o => o.Order).ToList();
            var formList = fields?.Where(o => o.FieldType.Equals("Form", StringComparison.InvariantCultureIgnoreCase)).OrderBy(o => o.Order).ToList();

            foreach (var form in formList)
            {
                WizardButtonData editButton = null;
                if (hasEditButton)
                {
                    JToken data = form.GetFieldData();
                    editButton = new WizardButtonData();
                    editButton.Class = "btn btn-sm btn-outline-primary";
                    editButton.Label = "Edit";
                    editButton.Icon = "fa fa-edit";
                    editButton.Action = new { type = "nav", section = DataUtility.ParseInt(data["section"]), step = DataUtility.ParseInt(data["step"]) };
                }

                ((List<WizardFormData>)content).Add(new WizardFormData(form.FieldKey, embed, editButton));
            }
        }

        public WizardFormData CreateWizardFormData(string formKey, IEnumerable<WizardContentField> fieldData, WizardButtonData editButton = null)
        {
            var data = fieldData?.ToList();
            var form = data?.FirstOrDefault(o => o.FieldType.Equals("Form", StringComparison.InvariantCultureIgnoreCase) && o.FieldKey.Equals(formKey, StringComparison.InvariantCultureIgnoreCase));
            if (form == null)
            {
                return new WizardFormData(null, null, null);
            }

            var returnObject = new WizardFormData(formKey, fieldData, editButton);

            string strInput;

            strInput = form.FieldData?.Trim();
            if (string.IsNullOrWhiteSpace(strInput)) return returnObject;
            var fields = data.Where(o => o.ParentKey.Equals(form.FieldKey, StringComparison.CurrentCultureIgnoreCase)).OrderBy(o => o.Order).ToList();
            foreach (var o in fields)
            {
                JToken field = JToken.FromObject(o);
                strInput = o.FieldData?.Trim();
                if (!string.IsNullOrWhiteSpace(strInput))
                {
                    if (strInput.StartsWith("{") && strInput.EndsWith("}"))
                    {
                        try
                        {
                            var dta = JToken.Parse(strInput);
                            if ((field["FieldType"]?.ToString() ?? "").Equals("Select", StringComparison.InvariantCultureIgnoreCase))
                            {
                                /*
                                // Example of overriding lookup data:
                                switch (dta["lookup"]?.ToString().ToLower())
                                {
                                    case "country":
                                        dta["data"] = JToken.FromObject(DataRepository.GetCountryList());
                                        break;
                                }
                                */
                            }

                            field["FieldData"] = dta;
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                }

                field["FieldData"] = field["FieldData"] ?? new JObject();

                ((List<JToken>)(returnObject.Fields)).Add(field);
            }
            if (strInput.StartsWith("{") && strInput.EndsWith("}"))
            {
                try
                {
                    var jObj = JToken.Parse(strInput);
                    returnObject.UseCardLayout = DataUtility.ParseBool(jObj["useCardLayout"]);
                }
                catch
                {
                    // ignored
                }
            }

            return returnObject;
        }
    }
}
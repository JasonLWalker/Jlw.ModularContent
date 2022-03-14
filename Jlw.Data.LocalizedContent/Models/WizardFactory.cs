// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-27-2021
//
// Last Modified By : jlwalker
// Last Modified On : 06-15-2021
// ***********************************************************************
// <copyright file="WizardFactory.cs" company="Jason L. Walker">
//     Copyright ©2012-2021 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using Jlw.Utilities.Data;
using Newtonsoft.Json.Linq;

namespace Jlw.Data.LocalizedContent
{
    /// <summary>
    /// Class WizardFactory.
    /// Implements the <see cref="Jlw.Data.LocalizedContent.IWizardFactory" />
    /// </summary>
    /// <seealso cref="Jlw.Data.LocalizedContent.IWizardFactory" />
    /// TODO Edit XML Comment Template for WizardFactory
    public class WizardFactory : IWizardFactory
    {
        /// <summary>
        /// Gets or sets the form data.
        /// </summary>
        /// <value>The form data.</value>
        /// TODO Edit XML Comment Template for FormData
        public object FormData { get; set; }


        /// <summary>
        /// The data repository
        /// </summary>
        /// TODO Edit XML Comment Template for DataRepository
        protected IWizardFactoryRepository DataRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="WizardFactory"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// TODO Edit XML Comment Template for #ctor
        public WizardFactory(IWizardFactoryRepository repository)
        {
            DataRepository = repository;
        }

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for CreateWizardContent
        public virtual IWizardContent CreateWizardContent(string groupKey, object formData = null)
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

            ((List<WizardFormData>) content.Forms).Sort((o1, o2) => o1.Order - o2.Order );
            return content;
        }

        /// <inheritdoc />
        /// TODO Edit XML Comment Template for CreateWizardContent
        public virtual IWizardContent CreateWizardScreenContent(string groupKey, string screenKey, object formData = null)
        {
            var fieldData = DataRepository.GetFieldData(groupKey)?.ToList() ?? new List<WizardContentField>();
            var wizard = fieldData?.FirstOrDefault(o => o.FieldType.Equals("Screen", StringComparison.InvariantCultureIgnoreCase) && o.FieldKey.Equals(screenKey, StringComparison.InvariantCultureIgnoreCase));
            var model = formData ?? new object();
            var content = new WizardScreenContent(screenKey, fieldData, formData);

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

            ((List<WizardFormData>)content.Forms).Sort((o1, o2) => o1.Order - o2.Order);
            foreach (var wizardFormData in content.Forms)
            {
                foreach (var formField in wizardFormData.Fields)
                {
                    formField["Label"] = wizardFormData.ResolvePlaceholders(formField["Label"].ToString(), formData);
                }
            }

            return content;
        }

        public virtual WizardContentEmail CreateWizardContentEmail(string groupKey, string parentKey, object formData = null)
        {
            var fieldData = DataRepository.GetFieldData(groupKey)?.ToList() ?? new List<WizardContentField>();
            return new WizardContentEmail(parentKey, fieldData, formData);
        }

        /// <summary>
        /// Adds the embedded form.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="content">The content.</param>
        /// <param name="extraFields">The extra fields.</param>
        /// <param name="isDisabled">if set to <c>true</c> [is disabled].</param>
        /// <param name="hasEditButton">if set to <c>true</c> [has edit button].</param>
        /// TODO Edit XML Comment Template for AddEmbeddedForm
        public virtual void AddEmbeddedForm(string key, IWizardContent content, IEnumerable<WizardContentField> extraFields = null, bool isDisabled = false, bool hasEditButton = false)
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

                ((List<WizardFormData>)(content.Forms)).Add(CreateWizardFormData(form.FieldKey, embed, editButton));
            }
        }

        /// <summary>
        /// Creates the wizard form data.
        /// </summary>
        /// <param name="formKey">The form key.</param>
        /// <param name="fieldData">The field data.</param>
        /// <param name="editButton">The edit button.</param>
        /// <returns>WizardFormData.</returns>
        /// TODO Edit XML Comment Template for CreateWizardFormData
        public virtual WizardFormData CreateWizardFormData(string formKey, IEnumerable<WizardContentField> fieldData, WizardButtonData editButton = null)
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


            strInput = form.FieldData?.Trim();
            if (string.IsNullOrWhiteSpace(strInput)) return returnObject;
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
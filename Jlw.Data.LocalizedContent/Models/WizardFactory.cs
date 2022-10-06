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
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Jlw.Utilities.Data;
using Newtonsoft.Json.Linq;

namespace Jlw.Data.LocalizedContent
{
    /// <summary>
    /// Class WizardFactory.
    /// Implements the <see cref="Jlw.Data.LocalizedContent.IWizardFactory" />
    /// </summary>
    /// <seealso cref="Jlw.Data.LocalizedContent.IWizardFactory" />
    public class WizardFactory : IWizardFactory
    {
        /// <summary>
        /// Gets or sets the form data.
        /// </summary>
        /// <value>The form data.</value>
        public object FormData { get; set; }


        /// <summary>
        /// The data repository
        /// </summary>
        protected IWizardFactoryRepository DataRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="WizardFactory"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public WizardFactory(IWizardFactoryRepository repository)
        {
            DataRepository = repository;
        }

        /// <inheritdoc />
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
        public virtual IWizardContent CreateWizardScreenContent(string groupKey, string screenKey, object formData = null, string language = null, string groupFilter = null)
        {
            var fieldData = DataRepository.GetFieldData(groupKey)?.ToList() ?? new List<WizardContentField>();
            var wizard = fieldData?.FirstOrDefault(o => o.FieldType.Equals("Screen", StringComparison.InvariantCultureIgnoreCase) && o.FieldKey.Equals(screenKey, StringComparison.InvariantCultureIgnoreCase));
            var model = formData ?? new object();
            var content = new WizardScreenContent(screenKey, fieldData, formData);

            var fields = fieldData.Where(o => o.ParentKey.Equals(wizard?.FieldKey, StringComparison.CurrentCultureIgnoreCase)).OrderBy(o => o.Order).ToList();
            var formList = fields.Where(o => o.FieldType.Equals("Form", StringComparison.InvariantCultureIgnoreCase) || o.FieldType.Equals("Embed", StringComparison.InvariantCultureIgnoreCase)).OrderBy(o => o.Order).ToList();
            //string embedData = fields.FirstOrDefault(o => o.FieldType.Equals("Embed", StringComparison.InvariantCultureIgnoreCase))?.FieldData ?? "{}";
//            var embedList = fields.Where(o => o.FieldType.Equals("Embed", StringComparison.InvariantCultureIgnoreCase)).OrderBy(o => o.Order).ToList();
            foreach (var form in formList)
            {
                WizardFormData? formElem = null;
                if (form.FieldType.ToLower() == "form")
                    formElem = CreateWizardFormData(form.FieldKey, fieldData);
                if (form.FieldType.ToLower() == "embed")
                    formElem = CreateEmbeddedScreenFormData(form, fieldData);

                if (formElem != null)
                    ((List<WizardFormData>)content.Forms).Add(formElem);
            }

            ((List<WizardFormData>)content.Forms).Sort((o1, o2) => o1.Order - o2.Order);
            ProcessPlaceholders(content, formData);
            return content;
        }

        /// <inheritdoc />
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
        public virtual void AddEmbeddedForm(string key, IWizardContent content, IEnumerable<WizardContentField> extraFields = null, bool isDisabled = false, bool hasEditButton = false)
        {
            List<WizardContentField> embed = (List<WizardContentField>)DataRepository.GetFieldData(key);
            var wizard = embed?.FirstOrDefault(o => o.FieldType.Equals("Screen", StringComparison.InvariantCultureIgnoreCase) || o.FieldType.Equals("Wizard", StringComparison.InvariantCultureIgnoreCase));
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

        /// ToDo: Add XMLDoc comments
        public virtual WizardFormData CreateEmbeddedScreenFormData(IWizardContentField embed, IEnumerable<WizardContentField> fieldData)
        {
            if (embed is null || fieldData is null)
                return null;

            string embedData = embed.FieldData ?? "{}";
            bool isDisabled = false;
            bool hasEditButton = false;
            bool useCardLayout = false;
            string screenName = null;
            string formName = null;
            try
            {
                JToken dta = JToken.Parse(embedData);
                isDisabled = DataUtility.ParseNullableBool(dta, "disabled") ?? DataUtility.ParseBool(dta, "disabled");
                useCardLayout = DataUtility.ParseBool(dta, "useCardLayout");
                screenName = DataUtility.ParseNullableString(dta, "Screen") ?? DataUtility.ParseString(dta, "screen");
                formName = DataUtility.ParseNullableString(dta, "Form") ?? DataUtility.ParseString(dta, "form");
                hasEditButton = DataUtility.ParseNullableBool(dta, "editButton") ?? DataUtility.ParseBool(dta, "hasEditButton");
            }
            catch { }

            //if (string.IsNullOrWhiteSpace(screenName) || string.IsNullOrWhiteSpace(formName))
            //    return null;
            var form = fieldData.FirstOrDefault(o => (o.FieldType.Equals("Form", StringComparison.InvariantCultureIgnoreCase) || o.FieldType.Equals("Embed", StringComparison.InvariantCultureIgnoreCase)) && o.FieldKey.Equals(formName, StringComparison.InvariantCultureIgnoreCase) && o.ParentKey.Equals(screenName, StringComparison.InvariantCultureIgnoreCase));
            WizardButtonData editButton = null;

            if (form != null)
            {
                //return null;

                if (hasEditButton)
                {
                    JToken data = form.GetFieldData();
                    editButton = new WizardButtonData
                    {
                        Class = "btn btn-sm btn-outline-primary",
                        Label = "Edit",
                        Icon = "fa fa-edit",
                        Action = new
                        {
                            type = "nav",
                            screen = screenName
                        }
                    };
                }
            }

            var returnObject = new EmbededFormData(embed, formName, screenName, fieldData, editButton);
            returnObject.UseCardLayout = useCardLayout;

            string strInput;

            strInput = form?.FieldData?.Trim();
            if (string.IsNullOrWhiteSpace(strInput)) return returnObject;
            var fields = fieldData.Where(o => o.ParentKey.Equals(form.FieldKey, StringComparison.CurrentCultureIgnoreCase)).OrderBy(o => o.Order).ToList();
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
                            field["FieldData"] = dta;
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                }
                field["FieldData"] = field["FieldData"] ?? new JObject();

                try
                {
                    if (isDisabled)
                    {
                        string props = DataUtility.ParseString(field["FieldData"], "props") ?? "";
                        props = props.Replace("disabled=\"disabled\"", "");
                        field["FieldData"]["props"] = props + " disabled=\"disabled\"";
                    }
                }
                catch
                {
                    // ignored
                }

                ((List<JToken>)(returnObject.Fields)).Add(field);
            }

            return returnObject;
        }

        /// ToDo: Add XMLDoc comments
        public void ProcessPlaceholders(IWizardContentField field, object replacementObject)
        {
            if (field is null || replacementObject is null)
                return;

            field.Label = ResolvePlaceholders(field.Label, replacementObject);
            field.WrapperHtmlStart = ResolvePlaceholders(field.WrapperHtmlStart, replacementObject);
            field.WrapperHtmlEnd = ResolvePlaceholders(field.WrapperHtmlEnd, replacementObject);
            switch (field)
            {
                case WizardScreenContent screenField:
                {
                    ProcessPlaceholders(screenField.BodyData, replacementObject);
                    ProcessPlaceholders(screenField.HeadingData, replacementObject);

                    screenField.Body = ResolvePlaceholders(screenField.Body, replacementObject);
                    screenField.Heading = ResolvePlaceholders(screenField.Heading, replacementObject);
                    screenField.FieldData = ResolveDataPlaceholders(JToken.FromObject(screenField.FieldData ?? new object()), replacementObject);

                    if (screenField.Forms?.Count() > 0)
                    {
                        foreach (var formField in screenField.Forms)
                        {
                            ProcessPlaceholders(formField, replacementObject);
                        }
                    }

                    if (screenField.Buttons?.Count() > 0)
                    {
                        foreach (var formField in screenField.Buttons)
                        {
                            ProcessPlaceholders(formField, replacementObject);
                        }
                    }

                    break;
                }
                case WizardFormData formField:
                    var fields = formField.Fields.ToList();
                    if (fields.Count > 0)
                    {
                        for (var i = 0; i < fields.Count; i++)
                        {
                            ResolveDataPlaceholders(fields[i], replacementObject);
                        }
                        
                    }
                    break;
            }
        }

        /// ToDo: Add XMLDoc comments
        protected virtual JToken ResolveDataPlaceholders(JToken source, object replacementObject = null)
        {
            if (replacementObject is null || source is null) return source;

            JToken data;
            if (!(replacementObject is JToken token))
                data = JToken.FromObject(replacementObject);
            else
                data = token;

            JTokenType t = source.Type;


            switch (t)
            {
                case JTokenType.Object:
                case JTokenType.Array:
                    var keys = source.Select(o => o.Path).ToList();
                    foreach (var key in keys)
                    {
                        var value = ResolveDataPlaceholders(source[key], data);
                        if (value != null)
                            source[key].Replace(value);
                    }
                    break;
                case JTokenType.String:
                    return ResolvePlaceholders(source.ToString(), data);
            }

            return source;
        }

        /// ToDo: Add XMLDoc comments
        public virtual string ResolvePlaceholders(string sourceString, object replacementObject = null)
        {
            if (replacementObject is null) return sourceString;

            JToken data;
            if (!(replacementObject is JToken token))
                data = JToken.FromObject(replacementObject);
            else
                data = token;

            string s = sourceString;
            var re = new Regex(@"\[%([\w\d\-_\s]*)%\]", RegexOptions.CultureInvariant);

            return re.Replace(s, (match) => ResolvePlaceholderMatchCallback(match, data));
        }

        /// ToDo: Add XMLDoc comments
        protected virtual string ResolvePlaceholderMatchCallback(Match match, JToken replacementObjectData = null)
        {
            string key;
            if (match?.Groups.Count > 1 && replacementObjectData?.SelectToken(key = match?.Groups[1].Value.Trim()) != null)
            {
                return replacementObjectData.SelectToken(key)?.ToString();
            }

            return match?.Value;
        }

    }
}
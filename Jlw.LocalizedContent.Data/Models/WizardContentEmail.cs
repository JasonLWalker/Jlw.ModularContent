// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-27-2021
//
// Last Modified By : jlwalker
// Last Modified On : 05-15-2023
// ***********************************************************************
// <copyright file="WizardContentEmail.cs" company="Jason L. Walker">
//     Copyright ©2012-2023 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace Jlw.LocalizedContent
{
    /// <inheritdoc />
    public class WizardContentEmail : IWizardContentEmail
    {
        /// <inheritdoc />
        public string GroupKey { get; set; }

        /// <inheritdoc />
        public string Subject { get; set; }
        /// <inheritdoc />
        public string Body { get; set; }

        /// <inheritdoc />
        public object FormData { get; set; }
        
        /// <summary>
        /// Constructor used to initialize the email
        /// </summary>
        /// <param name="parentKey"></param>
        /// <param name="fieldData"></param>
        /// <param name="formData"></param>
        public WizardContentEmail(string parentKey, IEnumerable<IWizardContentField> fieldData, object formData = null)
        {

            var data = fieldData?.ToList();
            var message = data?.FirstOrDefault(o => o.FieldType.Equals("Email", StringComparison.InvariantCultureIgnoreCase) && o.FieldKey.Equals(parentKey, StringComparison.InvariantCultureIgnoreCase));
            FormData = formData ?? new object();
            GroupKey = message?.GroupKey ?? data?.FirstOrDefault()?.GroupKey ?? "";

            if (message == null)
            {
                Subject = "";
                Body = "";
                return;
            }
            var fields = data.Where(o => o.ParentKey.Equals(message.FieldKey, StringComparison.CurrentCultureIgnoreCase)).OrderBy(o => o.Order).ToList();
            Subject = fields.FirstOrDefault(o => o.FieldType.Equals("Subject", StringComparison.InvariantCultureIgnoreCase))?.Label ?? "";
            Body = fields.FirstOrDefault(o => o.FieldType.Equals("Body", StringComparison.InvariantCultureIgnoreCase))?.Label ?? "";
            ResolvePlaceholders(formData);
        }

        /// <summary>
        /// Resolve any placeholders in the subject or body of the email using the data passed into the reference object
        /// </summary>
        /// <param name="o">Reference object</param>
        protected virtual void ResolvePlaceholders(object o)
        {
            JToken data = JToken.FromObject(o ?? new object());

            foreach (var token in data)
            {
                var re = new Regex(@"\[%\s*" + token.Path + @"\s*%\]", RegexOptions.CultureInvariant);

                string val = data[token.Path].ToString();
                {
                    Subject = re.Replace(Subject, val);
                    Body = re.Replace(Body, val);
                }
            }
        }
    }
}

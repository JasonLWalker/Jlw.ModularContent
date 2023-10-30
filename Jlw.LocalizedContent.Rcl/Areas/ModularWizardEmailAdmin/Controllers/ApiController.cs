// ***********************************************************************
// Assembly         : Jlw.Web.Rcl.LocalizedContent
// Author           : jlwalker
// Created          : 05-21-2021
//
// Last Modified By : jlwalker
// Last Modified On : 05-15-2023
// ***********************************************************************
// <copyright file="ApiController.cs" company="Jason L. Walker">
//     Copyright ©2019-2023 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Linq;
using Jlw.Utilities.Data;
using Jlw.Utilities.Data.DataTables;
using Jlw.Utilities.WebApiUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Jlw.ModularContent.Areas.ModularWizardEmailAdmin.Controllers 
{
    /// <summary>
    /// Class ApiController.
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    /// TODO Edit XML Comment Template for ApiController
    [ApiController]
    [Authorize]
    [Produces("application/json")] 
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] 
    [JsonConverter(typeof(DefaultContractResolver))]
    [JsonObject(NamingStrategyType = typeof(DefaultNamingStrategy))]
	public abstract class ApiController : ControllerBase 
	{
        /// <summary>
        /// The data repository instance
        /// </summary>
        protected readonly ILocalizedContentFieldRepository _fieldRepository;
        protected readonly IWizardFactoryRepository _factoryRepository;
        protected readonly IModularContentTextRepository _textRepository;
        /// <summary>
        /// The primary group filter
        /// for this class. This will be set in descendant classes to allow those classes to only modify a subset of database records.
        /// </summary>
        protected string _groupFilter;

        /// <summary>
        /// Set this flag to true when overriding API in order to enable access to API methods
        /// </summary>
        protected bool _unlockApi = false; 

        
        /// <summary>
        /// Class LocalizedContentFieldRecordInput.
        /// Implements the <see cref="ModularContentField" /> model with public property setters. This class will be used to pass input data from the client to the API methods.
        /// </summary>
        public class ModularWizardEmailRecordInput : WizardContentField
		{
            /// <summary>
            /// Gets or sets the edit token.
            /// </summary>
            /// <value>The edit token.</value>
            /// TODO Edit XML Comment Template for EditToken
            public string EditToken { get; set; }
            /// <inheritdoc />
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
            [JsonConverter(typeof(JlwJsonConverter<long>))]
			public new long Id  { get=>base.Id; set => base.Id = value; }
            /// <inheritdoc />
			public new string GroupKey  { get=>base.GroupKey; set=>base.GroupKey = value; }
            /// <inheritdoc />
			public new string FieldKey  { get=>base.FieldKey; set=>base.FieldKey = value; }
            /// <inheritdoc />
			public new string FieldType  { get=>base.FieldType; set=>base.FieldType = value; }
            /// <inheritdoc />
			public new string FieldData  { get=>base.FieldData; set=>base.FieldData = value; }
            /// <inheritdoc />
			public new string FieldClass  { get=>base.FieldClass; set=>base.FieldClass = value; }
            /// <inheritdoc />
			public new string ParentKey  { get=>base.ParentKey; set=>base.ParentKey = value; }
            /// <inheritdoc />
			public new string DefaultLabel  { get=>base.DefaultLabel; set=>base.DefaultLabel = value; }
            /// <inheritdoc />
            public new string Label { get=>base.Label; set=>base.Label = value; }
            /// <inheritdoc />
			public new string WrapperClass  { get=>base.WrapperClass; set=>base.WrapperClass = value; }
            /// <inheritdoc />
			public new string WrapperHtmlStart  { get=>base.WrapperHtmlStart; set=>base.WrapperHtmlStart = value; }
            /// <inheritdoc />
			public new string WrapperHtmlEnd  { get=>base.WrapperHtmlEnd; set=>base.WrapperHtmlEnd = value; }
            /// <inheritdoc />
			public new string AuditChangeType  { get=>base.AuditChangeType; set=>base.AuditChangeType = value; }
            /// <inheritdoc />
			public new string AuditChangeBy  { get=>base.AuditChangeBy; set=>base.AuditChangeBy = value; }
            /// <inheritdoc />
			public new DateTime AuditChangeDate  { get=>base.AuditChangeDate; set=>base.AuditChangeDate = value; }
            /// <inheritdoc />
			public new int Order  { get; set; }
            /// <inheritdoc />
            public new string GroupFilter { get; set; }
            public string Language { get; set; }
            public string LanguageFilter { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }

            public ModularWizardEmailRecordInput() : base(null) {}

            public ModularWizardEmailRecordInput(object o) : base(o)
            {
                Initialize(o);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// TODO Edit XML Comment Template for #ctor
        public ApiController(ILocalizedContentFieldRepository fieldRepository, IModularContentTextRepository textRepository, IWizardFactoryRepository factoryRepository) 
        { 
            _fieldRepository = fieldRepository;
            _textRepository = textRepository;
            _factoryRepository = factoryRepository;
			_groupFilter = "";
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>System.Object.</returns>
        /// TODO Edit XML Comment Template for Index
        [HttpPost]
        public virtual object Index()
        {
            return new {};
        }

        /// <summary>
        /// Dts the list.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns>System.Object.</returns>
        /// TODO Edit XML Comment Template for DtList
        [HttpPost("DtList")]
        public virtual object DtList([FromForm] LocalizedContentFieldDataTablesInput o)
        {
            o.GroupFilter = _groupFilter;

            if (!_unlockApi) return JToken.FromObject(new DataTablesOutput(o));
            JToken data = JToken.FromObject(_fieldRepository.GetDataTableList(o));
            if (data?.FirstOrDefault(x => x.Path == "data") != null)
            {
                foreach (var row in data["data"]!)
                {
                    var parent = row.ToObject<WizardContentField>();
                    string lang = string.IsNullOrWhiteSpace(row["Language"]?.ToString())
                        ? null
                        : row["Language"].ToString();
                    string groupKey = parent?.GroupKey;
                    string fieldKey = parent?.FieldKey;
                    var fields = _factoryRepository.GetWizardFields(groupKey, fieldKey, lang, _groupFilter).ToList();
                    fields.Add(parent);
                    var oEmail = new WizardContentEmail(fieldKey, fields, new { });
                    row["Body"] = oEmail.Body;
                    row["Subject"] = oEmail.Subject;
                }
            }

            return data;
        }

        /// <summary>
        /// Returns the record specified by the input from oData.
        /// </summary>
        /// <param name="oData">The input object.</param>
        /// <returns>System.Object.</returns>
        /// TODO Edit XML Comment Template for Data
        [HttpPost("Data")]
        public virtual object Data(ModularWizardEmailRecordInput oData)
        {
            ModularWizardEmailRecordInput oReturn;
            oData.GroupFilter = _groupFilter;
            //oData.Language = string.IsNullOrWhiteSpace(oData.Language) ? "EN" : oData.Language;

            if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));

            try
            {
                //var parent = _fieldRepository.GetRecord(oData);
                var parent = _factoryRepository.GetRecord(oData);
                if (parent == null || parent.Id < 1)
                    return JToken.FromObject(new ApiStatusMessage("Unable to locate a matching record.", "Record not found", ApiMessageType.Danger));

                oReturn = new ModularWizardEmailRecordInput(parent);
                string lang = string.IsNullOrWhiteSpace(oData.Language) ? null : oData.Language;
                

                var fields = _factoryRepository.GetWizardFields(oReturn.GroupKey, oReturn.FieldKey, lang, _groupFilter).ToList();
                fields.Add(new WizardContentField(parent));
                var oEmail = new WizardContentEmail(oData.FieldKey, fields, new { });
                oReturn.Language = lang ?? "EN";
                oReturn.Body = oEmail.Body;
                oReturn.Subject = oEmail.Subject;
                oReturn.Label = parent.Label;
            }
            catch (Exception ex)
            {
                return JToken.FromObject(new ApiExceptionMessage("An error has occurred", ex));
            }

            return JToken.FromObject(oReturn);
        }

        /// <summary>
        /// Saves the specified o.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns>System.Object.</returns>
        /// TODO Edit XML Comment Template for Save
        [HttpPost("Save")]
        public virtual object Save(ModularWizardEmailRecordInput o)
        {
            var bResult = false;
            o.GroupFilter = _groupFilter;
            o.Language = string.IsNullOrWhiteSpace(o.Language) ? "EN" : o.Language;
            if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));

            IModularContentField oResult = null;
            //try
            {
                o.AuditChangeBy = User.Identity.Name;
                if (o.Id < 1)
                {
                    var record = _factoryRepository.GetWizardFields(o.GroupKey, _groupFilter).FirstOrDefault(x=>x.GroupKey.Equals(o.GroupKey, StringComparison.InvariantCulture) && x.FieldKey.Equals(o.FieldKey, StringComparison.InvariantCulture) && x.FieldType.Equals("EMAIL", StringComparison.InvariantCulture));
                    o.Id = record?.Id ?? 0;
                }

                oResult = _fieldRepository.SaveRecord(o);

                if (oResult != null)
                {
                    bResult = true;
                    var text = JObject.FromObject(oResult);
                    text["Text"] = o.Label;
                    text["AuditChangeBy"] = o.AuditChangeBy;
                    text["Language"] = o.Language;
                    _textRepository.SaveRecord(text.ToObject<ModularContentText>());
                }
                else
                {
                    return JToken.FromObject(new ApiStatusMessage("Unable to save record. Please check the data and try again.", "Error while saving", ApiMessageType.Danger));
                }

                var fields = _factoryRepository.GetWizardFields(oResult.GroupKey, oResult.FieldKey, o.Language, _groupFilter).ToList();
                IModularContentField body = fields.FirstOrDefault(x => x.GroupKey == oResult.GroupKey && x.ParentKey == oResult.FieldKey && x.FieldType.ToUpper() == "BODY");
                IModularContentField subject = fields.FirstOrDefault(x => x.GroupKey == oResult.GroupKey && x.ParentKey == oResult.FieldKey && x.FieldType.ToUpper() == "SUBJECT");
                if (body is null)
                {
                    var field = JObject.FromObject(o);
                    field["AuditChangeBy"] = o.AuditChangeBy;
                    field["DefaultLabel"] = "";
                    field["FieldType"] = "BODY";
                    field["FieldKey"] = o.FieldKey + "_Body";
                    field["ParentKey"] = o.FieldKey;
                    body = _fieldRepository.SaveRecord(field.ToObject<ModularContentField>());
                }

                if (body != null)
                {
                    var text = JObject.FromObject(body);
                    text["Text"] = o.Body;
                    text["AuditChangeBy"] = o.AuditChangeBy;
                    text["Language"] = o.Language;
                    text["GroupFilter"] = _groupFilter;
                    _textRepository.SaveRecord(text.ToObject<ModularContentText>());
                }

                if (subject is null)
                {
                    var field = JObject.FromObject(o);
                    field["AuditChangeBy"] = o.AuditChangeBy;
                    field["DefaultLabel"] = "";
                    field["FieldType"] = "SUBJECT";
                    field["FieldKey"] = o.FieldKey + "_Subject";
                    field["ParentKey"] = o.FieldKey;
                    subject = _fieldRepository.SaveRecord(field.ToObject<ModularContentField>());
                }

                if (subject != null)
                {
                    var text = JObject.FromObject(subject);
                    text["Text"] = o.Subject;
                    text["AuditChangeBy"] = o.AuditChangeBy;
                    text["Language"] = o.Language;
                    text["GroupFilter"] = _groupFilter;
                    _textRepository.SaveRecord(text.ToObject<ModularContentText>());
                }


                //_LocalizedContentFieldList.Refresh(); 
            }
            //catch (Exception ex)
            {
                //return JToken.FromObject(new ApiExceptionMessage("An error has occurred", ex));
            }

            if (bResult == true)
                return JToken.FromObject(new ApiObjectMessage(oResult, "Record has been saved successfully.", "Record Saved", ApiMessageType.Success));

            // Else 
            return JToken.FromObject(new ApiStatusMessage("Unable to save record. Please check the data and try again.", "Error while saving", ApiMessageType.Danger));
        }

        /// <summary>
        /// Deletes the specified o.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns>System.Object.</returns>
        /// TODO Edit XML Comment Template for Delete
        [HttpPost("Delete")]
        public virtual object Delete(ModularWizardEmailRecordInput o)
        {
            var bResult = true;
            o.AuditChangeBy = User.Identity?.Name;
            o.GroupFilter = _groupFilter;

            if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));

            try
            {
                o.AuditChangeBy = User.Identity?.Name;
                var oData = _factoryRepository.DeleteWizardFieldRecursive(o, 5, o.LanguageFilter ?? "");
                if (!string.IsNullOrWhiteSpace(o.LanguageFilter))
                    bResult = false;
                else
                    bResult = oData != null;
            }
            catch (Exception ex)
            {
                return JToken.FromObject(new ApiExceptionMessage("An error has occurred", ex));
            }

            if (bResult != true)
                return JToken.FromObject(new ApiStatusMessage("Record has been successfully deleted.", "Record Deleted", ApiMessageType.Success));

            // Else 
            return JToken.FromObject(new ApiStatusMessage("Unable to delete record. Please check the data and try again.", "Error while deleting", ApiMessageType.Danger));
		}
    }
}
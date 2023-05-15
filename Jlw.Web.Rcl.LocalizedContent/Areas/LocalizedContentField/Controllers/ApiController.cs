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
using Jlw.Data.LocalizedContent;
using Jlw.Utilities.Data;
using Jlw.Utilities.Data.DataTables;
using Jlw.Utilities.WebApiUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Jlw.Web.Rcl.LocalizedContent.Areas.LocalizedContentField.Controllers 
{
    /// <summary>
    /// Class ApiController.
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    /// TODO Edit XML Comment Template for ApiController
    [Area("LocalizedContentField")]
    [ApiController]
    [Authorize]
    [Produces("application/json")] 
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] 
	[Route("admin/[area]/[controller]")]
    [JsonConverter(typeof(DefaultContractResolver))]
    [JsonObject(NamingStrategyType = typeof(DefaultNamingStrategy))]
	public abstract class ApiController : ControllerBase 
	{
        /// <summary>
        /// The data repository instance
        /// </summary>
        protected readonly ILocalizedContentFieldRepository _repo;

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
        /// Implements the <see cref="LocalizedContentField" /> model with public property setters. This class will be used to pass input data from the client to the API methods.
        /// </summary>
        public class LocalizedContentFieldRecordInput : Data.LocalizedContent.LocalizedContentField, ILocalizedContentField
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
            public new long Id
            {
                get => base.Id;
                set => base.Id = value;
            }

            /// <inheritdoc />
            public new string GroupKey
            {
                get => base.GroupKey;
                set => base.GroupKey = value;
            }

            /// <inheritdoc />
            public new string FieldKey
            {
                get => base.FieldKey; 
                set => base.FieldKey = value;
            }

            /// <inheritdoc />
            public new string FieldType
            {
                get => base.FieldType; 
                set => base.FieldType = value;
            }

            /// <inheritdoc />
            public new string FieldData
            {
                get => base.FieldData; 
                set => base.FieldData = value;
            }

            /// <inheritdoc />
            public new string FieldClass
            {
                get => base.FieldClass; 
                set => base.FieldClass = value;
            }

            /// <inheritdoc />
            public new string ParentKey
            {
                get => base.ParentKey; 
                set => base.ParentKey = value;
            }

            /// <inheritdoc />
            public new string DefaultLabel
            {
                get => base.DefaultLabel; 
                set => base.DefaultLabel = value;
            }

            /// <inheritdoc />
            public new string WrapperClass
            {
                get => base.WrapperClass; 
                set => base.WrapperClass = value;
            }

            /// <inheritdoc />
            public new string WrapperHtmlStart
            {
                get => base.WrapperHtmlStart; 
                set => base.WrapperHtmlStart = value;
            }

            /// <inheritdoc />
            public new string WrapperHtmlEnd
            {
                get => base.WrapperHtmlEnd; 
                set => base.WrapperHtmlEnd = value;
            }

            /// <inheritdoc />
            public new string AuditChangeType
            {
                get => base.AuditChangeType; 
                set => base.AuditChangeType = value;
            }

            /// <inheritdoc />
            public new string AuditChangeBy
            {
                get => base.AuditChangeBy; 
                set => base.AuditChangeBy = value;
            }

            /// <inheritdoc />
            public new DateTime AuditChangeDate
            {
                get => base.AuditChangeDate; 
                set => base.AuditChangeDate = value;
            }

            /// <inheritdoc />
            public new int Order
            {
                get => base.Order; 
                set => base.Order = value;
            }

            /// <inheritdoc />
            public new string GroupFilter
            {
                get => base.GroupFilter; 
                set => base.GroupFilter = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// TODO Edit XML Comment Template for #ctor
        public ApiController(ILocalizedContentFieldRepository repository) 
        { 
            _repo = repository;
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

            return JToken.FromObject(_repo.GetDataTableList(o));
        }

        /// <summary>
        /// Datas the specified o.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns>System.Object.</returns>
        /// TODO Edit XML Comment Template for Data
        [HttpPost("Data")]
        public virtual object Data(LocalizedContentFieldRecordInput o)
        {
            ILocalizedContentField oResult;
            o.GroupFilter = _groupFilter;

            if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));

            try
            {
                oResult = _repo.GetRecord(o);
            }
            catch (Exception ex)
            {
                return JToken.FromObject(new ApiExceptionMessage("An error has occurred", ex));
            }

            if (oResult == null || oResult.Id < 1)
                return JToken.FromObject(new ApiStatusMessage("Unable to locate a matching record.", "Record not found", ApiMessageType.Danger));

            return JToken.FromObject(oResult);
        }

        /// <summary>
        /// Saves the specified o.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns>System.Object.</returns>
        /// TODO Edit XML Comment Template for Save
        [HttpPost("Save")]
        public virtual object Save(LocalizedContentFieldRecordInput o)
        {
            var bResult = false;
            o.GroupFilter = _groupFilter;

            if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));

            ILocalizedContentField oResult = null;
            try
            {
                o.AuditChangeBy = User.Identity.Name;
                oResult = _repo.SaveRecord(o);
                bResult = oResult != null;

                //_LocalizedContentFieldList.Refresh(); 
            }
            catch (Exception ex)
            {
                return JToken.FromObject(new ApiExceptionMessage("An error has occurred", ex));
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
        public virtual object Delete(LocalizedContentFieldRecordInput o)
        {
            var bResult = false;
            o.GroupFilter = _groupFilter;

            if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));

            try
            {
                o.AuditChangeBy = User.Identity.Name;
                //bResult = 
                var oResult = _repo.DeleteRecord(o);
                bResult = oResult != null;
                //_LocalizedContentFieldList.Refresh(); 
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
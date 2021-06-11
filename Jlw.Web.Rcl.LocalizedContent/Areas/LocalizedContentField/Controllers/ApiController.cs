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
    [Area("LocalizedContentField")]
    [ApiController]
    //[Authorize("LocalizedContentUser")]
    [Produces("application/json")] 
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] 
	[Route("admin/[area]/[controller]")]
    [JsonConverter(typeof(DefaultContractResolver))]
    [JsonObject(NamingStrategyType = typeof(DefaultNamingStrategy))]
	public abstract class ApiController : ControllerBase 
	{ 
        protected readonly ILocalizedContentFieldRepository _repo;
        protected string _groupFilter;

 
		public class LocalizedContentFieldRecordInput : Data.LocalizedContent.LocalizedContentField 
		{ 
			public string EditToken { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
            [JsonConverter(typeof(JlwJsonConverter<long>))]
			public new long Id  { get; set; } 
			public new string GroupKey  { get; set; }
			public new string FieldKey  { get; set; } 
			public new string FieldType  { get; set; } 
			public new string FieldData  { get; set; } 
			public new string FieldClass  { get; set; } 
			public new string ParentKey  { get; set; } 
			public new string DefaultLabel  { get; set; } 
			public new string WrapperClass  { get; set; } 
			public new string WrapperHtmlStart  { get; set; } 
			public new string WrapperHtmlEnd  { get; set; } 
			public new string AuditChangeType  { get; set; } 
			public new string AuditChangeBy  { get; set; } 
			public new DateTime AuditChangeDate  { get; set; } 
			public new int Order  { get; set; }
            public new string GroupFilter { get; set; }

		}

		public ApiController(ILocalizedContentFieldRepository repository) 
        { 
            _repo = repository;
			_groupFilter = "";
        }

        [HttpPost]
        public virtual object Index()
        {
            return new {};
        }

		[HttpPost("DtList")]
        public virtual object DtList([FromForm] LocalizedContentFieldDataTablesInput o)
        {
            o.GroupFilter = _groupFilter;
            return JToken.FromObject(_repo.GetDataTableList(o));
        }

		[HttpPost("Data")]
        public virtual object Data(LocalizedContentFieldRecordInput o)
        {
            ILocalizedContentField oResult;
            o.GroupFilter = _groupFilter;

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

		[HttpPost("Save")]
        public virtual object Save(LocalizedContentFieldRecordInput o)
        {
            var bResult = false;
            o.GroupFilter = _groupFilter;

            try
            {
                o.AuditChangeBy = User.Identity.Name;
                var oResult = _repo.SaveRecord(o);
                bResult = oResult != null;

                //_LocalizedContentFieldList.Refresh(); 
            }
            catch (Exception ex)
            {
                return JToken.FromObject(new ApiExceptionMessage("An error has occurred", ex));
            }

            if (bResult == true)
                return JToken.FromObject(new ApiStatusMessage("Record has been saved successfully.", "Record Saved", ApiMessageType.Success));

            // Else 
            return JToken.FromObject(new ApiStatusMessage("Unable to save record. Please check the data and try again.", "Error while saving", ApiMessageType.Danger));
        }

		[HttpPost("Delete")]
        public virtual object Delete(LocalizedContentFieldRecordInput o)
        {
            var bResult = false;
            o.GroupFilter = _groupFilter;

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
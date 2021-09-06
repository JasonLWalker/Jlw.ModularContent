using System;
using Jlw.Data.LocalizedContent;
using Jlw.Utilities.Data.DataTables;
using Jlw.Utilities.WebApiUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Jlw.Web.Rcl.LocalizedContent.Areas.LocalizedContentText.Controllers 
{
    [Area("LocalizedContentText")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Route("admin/[area]/[controller]")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [JsonConverter(typeof(DefaultContractResolver))]
    [JsonObject(NamingStrategyType = typeof(DefaultNamingStrategy))]
	public abstract class ApiController : ControllerBase 
	{ 
        private readonly ILocalizedContentTextRepository _repo;
        protected string _groupFilter;
        protected bool _unlockApi = false; // Set this flag to true when overriding API in order to enable access to API methods

		public class LocalizedContentTextRecordInput : Data.LocalizedContent.LocalizedContentText
		{ 
			public string EditToken { get; set; } 
			public new string GroupKey  { get; set; } 
			public new string FieldKey  { get; set; } 
			public new string Language  { get; set; } 
			public new string Text  { get; set; } 
			public new string AuditChangeType  { get; set; } 
			public new string AuditChangeBy  { get; set; } 
			public new DateTime AuditChangeDate  { get; set; }
            public string GroupFilter { get; set; }
		}

		public ApiController (ILocalizedContentTextRepository repository) 
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
		public virtual object DtList([FromForm]LocalizedContentTextDataTablesInput o) 
		{
            o.GroupFilter = _groupFilter;

            if (!_unlockApi) return JToken.FromObject(new DataTablesOutput(o));

            return JToken.FromObject(_repo.GetDataTableList(o));
        }

		[HttpPost("Data")] 
		public virtual object Data(LocalizedContentTextRecordInput o) 
		{ 
			ILocalizedContentText oResult;
            if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));
            o.GroupFilter = _groupFilter;

			try
			{ 
				oResult = _repo.GetRecord(o); 
			} 
			catch (Exception ex) 
			{ 
				return JToken.FromObject(new ApiExceptionMessage("An error has occurred", ex)); 
			} 
 
 
			return JToken.FromObject(oResult); 
		} 
 
		[HttpPost("Save")] 
		public virtual object Save(LocalizedContentTextRecordInput o) 
		{ 
			var bResult = false;
            if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));

            o.GroupFilter = _groupFilter;
            o.AuditChangeBy = User.Identity.Name;
            try
			{ 
				var oResult = _repo.SaveRecord(o); 
                bResult = oResult != null; 
 
                //_LocalizedContentTextList.Refresh(); 
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
		public virtual object Delete(LocalizedContentTextRecordInput o) 
		{ 
			var bResult = false;

            if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));
			
            o.GroupFilter = _groupFilter;
            o.AuditChangeBy = User.Identity.Name;
			try 
			{ 
				//bResult = 
                var oResult = _repo.DeleteRecord(o); 
                bResult = oResult != null; 
                //_LocalizedContentTextList.Refresh(); 
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
using System;
using Jlw.Utilities.Data.DataTables;
using Jlw.Utilities.WebApiUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Jlw.LocalizedContent.Areas.LocalizedGroupDataItem.Controllers 
{
    [Area("LocalizedGroupDataItem")]
    [ApiController]
    [Authorize]
    [Produces("application/json")] 
    [Route("admin/[area]/[controller]")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [JsonConverter(typeof(DefaultContractResolver))]
    [JsonObject(NamingStrategyType = typeof(DefaultNamingStrategy))]
	public abstract class ApiController : ControllerBase 
	{
        /// <summary>
        /// The data repository instance
        /// </summary>
        private readonly ILocalizedGroupDataItemRepository _repo;

        /// <summary>
        /// The primary group filter
        /// for this class. This will be set in descendant classes to allow those classes to only modify a subset of database records.
        /// </summary>
        protected string _groupFilter;
        protected bool _unlockApi = false; // Set this flag to true when overriding API in order to enable access to API methods
        protected string _forcedGroupKey = null;

		public class LocalizedGroupDataItemRecordInput : Jlw.LocalizedContent.LocalizedGroupDataItem 
		{ 
			public string EditToken { get; set; } 
			public new long Id  { get; set; } 
			public new string Language  { get; set; } 
			public new string GroupKey  { get; set; } 
			public new string Key  { get; set; } 
			public new string Value  { get; set; } 
			public new int Order  { get; set; } 
			public new string Description  { get; set; } 
			public new string Data  { get; set; } 
            public new string AuditChangeBy { get; set; }
			public string GroupFilter { get; set; }
		}

		public ApiController(ILocalizedGroupDataItemRepository repository) 
        { 
            _repo = repository; 
        }

        [HttpPost]
        public virtual object Index()
        {
            return new { };
        }

	    [HttpPost("DtList")] 
		public virtual object GetDataTableList([FromForm]LocalizedGroupDataItemDataTablesInput o) 
		{
            o.GroupFilter = _groupFilter;
            o.GroupKey = _forcedGroupKey ?? o.GroupKey;

            if (!_unlockApi) return JToken.FromObject(new DataTablesOutput(o));

            return JToken.FromObject(_repo.GetDataTableList(o));
        }

		[HttpPost("Data")] 
		public virtual object GetRecordData(LocalizedGroupDataItemRecordInput o) 
		{ 
			ILocalizedGroupDataItem oResult;
            if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));
            o.GroupFilter = _groupFilter;
            o.GroupKey = _forcedGroupKey ?? o.GroupKey;

            try
            { 
				oResult = _repo.GetRecord(o); 
			} 
			catch (Exception ex) 
			{ 
				return new ApiExceptionMessage("An error has occurred", ex); 
			}


            return JToken.FromObject(oResult);
		}

		[HttpPost("Save")] 
		public virtual object SaveRecordData(LocalizedGroupDataItemRecordInput o) 
		{
            var bResult = false;
            if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));

            o.GroupFilter = _groupFilter;
            o.AuditChangeBy = User.Identity.Name;
            o.GroupKey = _forcedGroupKey ?? o.GroupKey;
            try
            {
                var oResult = _repo.SaveRecord(o);
                bResult = oResult != null;
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
		public virtual object DeleteRecordData(LocalizedGroupDataItemRecordInput o) 
		{
            var bResult = false;

            if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));

            o.GroupFilter = _groupFilter;
            o.GroupKey = _forcedGroupKey ?? o.GroupKey;
            o.AuditChangeBy = User.Identity.Name;
            try
            {
                //bResult = 
                var oResult = _repo.DeleteRecord(o);
                bResult = oResult != null;
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
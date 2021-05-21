using System;
using Jlw.Utilities.WebApiUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.Web.Rcl.LocalizedContent.Areas.LocalizedGroupDataItem.Controllers 
{
    [Area("LocalizedGroupDataItem")]
    [ApiController]
    [Authorize("LocalizedContentUser")]
    [Produces("application/json")] 
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Route("admin/[area]/[controller]")]
	public class ApiController : ControllerBase 
	{ 
        private readonly ILocalizedGroupDataItemRepository _repo; 
        
        public class LocalizedGroupDataItemRecordInput : Data.LocalizedContent.LocalizedGroupDataItem 
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
            //public new string AuditChangeType { get; set; }
            public new string AuditChangeBy { get; set; }
            //public new DateTime AuditChangeDate { get; set; }
		}

		public ApiController(ILocalizedGroupDataItemRepository repository) 
        { 
            _repo = repository; 
        }

        [HttpPost]
        public object Index()
        {
            return new { };
        }

	    [HttpPost("DtList")] 
		public object GetDataTableList([FromForm]LocalizedGroupDataItemDataTablesInput o) 
		{
            return _repo.GetDataTableList(o);
        }

		[HttpPost("Data")] 
		public object GetRecordData(LocalizedGroupDataItemRecordInput o) 
		{ 
			ILocalizedGroupDataItem oResult; 
 
			try 
			{ 
				oResult = _repo.GetRecord(o); 
			} 
			catch (Exception ex) 
			{ 
				return new ApiExceptionMessage("An error has occurred", ex); 
			} 
 
 
			return oResult; 
		} 
 
		[HttpPost("Save")] 
		public object SaveRecordData(LocalizedGroupDataItemRecordInput o) 
		{ 
			var bResult = false;
            o.AuditChangeBy = User.Identity.Name;
			try 
			{ 
				var oResult = _repo.SaveRecord(o); 
                bResult = oResult != null; 
 
                //_LocalizedGroupDataItemList.Refresh(); 
            } 
			catch (Exception ex)
            {
                //return new {Message = "test", MessageType = DataUtility.ParseAs(typeof(int), ApiMessageType.Success)};
                return new ApiExceptionMessage("An error has occurred", ex); 
            }

            if (bResult == true)
            {
                //_siteConfiguration.TransitionalTransferConfiguration.Initialize();
                //_siteConfiguration.ChoiceProgramConfiguration.Initialize();
                return new ApiStatusMessage("Record has been saved successfully.", "Record Saved", ApiMessageType.Success);
            }

            // Else 
			return new ApiStatusMessage("Unable to save record. Please check the data and try again.", "Error while saving", ApiMessageType.Danger); 
		}
        
        [HttpPost("Delete")] 
		public object DeleteRecordData(LocalizedGroupDataItemRecordInput o) 
		{ 
			var bResult = false; 
 
			try 
			{ 
				//bResult = 
                var oResult = _repo.DeleteRecord(o); 
                bResult = oResult != null; 
                //_LocalizedGroupDataItemList.Refresh(); 
            } 
			catch (Exception ex) 
			{ 
				return new ApiExceptionMessage("An error has occurred", ex); 
			} 
 
			if (bResult != true) 
				return new ApiStatusMessage("Record has been successfully deleted.", "Record Deleted", 
					ApiMessageType.Success); 
 
			// Else 
			return new ApiStatusMessage("Unable to delete record. Please check the data and try again.", 
				"Error while deleting", ApiMessageType.Danger); 
		} 
 
    } 
}
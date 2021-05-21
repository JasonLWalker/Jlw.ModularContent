using System;
using Jlw.Utilities.WebApiUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.Web.Rcl.LocalizedContent.Areas.LocalizedContentText.Controllers 
{
    [Area("LocalizedContentText")]
    [ApiController]
    [Authorize("LocalizedContentUser")]
    [Produces("application/json")]
    [Route("admin/[area]/[controller]")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] 
	public class ApiController : ControllerBase 
	{ 
        private readonly ILocalizedContentTextRepository _repo; 
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
		} 
 
		public ApiController (ILocalizedContentTextRepository repository) 
        { 
            _repo = repository; 
        }

        [HttpPost]
        public object Index()
        {
            return new {};
        }


		[HttpPost("DtList")] 
		public object GetDataTableList([FromForm]LocalizedContentTextDataTablesInput o) 
		{
            return _repo.GetDataTableList(o);
        }

		[HttpPost("Data")] 
		public object GetRecordData(LocalizedContentTextRecordInput o) 
		{ 
			ILocalizedContentText oResult; 
 
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
		public object SaveRecordData(LocalizedContentTextRecordInput o) 
		{ 
			var bResult = false;
            o.AuditChangeBy = User.Identity.Name;
			try 
			{ 
				var oResult = _repo.SaveRecord(o); 
                bResult = oResult != null; 
 
                //_LocalizedContentTextList.Refresh(); 
            } 
			catch (Exception ex) 
			{ 
				return new ApiExceptionMessage("An error has occurred", ex); 
			} 
 
			if (bResult == true) 
				return new ApiStatusMessage("Record has been saved successfully.", "Record Saved", ApiMessageType.Success); 
 
			// Else 
			return new ApiStatusMessage("Unable to save record. Please check the data and try again.", 
				"Error while saving", ApiMessageType.Danger); 
		} 
 
		[HttpPost("Delete")] 
		public object DeleteRecordData(LocalizedContentTextRecordInput o) 
		{ 
			var bResult = false;

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
				return new ApiExceptionMessage("An error has occurred", ex); 
			} 
 
			if (bResult != true) 
				return new ApiStatusMessage("Record has been successfully deleted.", "Record Deleted", ApiMessageType.Success); 
 
			// Else 
			return new ApiStatusMessage("Unable to delete record. Please check the data and try again.", 
				"Error while deleting", ApiMessageType.Danger); 
		} 
 
    } 
}
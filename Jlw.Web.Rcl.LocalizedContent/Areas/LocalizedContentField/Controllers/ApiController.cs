using System;
using Jlw.Data.LocalizedContent;
using Jlw.Utilities.WebApiUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.Web.Rcl.LocalizedContent.Areas.LocalizedContentField.Controllers 
{
	[Area("LocalizedContentField")]
    [ApiController]
    [Authorize("LocalizedContentUser")]
    [Produces("application/json")] 
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] 
	[Route("admin/[area]/[controller]")]
	public class ApiController : ControllerBase 
	{ 
        protected readonly ILocalizedContentFieldRepository _repo;
 
		public class LocalizedContentFieldRecordInput : Data.LocalizedContent.LocalizedContentField 
		{ 
			public string EditToken { get; set; }
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
		} 
 
		public ApiController(ILocalizedContentFieldRepository repository) 
        { 
            _repo = repository;
        }

        [HttpPost]
        public object Index()
        {
            return new {};
        }


		[HttpPost("DtList")] 
		public object GetDataTableList([FromForm]LocalizedContentFieldDataTablesInput o) 
		{ 
            return _repo.GetDataTableList(o);
        } 
 
		[HttpPost("Data")]
		public object GetRecordData(LocalizedContentFieldRecordInput o) 
		{ 
			ILocalizedContentField oResult; 
 
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
		public object SaveRecordData(LocalizedContentFieldRecordInput o) 
		{ 
			var bResult = false; 
 
			try
            {
                o.AuditChangeBy = User.Identity.Name;
				var oResult = _repo.SaveRecord(o); 
                bResult = oResult != null; 
 
                //_LocalizedContentFieldList.Refresh(); 
            } 
			catch (Exception ex) 
			{ 
				return new ApiExceptionMessage("An error has occurred", ex); 
			} 
 
			if (bResult == true) 
				return new ApiStatusMessage("Record has been saved successfully.", "Record Saved", 
					ApiMessageType.Success); 
 
			// Else 
			return new ApiStatusMessage("Unable to save record. Please check the data and try again.", 
				"Error while saving", ApiMessageType.Danger); 
		} 
 
		[HttpPost("Delete")] 
		public object DeleteRecordData(LocalizedContentFieldRecordInput o) 
		{ 
			var bResult = false; 
 
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
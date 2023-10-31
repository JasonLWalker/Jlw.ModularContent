using System;
using Jlw.ModularContent;
using Jlw.ModularContent.Areas.ModularContentWizardAdmin.Controllers;
using Jlw.Utilities.Data.DataTables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Jlw.Web.ModularContent.SampleWebApp.Controllers
{
    [Authorize("ContentOverrideAdmin")]
    [Route("admin/api/OverrideModularWizard/")]
    public class OverrideModularWizardAdminApiController : WizardApiController
    {
        public OverrideModularWizardAdminApiController(IModularWizardFactoryRepository repository, IModularWizardFactory wizardFactory, IModularContentFieldRepository fieldRepository, IModularContentTextRepository languageRepository, IModularWizardAdminSettings settings) : base (repository, wizardFactory, fieldRepository, languageRepository, settings)
        {
            _groupFilter = "Sample%";
            _errorMessageGroup = "SampleWizard_Errors";

            //HiddenFilterPrefix = "Sample";
            
            PreviewRecordData = new
            {
                SampleName = "John Doe"
            };
        }

        [HttpGet("")]
        public override object Index()
        {
            return base.Index();
        }

        public override object ErrorMessageDtList([FromForm] ModularContentTextDataTablesInput o)
        {
			var auth = TestAuthDenial(); // Check to see if user has permissions. (Method returns null if authorized, or an object if not authorized)
			if (auth != null) JToken.FromObject(new DataTablesOutput(o)); ;

			o.GroupFilter = _groupFilter;
            o.GroupKey = "SampleWizard_Errors";
            o.Language = null;

            return JToken.FromObject(_languageRepository.GetDataTableList(o));
        }

        [NonAction]
        protected override void PopulateDefaultSettings()
        {
			// 
	        _settings.IsAuthorized = User?.HasClaim(x => 
				(x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) && 
				(x.Value?.Equals(nameof(LocalizedContentAccess.Authorized), StringComparison.InvariantCultureIgnoreCase) ?? false)
			) ?? false;

            #region Edit
            _settings.CanReadField = User?.HasClaim(x =>
		        (x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
		        (x.Value?.Equals(nameof(LocalizedContentAccess.ReadFieldRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
	        ) ?? false;

	        _settings.CanEditField = User?.HasClaim(x =>
	            (x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
	            (x.Value?.Equals(nameof(LocalizedContentAccess.SaveFieldRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
            ) ?? false;

	        _settings.CanOrderField = User?.HasClaim(x =>
		        (x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
		        (x.Value?.Equals(nameof(LocalizedContentAccess.ReorderFieldRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
	        ) ?? false;
			#endregion

            #region Insert
            _settings.CanInsertScreen = User?.HasClaim(x =>
	            (x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
	            (x.Value?.Equals(nameof(LocalizedContentAccess.InsertScreenRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
            ) ?? false;
			
            _settings.CanInsertWizard = User?.HasClaim(x =>
	            (x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
	            (x.Value?.Equals(nameof(LocalizedContentAccess.InsertWizardRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
            ) ?? false;

            _settings.CanInsertField = User?.HasClaim(x =>
	            (x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
	            (x.Value?.Equals(nameof(LocalizedContentAccess.InsertFieldRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
            ) ?? false;
            #endregion

			#region Rename
			_settings.CanRenameScreen = User?.HasClaim(x =>
	            (x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
	            (x.Value?.Equals(nameof(LocalizedContentAccess.RenameScreenRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
            ) ?? false;

            _settings.CanRenameWizard = User?.HasClaim(x =>
	            (x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
	            (x.Value?.Equals(nameof(LocalizedContentAccess.RenameWizardRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
            ) ?? false;

            _settings.CanRenameField = User?.HasClaim(x =>
	            (x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
	            (x.Value?.Equals(nameof(LocalizedContentAccess.RenameFieldRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
            ) ?? false;
            #endregion Rename

            #region Delete
            _settings.CanDeleteScreen = User?.HasClaim(x =>
	            (x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
	            (x.Value?.Equals(nameof(LocalizedContentAccess.DeleteScreenRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
            ) ?? false;

            _settings.CanDeleteWizard = User?.HasClaim(x =>
	            (x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
	            (x.Value?.Equals(nameof(LocalizedContentAccess.DeleteWizardRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
            ) ?? false;

            _settings.CanDeleteField = User?.HasClaim(x =>
	            (x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
	            (x.Value?.Equals(nameof(LocalizedContentAccess.DeleteFieldRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
            ) ?? false;
			#endregion

			#region Duplicate
			_settings.CanDuplicateScreen = User?.HasClaim(x =>
				(x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
				(x.Value?.Equals(nameof(LocalizedContentAccess.DuplicateScreenRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
			) ?? false;

			_settings.CanDuplicateWizard = User?.HasClaim(x =>
				(x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
				(x.Value?.Equals(nameof(LocalizedContentAccess.DuplicateWizardRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
			) ?? false;

			_settings.CanDuplicateField = User?.HasClaim(x =>
				(x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
				(x.Value?.Equals(nameof(LocalizedContentAccess.DuplicateFieldRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
			) ?? false;
            #endregion

            #region Label Text
            _settings.CanInsertLabelText = User?.HasClaim(x =>
                (x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
                (x.Value?.Equals(nameof(LocalizedContentAccess.InsertLabelText), StringComparison.InvariantCultureIgnoreCase) ?? false)
            ) ?? false;

            _settings.CanEditLabelText = User?.HasClaim(x =>
                (x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
                (x.Value?.Equals(nameof(LocalizedContentAccess.SaveLabelText), StringComparison.InvariantCultureIgnoreCase) ?? false)
            ) ?? false;

            _settings.CanDeleteLabelText = User?.HasClaim(x =>
                (x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
                (x.Value?.Equals(nameof(LocalizedContentAccess.DeleteLabelText), StringComparison.InvariantCultureIgnoreCase) ?? false)
            ) ?? false;
            #endregion

            #region Error Text
            _settings.CanInsertErrorText = User?.HasClaim(x =>
                (x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
                (x.Value?.Equals(nameof(LocalizedContentAccess.InsertErrorText), StringComparison.InvariantCultureIgnoreCase) ?? false)
            ) ?? false;

            _settings.CanEditErrorText = User?.HasClaim(x =>
                (x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
                (x.Value?.Equals(nameof(LocalizedContentAccess.SaveErrorText), StringComparison.InvariantCultureIgnoreCase) ?? false)
            ) ?? false;

            _settings.CanDeleteErrorText = User?.HasClaim(x =>
                (x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
                (x.Value?.Equals(nameof(LocalizedContentAccess.DeleteErrorText), StringComparison.InvariantCultureIgnoreCase) ?? false)
            ) ?? false;
            #endregion




            _settings.CanPreview = User?.HasClaim(x =>
				(x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
				(x.Value?.Equals(nameof(LocalizedContentAccess.Preview), StringComparison.InvariantCultureIgnoreCase) ?? false)
			) ?? false;

			_settings.CanExport = User?.HasClaim(x =>
				(x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
				(x.Value?.Equals(nameof(LocalizedContentAccess.Export), StringComparison.InvariantCultureIgnoreCase) ?? false)
			) ?? false;

		}
	}
}
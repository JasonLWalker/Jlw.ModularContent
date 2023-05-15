using System;
using System.Collections.Generic;
using Jlw.LocalizedContent;
using Jlw.Web.Rcl.LocalizedContent;
using Jlw.Web.Rcl.LocalizedContent.Areas.ModularWizardAdmin.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json.Linq;

namespace Jlw.Web.LocalizedContent.SampleWebApp.Controllers
{
    [Route("~/Admin/[controller]")]
    [Authorize("ContentOverrideAdmin")]
    public class OverrideModularWizardAdminController : AdminController
    {
        
        public OverrideModularWizardAdminController(IWizardAdminSettings settings, IWizardFactoryRepository repository) : base(settings, repository)
        {
            _groupFilter = "Sample%";

        }

		[NonAction]
		protected override void PopulateDefaultSettings()
		{
			DefaultSettings.Area = "";
			DefaultSettings.ControllerName = "OverrideModularWizardAdmin";
			DefaultSettings.ToolboxHeight = "calc(100vh - 125px)";
			DefaultSettings.IsAuthorized = User?.HasClaim(x => 
				(x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) && 
				(x.Value?.Equals(nameof(LocalizedContentAccess.Authorized), StringComparison.InvariantCultureIgnoreCase) ?? false)
			) ?? false;

			#region Edit
			DefaultSettings.CanReadField = User?.HasClaim(x =>
				(x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
				(x.Value?.Equals(nameof(LocalizedContentAccess.ReadFieldRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
			) ?? false;

			DefaultSettings.CanEditWizard = User?.HasClaim(x =>
				(x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
				(x.Value?.Equals(nameof(LocalizedContentAccess.SaveFieldRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
			) ?? false;

			DefaultSettings.CanEditScreen = User?.HasClaim(x =>
				(x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
				(x.Value?.Equals(nameof(LocalizedContentAccess.SaveFieldRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
			) ?? false;

			DefaultSettings.CanEditField = User?.HasClaim(x =>
				(x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
				(x.Value?.Equals(nameof(LocalizedContentAccess.SaveFieldRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
			) ?? false;

			DefaultSettings.CanOrderField = User?.HasClaim(x =>
				(x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
				(x.Value?.Equals(nameof(LocalizedContentAccess.ReorderFieldRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
			) ?? false;
			#endregion

			#region Insert
			DefaultSettings.CanInsertScreen = User?.HasClaim(x =>
				(x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
				(x.Value?.Equals(nameof(LocalizedContentAccess.InsertScreenRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
			) ?? false;

			DefaultSettings.CanInsertWizard = User?.HasClaim(x =>
				(x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
				(x.Value?.Equals(nameof(LocalizedContentAccess.InsertWizardRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
			) ?? false;

			DefaultSettings.CanInsertField = User?.HasClaim(x =>
				(x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
				(x.Value?.Equals(nameof(LocalizedContentAccess.InsertFieldRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
			) ?? false;
			#endregion

			#region Rename
			DefaultSettings.CanRenameScreen = User?.HasClaim(x =>
				(x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
				(x.Value?.Equals(nameof(LocalizedContentAccess.RenameScreenRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
			) ?? false;

			DefaultSettings.CanRenameWizard = User?.HasClaim(x =>
				(x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
				(x.Value?.Equals(nameof(LocalizedContentAccess.RenameWizardRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
			) ?? false;

			DefaultSettings.CanRenameField = User?.HasClaim(x =>
				(x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
				(x.Value?.Equals(nameof(LocalizedContentAccess.RenameFieldRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
			) ?? false;
			#endregion

			#region Delete
			DefaultSettings.CanDeleteScreen = User?.HasClaim(x =>
				(x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
				(x.Value?.Equals(nameof(LocalizedContentAccess.DeleteScreenRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
			) ?? false;

			DefaultSettings.CanDeleteWizard = User?.HasClaim(x =>
				(x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
				(x.Value?.Equals(nameof(LocalizedContentAccess.DeleteWizardRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
			) ?? false;

			DefaultSettings.CanDeleteField = User?.HasClaim(x =>
				(x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
				(x.Value?.Equals(nameof(LocalizedContentAccess.DeleteFieldRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
			) ?? false;
			#endregion

			#region Duplicate
			DefaultSettings.CanDuplicateScreen = User?.HasClaim(x =>
				(x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
				(x.Value?.Equals(nameof(LocalizedContentAccess.DuplicateScreenRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
			) ?? false;

			DefaultSettings.CanDuplicateWizard = User?.HasClaim(x =>
				(x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
				(x.Value?.Equals(nameof(LocalizedContentAccess.DuplicateWizardRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
			) ?? false;

			DefaultSettings.CanDuplicateField = User?.HasClaim(x =>
				(x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
				(x.Value?.Equals(nameof(LocalizedContentAccess.DuplicateFieldRecords), StringComparison.InvariantCultureIgnoreCase) ?? false)
			) ?? false;
			#endregion

			#region Label Text
			DefaultSettings.CanInsertLabelText = User?.HasClaim(x =>
                (x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
                (x.Value?.Equals(nameof(LocalizedContentAccess.InsertLabelText), StringComparison.InvariantCultureIgnoreCase) ?? false)
            ) ?? false;

            DefaultSettings.CanEditLabelText = User?.HasClaim(x =>
                (x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
                (x.Value?.Equals(nameof(LocalizedContentAccess.SaveLabelText), StringComparison.InvariantCultureIgnoreCase) ?? false)
            ) ?? false;

            DefaultSettings.CanDeleteLabelText = User?.HasClaim(x =>
                (x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
                (x.Value?.Equals(nameof(LocalizedContentAccess.DeleteLabelText), StringComparison.InvariantCultureIgnoreCase) ?? false)
            ) ?? false;
            #endregion

            #region Error Text
            DefaultSettings.CanInsertErrorText = User?.HasClaim(x =>
                (x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
                (x.Value?.Equals(nameof(LocalizedContentAccess.InsertErrorText), StringComparison.InvariantCultureIgnoreCase) ?? false)
            ) ?? false;

            DefaultSettings.CanEditErrorText = User?.HasClaim(x =>
                (x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
                (x.Value?.Equals(nameof(LocalizedContentAccess.SaveErrorText), StringComparison.InvariantCultureIgnoreCase) ?? false)
            ) ?? false;

            DefaultSettings.CanDeleteErrorText = User?.HasClaim(x =>
                (x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
                (x.Value?.Equals(nameof(LocalizedContentAccess.DeleteErrorText), StringComparison.InvariantCultureIgnoreCase) ?? false)
            ) ?? false;
            #endregion


            DefaultSettings.CanPreview = User?.HasClaim(x =>
				(x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
				(x.Value?.Equals(nameof(LocalizedContentAccess.Preview), StringComparison.InvariantCultureIgnoreCase) ?? false)
			) ?? false;

			DefaultSettings.CanExport = User?.HasClaim(x =>
				(x?.Type?.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
				(x.Value?.Equals(nameof(LocalizedContentAccess.Export), StringComparison.InvariantCultureIgnoreCase) ?? false)
			) ?? false;



			ViewData["Breadcrumb"] = new List<KeyValuePair<string, string>>()
			{
				new KeyValuePair<string, string>(Url.Action("Index", "Home", new {Area=""}), "Home"),
				new KeyValuePair<string, string>(Url.Action("Index", "Home", new {Area=""}), "Administration"),
				new KeyValuePair<string, string>(Url.Action("Index", "OverrideModularWizardAdmin", new {Area=""}), "Wizard Admin Override"),
			};
		}
	}
}

using System.Collections.Generic;
using Jlw.Utilities.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json.Linq;

namespace Jlw.ModularContent;

public class WizardAdminSettings : WizardSettings, IWizardAdminSettings
{
	#region Properties
	
	public bool IsAuthorized { get; set; }



	public bool CanRead { get; set; }
	public bool CanEdit { get; set; }
	public bool CanDelete { get; set; }
	public bool CanInsert { get; set; }

	public bool CanOrderField { get; set; }


	public bool CanReadField { get; set; }



	#region Label Properties
	public bool CanDeleteLabel { get; set; }
	public bool CanInsertLabel { get; set; }
	#endregion



	public bool CanEditField { get; set; }
	public bool CanEditWizard { get; set; }
	public bool CanEditScreen { get; set; }
	public bool CanEditLabelText { get; set; }
	public bool CanEditError { get; set; }
	public bool CanEditErrorText { get; set; }



	public bool CanDeleteField { get; set; }
	public bool CanDeleteWizard { get; set; }
	public bool CanDeleteScreen { get; set; }
	public bool CanDeleteLabelText { get; set; }
	public bool CanDeleteError { get; set; }
	public bool CanDeleteErrorText { get; set; }
	

	
	public bool CanInsertField { get; set; }
	public bool CanInsertWizard { get; set; }
	public bool CanInsertScreen { get; set; }
	public bool CanInsertLabelText { get; set; }
	public bool CanInsertError { get; set; }
	public bool CanInsertErrorText { get; set; }



	public bool CanRenameField { get; set; }			//
	public bool CanRenameWizard { get; set; }			//
	public bool CanRenameScreen { get; set; }           //



	public bool CanDuplicateField { get; set; }
	public bool CanDuplicateScreen { get; set; }
	public bool CanDuplicateWizard { get; set; }


	public bool CanPreview { get; set; }
	public bool CanExport { get; set; }


	public bool IsAdmin { get; set; }



	public bool UseWysiwyg { get; set; } = true;
	public bool ShowWireFrame { get; set; } = true;
	public bool SideNavDefault { get; set; } = true;
	public bool WireFrameDefault { get; set; } = false;



	public JToken TinyMceSettings { get; set; }

	protected string _adminUrl = null;
	public virtual string AdminUrl
	{
		get => string.IsNullOrWhiteSpace(_adminUrl) ? LinkGenerator?.GetPathByAction("Index", ControllerName ?? "", new { Area }) ?? "" : _adminUrl; 
		set => _adminUrl = value;
	}

	protected string _previewUrl = null;
	public virtual string PreviewUrl
	{
		get => string.IsNullOrWhiteSpace(_previewUrl) ? LinkGenerator?.GetPathByAction("Preview", ControllerName ?? "", new { Area }) ?? "" : _previewUrl;
		set => _previewUrl = value;
	}

	protected string _previewScreenUrl = null;
	public virtual string PreviewScreenUrl
	{
		get => string.IsNullOrWhiteSpace(_previewScreenUrl) ? LinkGenerator?.GetPathByAction("PreviewScreen", ControllerName ?? "", new { Area }) ?? "" : _previewScreenUrl;
		set => _previewScreenUrl = value;
	}

	protected string _exportUrl = null;
	public virtual string ExportUrl
	{
		get => string.IsNullOrWhiteSpace(_exportUrl) ? LinkGenerator?.GetPathByAction("Export", ControllerName ?? "", new { Area }) ?? "" : _exportUrl;
		set => _exportUrl = value;
	}

	public string ToolboxHeight { get; set; }

	public string HiddenFilterPrefix { get; set; }
	public object PreviewRecordData { get; set; }

	public List<SelectListItem> LanguageList { get; } = new List<SelectListItem>() { new SelectListItem("English", "EN") };
	#endregion

	public WizardAdminSettings() : this(null) { }

	public WizardAdminSettings(object o) : base(o)
	{
		IsAuthorized = DataUtility.ParseNullableBool(o, nameof(IsAuthorized)) ?? false;



		IsAdmin = DataUtility.ParseNullableBool(o, nameof(IsAdmin)) ?? false;
		CanEdit = DataUtility.ParseNullableBool(o, nameof(CanEdit)) ?? false;
		CanDelete = DataUtility.ParseBool(o, nameof(CanDelete));
		CanInsert = DataUtility.ParseBool(o, nameof(CanInsert));


		CanRead = DataUtility.ParseNullableBool(o, nameof(CanRead)) ?? false;
		CanReadField = DataUtility.ParseNullableBool(o, nameof(CanReadField)) ?? false;

		CanOrderField = DataUtility.ParseNullableBool(o, nameof(CanOrderField)) ?? false;


		CanInsertScreen = DataUtility.ParseNullableBool(o, nameof(CanInsertScreen)) ?? false;
		CanInsertWizard = DataUtility.ParseNullableBool(o, nameof(CanInsertWizard)) ?? false;
		CanInsertField = DataUtility.ParseNullableBool(o, nameof(CanInsertField)) ?? false;

		// Rename
		CanRenameField = DataUtility.ParseNullableBool(o, nameof(CanRenameField)) ?? false;
		CanRenameScreen = DataUtility.ParseNullableBool(o, nameof(CanRenameScreen)) ?? false;
		CanRenameWizard = DataUtility.ParseNullableBool(o, nameof(CanRenameWizard)) ?? false;

		// Delete
		CanDeleteField = DataUtility.ParseNullableBool(o, nameof(CanDeleteField)) ?? false;
		CanDeleteScreen = DataUtility.ParseNullableBool(o, nameof(CanDeleteScreen)) ?? false;
		CanDeleteWizard = DataUtility.ParseNullableBool(o, nameof(CanDeleteWizard)) ?? false;

		// Duplicate
		CanDuplicateField = DataUtility.ParseNullableBool(o, nameof(CanDuplicateField)) ?? false;
		CanDuplicateScreen = DataUtility.ParseNullableBool(o, nameof(CanDuplicateScreen)) ?? false;
		CanDuplicateWizard = DataUtility.ParseNullableBool(o, nameof(CanDuplicateWizard)) ?? false;


		CanPreview = DataUtility.ParseNullableBool(o, nameof(CanPreview)) ?? false;
		CanExport = DataUtility.ParseNullableBool(o, nameof(CanExport)) ?? false;


		UseWysiwyg = DataUtility.ParseBool(o, "UseWysiwyg");

		

		ShowWireFrame = DataUtility.ParseBool(o, "ShowWireFrame");
		SideNavDefault = DataUtility.ParseBool(o, "SideNavDefault");
		WireFrameDefault = DataUtility.ParseBool(o, "WireFrameDefault");
		TinyMceSettings = DataUtility.ParseBool(o, "TinyMceSettings");



		_adminUrl = DataUtility.ParseString(o, nameof(AdminUrl));
		_previewUrl = DataUtility.ParseString(o, nameof(PreviewUrl));
		_previewScreenUrl = DataUtility.ParseString(o, nameof(PreviewScreenUrl));
		_exportUrl = DataUtility.ParseString(o, nameof(ExportUrl));



		ToolboxHeight = DataUtility.ParseString(o, "ToolboxHeight");
		HiddenFilterPrefix = DataUtility.ParseString(o, "HiddenFilterPrefix");
		PreviewRecordData = DataUtility.Parse<object>(o, "PreviewRecordData");
	}
}
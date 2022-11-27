using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json.Linq;

namespace Jlw.Web.Rcl.LocalizedContent;

public interface IWizardAdminSettings : IWizardSettings
{ 
	bool IsAuthorized { get; set; }				// Is user authorized to access this module
	bool IsAdmin { get; set; }					// Is the user an admin (possibly deprecated)


	bool CanRead { get; set; }					//
	bool CanEdit { get; set; }					// General purpose "Can Edit" flag (possibly deprecated)
	bool CanDelete { get; set; }				// General purpose "Can Delete" flag
	bool CanInsert { get; set; }
	

	bool CanReadField { get; set; }				//
	bool CanEditField { get; set; }				//
	bool CanEditWizard { get; set; }			//
	bool CanEditScreen { get; set; }			//
	bool CanEditLabelText { get; set; }			// Can the user edit label?
	
    bool CanEditError { get; set; }				//
	bool CanEditErrorText { get; set; }			// Can the user edit Error Text
	

	bool CanDeleteField { get; set; }			// Can the user delete fields/nodes
	bool CanDeleteWizard { get; set; }			// Can the user delete wizards
	bool CanDeleteScreen { get; set; }			// Can the user delete screens


	bool CanDeleteLabelText { get; set; }       // Can the user delete label text values?
	bool CanDeleteError { get; set; }			//
	bool CanDeleteErrorText { get; set; }       //

	bool CanOrderField { get; set; }           //

	bool CanInsertField { get; set; }			//
	bool CanInsertWizard { get; set; }			//
	bool CanInsertScreen { get; set; }			//
	bool CanInsertLabelText { get; set; }		//
	bool CanInsertError { get; set; }			//
	bool CanInsertErrorText { get; set; }		//


	bool CanRenameField { get; set; }           //
	bool CanRenameWizard { get; set; }           //
	bool CanRenameScreen { get; set; }           //


	bool CanDuplicateField { get; set; }
	bool CanDuplicateScreen { get; set; }
	bool CanDuplicateWizard { get; set; }


	bool CanPreview { get; set; }
	bool CanExport { get; set; }


	bool UseWysiwyg { get; set; }
	bool ShowWireFrame { get; set; }
	bool SideNavDefault { get; set; }
	bool WireFrameDefault { get; set; }
	JToken TinyMceSettings { get; set; }


	string AdminUrl { get; set; }
	string PreviewUrl { get; set; }
	string PreviewScreenUrl { get; set; }
	string ExportUrl { get; set; }


	string ToolboxHeight { get; set; }
	string HiddenFilterPrefix { get; set; }
	public object PreviewRecordData { get; set; }
	List<SelectListItem> LanguageList { get; }

}
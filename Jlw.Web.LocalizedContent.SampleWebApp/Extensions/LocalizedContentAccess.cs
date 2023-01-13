namespace Jlw.Web.LocalizedContent.SampleWebApp;

public enum LocalizedContentAccess
{
	Unknown = 0,

	Authorized,                 // Authorized to access module

	ReorderFieldRecords,

	DeleteRecords,
	DeleteFieldRecords,
	DeleteScreenRecords,
	DeleteWizardRecords,

	DeleteLabelText,
	DeleteErrorText,

	DuplicateFieldRecords,
	DuplicateScreenRecords,
	DuplicateWizardRecords,

	InsertRecords,
	InsertFieldRecords,
	InsertScreenRecords,
	InsertWizardRecords,
	
	InsertLabelText,
	InsertErrorText,

	ReadRecords,
	ReadFieldRecords,

	SaveRecords,
	SaveFieldRecords,
	SaveLabelText,
	SaveErrorText,

	RenameFieldRecords,
	RenameScreenRecords,
	RenameWizardRecords,

	Configure,
	
	Preview,
	Export,

	Admin,

	WizardConfigurationAdmin,
	WizardTextConfigurationAdmin,
	EmailConfigurationAdmin,
	EmailTextConfigurationAdmin,
}


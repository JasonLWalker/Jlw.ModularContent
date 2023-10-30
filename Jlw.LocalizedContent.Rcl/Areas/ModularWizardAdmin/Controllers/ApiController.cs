using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Jlw.Utilities.Data;
using Jlw.Utilities.WebApiUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Jlw.ModularContent.Areas.ModularWizardAdmin.Controllers;

[ApiController]
[Authorize]
[Produces("application/json")]
[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[JsonConverter(typeof(DefaultContractResolver))]
[JsonObject(NamingStrategyType = typeof(DefaultNamingStrategy))]
public abstract class ApiController : WizardApiBaseController
{
    #region Internal Properties
    protected string _groupFilter = "";
    protected string _errorMessageGroup = "";
    protected IWizardAdminSettings _settings;
    protected int nMaxTreeDepth = 10;
    protected readonly Regex _reFieldName = new Regex("[^a-zA-Z0-9\\-]");
    protected readonly ILocalizedContentTextRepository _languageRepository;
    
    protected readonly List<WizardField> DefaultWizardControls = new List<WizardField>();
    protected readonly string HiddenFilterPrefix = "";

    private object _previewRecordData;
    protected object PreviewRecordData
    {
        get => _previewRecordData ?? new {}; 
        set => _previewRecordData = value;
    }

    private ILocalizedContentFieldRepository _fieldRepository { get; set; }
	#endregion


    protected ApiController(IWizardFactoryRepository repository, IWizardFactory wizardFactory, ILocalizedContentFieldRepository fieldRepository, ILocalizedContentTextRepository languageRepository, IWizardAdminSettings settings) : base(repository, wizardFactory)
    {
        _fieldRepository = fieldRepository;
        _languageRepository = languageRepository;
        _settings = settings;
        HiddenFilterPrefix = settings.HiddenFilterPrefix ?? "";
        PreviewRecordData = settings.PreviewRecordData ?? new {};
        InitializeControls(_settings);
    }

    [HttpGet("")]
    public virtual object Index()
    {
        return new { };
    }


    [HttpPost("")]
    public virtual object GetWizard(WizardInputModel model)
    {
	    var auth = TestAuthDenial(); // Check to see if user has permissions. (Method returns null if authorized, or an object if not authorized)
	    if (auth != null) return auth;

		if (String.IsNullOrWhiteSpace(model.Screen))
        {
            var fields = DataRepository.GetWizardFields(model.Wizard, model.Wizard, _groupFilter);
            var screen = fields.OrderBy(o => o.Order).FirstOrDefault(o => o.FieldType.Equals("SCREEN", StringComparison.InvariantCultureIgnoreCase));

            model.Screen = screen?.FieldKey;
        }
        return WizardFactory.CreateWizardScreenContent(model.Wizard, model.Screen, model.IsLivePreview ? PreviewRecordData : new { });
    }


    #region Field/Node Actions

    /// TODO Edit XML Comment Template for Data
    [HttpPost("GetField")]
    public virtual object GetFieldData(WizardField o)
    {
	    var auth = TestAuthDenial(); // Check to see if user has permissions. (Method returns null if authorized, or an object if not authorized)
	    if (auth != null) return auth;
	    if ((auth = TestAuthDenial(_settings.CanReadField, _settings)) != null) return auth;


		ILocalizedContentField oResult;
        o.GroupFilter = _groupFilter;

        try
		{
            oResult = DataRepository.GetRecord(o);
        }
        catch (Exception ex)
        {
            return JToken.FromObject(new ApiExceptionMessage("An error has occurred", ex));
        }

        if (oResult == null || oResult.Id < 1)
            return JToken.FromObject(new ApiStatusMessage("Unable to locate a matching record.", "Record not found", ApiMessageType.Danger));

        return new WizardField(oResult);
    }


    /// TODO Edit XML Comment Template for Data
    [HttpPost("SaveNode")]
    public virtual object SaveNode(WizardFieldUpdateData o)
    {
	    var auth = TestAuthDenial(); // Check to see if user has permissions. (Method returns null if authorized, or an object if not authorized)
	    if (auth != null) return auth;
	    if ((auth = TestAuthDenial(_settings.CanReadField, _settings)) != null) return auth;
        

		if (o.FieldName.Equals("DefaultLabel", StringComparison.InvariantCultureIgnoreCase))
	    {
		    if ((auth = TestAuthDenial(_settings.CanEditLabelText, _settings)) != null) return auth;
		    //if (!_settings.CanEditLabelText) return GetStatusObject("Permission Denied", "You do not have permissions to perform that action", ApiMessageType.Alert);
	    }
		else
	    {
			if ((auth = TestAuthDenial(_settings.CanEditField, _settings)) != null) return auth;
		    //if (!_settings.CanEditField) return GetStatusObject("Permission Denied", "You do not have permissions to perform that action", ApiMessageType.Alert);
		}

		var bResult = false;
	    o.GroupFilter = _groupFilter;

	    try
	    {
		    o.AuditChangeBy = User.Identity?.Name ?? "";
		    if (o.FieldName.Equals("FieldKey", StringComparison.InvariantCultureIgnoreCase))
		    {
			    var field = _fieldRepository.GetRecord(new LocalizedContentField(o));
			    //return DataRepository.RenameWizardFieldRecursive(new WizardContentField(new { Id = o.Id }), o.FieldValue);
			    return RenameField(new WizardField(field) { NewFieldKey = o.FieldValue });
		    }
		    else { }
		    var oResult = DataRepository.SaveFieldData(o);
		    if (oResult != null)
			    return JToken.FromObject(new ApiObjectMessage(oResult, "Record has been saved successfully.", "Record Saved", ApiMessageType.Success));
	    }
	    catch (Exception ex)
	    {
		    return JToken.FromObject(new ApiExceptionMessage("An error has occurred", ex));
	    }

	    // Else 
	    return JToken.FromObject(new ApiStatusMessage("Unable to save record. Please check the data and try again.", "Error while saving", ApiMessageType.Danger));
    }


	/// <summary>
	/// Saves the specified o.
	/// </summary>
	/// <param name="o">The o.</param>
	/// <returns>System.Object.</returns>
	/// TODO Edit XML Comment Template for Save
	[HttpPost("SaveField")]
	public virtual object SaveField(WizardField o)
	{
		var auth = TestAuthDenial(); // Check to see if user has permissions. (Method returns null if authorized, or an object if not authorized)
		if (auth != null) return auth;
		ILocalizedContentField oResult = _fieldRepository.GetRecordByName(o);

		if (oResult == null)
		{
			switch (o.FieldType?.ToUpper())
			{
				case "WIZARD":
					if ((auth = TestAuthDenial(_settings.CanInsertWizard, _settings)) != null) return auth;
					break;
				case "SCREEN":
					if ((auth = TestAuthDenial(_settings.CanInsertScreen, _settings)) != null) return auth;
					break;
				default:
					if ((auth = TestAuthDenial(_settings.CanInsertField, _settings)) != null) return auth;
					break;
			}
		}
		else
		{
			switch (o.FieldType?.ToUpper())
			{
				case "WIZARD":
					if ((auth = TestAuthDenial(_settings.CanEditWizard, _settings)) != null) return auth;
					break;
				case "SCREEN":
					if ((auth = TestAuthDenial(_settings.CanEditScreen, _settings)) != null) return auth;
					break;
				default:
					if ((auth = TestAuthDenial(_settings.CanEditField, _settings)) != null) return auth;
					break;
			}
		}

		var bResult = false;
		o.AuditChangeBy = User.Identity?.Name ?? "";
		o.FieldKey = _reFieldName.Replace(o.FieldKey, "_");
		//o.GroupFilter = _groupFilter;
		IWizardContentField field = o;
		if (field.Id < 1)
			field = InitNewWizardField(field);


		try
		{
			oResult = _fieldRepository.SaveRecord(new LocalizedContentField(field));
			bResult = oResult != null;
		}
		catch (Exception ex)
		{
			return JToken.FromObject(new ApiExceptionMessage("An error has occurred", ex));
		}

		if (bResult == true)
			return JToken.FromObject(new ApiObjectMessage(oResult, "Record has been saved successfully.", "Record Saved", ApiMessageType.Success));

		// Else 
		return JToken.FromObject(new ApiStatusMessage("Unable to save record. Please check the data and try again.", "Error while saving", ApiMessageType.Danger));
	}


	/// <summary>
	/// Deletes the specified o.
	/// </summary>
	/// <param name="o">The o.</param>
	/// <returns>System.Object.</returns>
	/// TODO Edit XML Comment Template for Delete
	[HttpPost("DeleteField")]
	public virtual object DeleteField(WizardField o)
	{
		var auth = TestAuthDenial(); // Check to see if user has permissions. (Method returns null if authorized, or an object if not authorized)
		if (auth != null) return auth;


		ILocalizedContentField oResult = _fieldRepository.GetRecord(o);

		switch (o.FieldType?.ToUpper())
		{
			case "HEADING":
				return JToken.FromObject(new ApiStatusMessage("Page Heading fields cannot be deleted.", "Unable to delete", ApiMessageType.Alert));
			case "BODY":
				return JToken.FromObject(new ApiStatusMessage("Page Body fields cannot be deleted.", "Unable to delete", ApiMessageType.Alert));
			case "WIZARD":
				if ((auth = TestAuthDenial(_settings.CanDeleteWizard, _settings)) != null) return auth;
				break;
			case "SCREEN":
				if ((auth = TestAuthDenial(_settings.CanDeleteScreen, _settings)) != null) return auth;
				break;
			default:
				switch (oResult.FieldKey?.ToUpper())
				{
					case "HEADING":
						return JToken.FromObject(new ApiStatusMessage("Page Heading fields cannot be renamed.", "Unable to rename", ApiMessageType.Alert));
					case "BODY":
						return JToken.FromObject(new ApiStatusMessage("Page Body fields cannot be renamed.", "Unable to rename", ApiMessageType.Alert));
					default:
						if ((auth = TestAuthDenial(_settings.CanDeleteField, _settings)) != null) return auth;
						break;
				}
				break;
		}


		var bResult = false;
		o.GroupFilter = _groupFilter;

		try
		{
			o.AuditChangeBy = User.Identity?.Name ?? "";
			oResult = this.DataRepository.DeleteWizardFieldRecursive(o);//.DeleteRecord(o);
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

	/// <summary>
	/// Deletes the specified o.
	/// </summary>
	/// <param name="o">The o.</param>
	/// <returns>System.Object.</returns>
	/// TODO Edit XML Comment Template for Delete
	[HttpPost("RenameField")]
	public virtual object RenameField(WizardField o)
	{
		var auth = TestAuthDenial(); // Check to see if user has permissions. (Method returns null if authorized, or an object if not authorized)
		if (auth != null) return auth;

		switch (o.FieldType?.ToUpper())
		{
			case "HEADING":
				return JToken.FromObject(new ApiStatusMessage("Page Heading fields cannot be renamed.", "Unable to rename", ApiMessageType.Alert));
			case "BODY":
				return JToken.FromObject(new ApiStatusMessage("Page Body fields cannot be renamed.", "Unable to rename", ApiMessageType.Alert));
			case "WIZARD":
				if ((auth = TestAuthDenial(_settings.CanRenameWizard, _settings)) != null) return auth;
				break;
			case "SCREEN":
				if ((auth = TestAuthDenial(_settings.CanRenameScreen, _settings)) != null) return auth;
				break;
			default:
				switch (o.FieldKey?.ToUpper())
				{
                    case "HEADING":
	                    return JToken.FromObject(new ApiStatusMessage("Page Heading fields cannot be renamed.", "Unable to rename", ApiMessageType.Alert));
                    case "BODY":
	                    return JToken.FromObject(new ApiStatusMessage("Page Body fields cannot be renamed.", "Unable to rename", ApiMessageType.Alert));
                    default:
	                    if ((auth = TestAuthDenial(_settings.CanRenameField, _settings)) != null) return auth;
	                    break;
				}
				break;
		}

		var bResult = false;
		o.GroupFilter = _groupFilter;

		ILocalizedContentField oResult = _fieldRepository.GetRecordByName(o);
		switch ((oResult?.FieldType ?? "").ToUpper())
		{
            case nameof(WizardFieldTypes.SCREEN):
	            if ((auth = TestAuthDenial(_settings.CanRenameScreen, _settings)) != null) return auth;
	            break;
            case nameof(WizardFieldTypes.WIZARD):
	            if ((auth = TestAuthDenial(_settings.CanRenameWizard, _settings)) != null) return auth;
	            break;
            default:
	            if ((auth = TestAuthDenial(_settings.CanRenameField, _settings)) != null) return auth;
	            break;
		}

		string newName = _reFieldName.Replace(o.NewFieldKey, "_");
		string origFieldKey = o.FieldKey;
		o.FieldKey = newName;

		oResult = _fieldRepository.GetRecordByName(o);


		if (oResult?.Id > 0)
		{
			return JToken.FromObject(new ApiStatusMessage("A Record with that name already exists, please choose a new name and try again.", "Screen already exists", ApiMessageType.Alert));
		}
		o.FieldKey = origFieldKey;

		try
		{
			o.AuditChangeBy = User.Identity?.Name ?? "";
			oResult = DataRepository.RenameWizardFieldRecursive(o, newName);
			bResult = oResult.FieldKey == newName;
		}
		catch (Exception ex)
		{
			return JToken.FromObject(new ApiExceptionMessage("An error has occurred", ex));
		}

		if (bResult)
			return JToken.FromObject(new ApiStatusMessage("Record has been successfully renamed.", "Record renamed", ApiMessageType.Success));

		// Else 
		return JToken.FromObject(new ApiStatusMessage("Unable to rename record. Please check the data and try again.", "Error while renaming", ApiMessageType.Danger));
	}

	/// <summary>
	/// Deletes the specified o.
	/// </summary>
	/// <param name="o">The o.</param>
	/// <returns>System.Object.</returns>
	/// TODO Edit XML Comment Template for Delete
	[HttpPost("DuplicateField")]
	public virtual object DuplicateField(WizardField o)
	{
		var auth = TestAuthDenial(); // Check to see if user has permissions. (Method returns null if authorized, or an object if not authorized)
		if (auth != null) return auth;

        switch (o.FieldType?.ToUpper())
        {
	        case "HEADING":
		        return JToken.FromObject(new ApiStatusMessage("Page Heading fields cannot be duplicated.", "Unable to duplicate", ApiMessageType.Alert));
	        case "BODY":
		        return JToken.FromObject(new ApiStatusMessage("Page Body fields cannot be duplicated.", "Unable to duplicate", ApiMessageType.Alert));
            case "WIZARD":
	            if ((auth = TestAuthDenial(_settings.CanDuplicateWizard, _settings)) != null) return auth;
	            break;
            case "SCREEN":
	            if ((auth = TestAuthDenial(_settings.CanDuplicateScreen, _settings)) != null) return auth;
	            break;
			default:
	            if ((auth = TestAuthDenial(_settings.CanDuplicateField, _settings)) != null) return auth;
	            break;
        }


		var bResult = false;
		o.GroupFilter = _groupFilter;

		string newName = _reFieldName.Replace(o.NewFieldKey, "_");
		string origFieldKey = o.FieldKey;
		o.FieldKey = newName;

		ILocalizedContentField oResult = _fieldRepository.GetRecordByName(o);
		if (oResult?.Id > 0)
		{
			return JToken.FromObject(new ApiStatusMessage("A Record with that name already exists, please choose a new name and try again.", "Screen already exists", ApiMessageType.Alert));
		}
		o.FieldKey = origFieldKey;

		try
		{
			o.AuditChangeBy = User.Identity?.Name ?? "";
			oResult = DataRepository.DuplicateWizardFieldRecursive(o, newName);
			bResult = oResult.FieldKey == newName;
		}
		catch (Exception ex)
		{
			return JToken.FromObject(new ApiExceptionMessage("An error has occurred", ex));
		}

		if (bResult)
			return JToken.FromObject(new ApiStatusMessage("Record has been successfully duplicated.", "Record duplicated", ApiMessageType.Success));

		// Else 
		return JToken.FromObject(new ApiStatusMessage("Unable to duplicate record. Please check the data and try again.", "Error while duplicating", ApiMessageType.Danger));
	}


	[Route("SaveOrder")]
	[HttpPost]
	public virtual object SaveFieldOrder(IEnumerable<WizardInputModel> nodeList)
	{
		var auth = TestAuthDenial(); // Check to see if user has permissions. (Method returns null if authorized, or an object if not authorized)
		if (auth != null) return auth;
		if ((auth = TestAuthDenial(_settings.CanOrderField, _settings)) != null) return auth;


		bool bSuccess = true;

		foreach (var node in nodeList)
		{
			if (node != null)
			{
				node.AuditChangeBy = User.Identity?.Name ?? "";
				var result = DataRepository.SaveFieldParentOrder(node);
				if (result?.ParentKey != node.ParentKey || result?.Order != node.Order)
					bSuccess = false;
			}
		}
		if (bSuccess)
			return new ApiStatusMessage("Field order successfully updated", "", ApiMessageType.Success);

		return new ApiStatusMessage("An error occurred while updating field order. Please try again.", "", ApiMessageType.Danger);
	}

    #endregion


    #region Wizard Actions

    /// <summary>Saves the submitted data from the wizard.</summary>
    /// <param name="model">The model.</param>
    /// <returns>System.Object.</returns>
    [Route("Save")]
    [HttpPost]
    public virtual object SaveWizard(WizardInputModel model)
    {
	    var auth = TestAuthDenial(); // Check to see if user has permissions. (Method returns null if authorized, or an object if not authorized)
	    if (auth != null) return auth;
	    if (!_settings.CanEditWizard) return GetStatusObject("Permission Denied", "You do not have permissions to perform that action", ApiMessageType.Alert);

		return ProcessWizard(model, true);
    }
	

    [HttpPost("NewWizard")]
    public virtual object NewWizard(LocalizedContentFieldRecordInput o)
    {
	    var auth = TestAuthDenial(); // Check to see if user has permissions. (Method returns null if authorized, or an object if not authorized)
	    if (auth != null) return auth;
	    if ((auth = TestAuthDenial(_settings.CanInsertWizard, _settings)) != null) return auth;

		var bResult = false;
        if (!string.IsNullOrWhiteSpace(_settings.HiddenFilterPrefix))
        {
            if (!o.GroupKey.StartsWith(_settings.HiddenFilterPrefix, StringComparison.InvariantCultureIgnoreCase))
            {
                o.GroupKey = _settings.HiddenFilterPrefix + o.GroupKey;
            }
        }

        o.GroupFilter = _groupFilter;
        o.GroupKey = _reFieldName.Replace(o.GroupKey, "_");
        o.FieldKey = _reFieldName.Replace(o.GroupKey, "_"); // Wizards have the same value for GroupKey and FieldKey
        o.FieldType = "WIZARD";
        o.FieldData ??= "{}";
        o.FieldClass ??= "";
        o.DefaultLabel ??= "";
        o.WrapperClass ??= "";
        o.WrapperHtmlEnd ??= "";
        o.WrapperHtmlStart ??= "";
        o.ParentKey ??= "";

        ILocalizedContentField oResult = _fieldRepository.GetRecordByName(o);
        if (oResult?.Id > 0)
        {
            return JToken.FromObject(new ApiStatusMessage("A Record with that name already exists, please choose a new name and try again.", "Wizard already exists", ApiMessageType.Alert));
        }

        try
        {
            o.AuditChangeBy = User.Identity?.Name ?? "";
            oResult = _fieldRepository.SaveRecord(o);
            bResult = oResult != null;
        }
        catch (Exception ex)
        {
            return JToken.FromObject(new ApiExceptionMessage("An error has occurred", ex));
        }

        if (oResult != null && oResult.Id < 1)
        {
            bResult = false;
            if (oResult.AuditChangeType?.Equals("ERROR", StringComparison.InvariantCultureIgnoreCase) ?? false)
                return JToken.FromObject(new ApiStatusMessage(oResult.FieldData, "Error while saving", ApiMessageType.Danger));
        }

        if (bResult)
            return JToken.FromObject(new ApiObjectMessage(oResult, "Record has been saved successfully.", "Record Saved", ApiMessageType.Success));

        // Else 
        return JToken.FromObject(new ApiStatusMessage("Unable to save record. Please check the data and try again.", "Error while saving", ApiMessageType.Danger));
    }

    
    [HttpGet("List")]
    public virtual IEnumerable<object> GetWizardList() 
    {
	    var auth = TestAuthDenial(); // Check to see if user has permissions. (Method returns null if authorized, or an object if not authorized)
	    if (auth != null) return auth;
	    if ((auth = TestAuthDenial(_settings.CanReadField, _settings)) != null) return auth;


		var data = DataRepository.GetWizardFields(null, _groupFilter);

        List<Object> wizardList = new List<object>();

        var aWizards = data.Where(o => o.FieldType.Equals("WIZARD", StringComparison.InvariantCultureIgnoreCase));
        foreach (var currentNode in aWizards)
        {
            wizardList.Add(currentNode);
        }

        return wizardList;
    }


    [HttpGet("Tree/{groupKey?}")]
    public virtual object GetWizardTree(string groupKey = "")
    {
	    var auth = TestAuthDenial(); // Check to see if user has permissions. (Method returns null if authorized, or an object if not authorized)
	    if (auth != null) return auth;
	    if ((auth = TestAuthDenial(_settings.CanReadField, _settings)) != null) return auth;



		var data = DataRepository.GetWizardFields(groupKey, _groupFilter).Select(o => new WizardField(o));

	    var wizardList = new List<WizardTreeNode>();

	    var aWizards = data.Where(o => o.FieldType.Equals("WIZARD", StringComparison.InvariantCultureIgnoreCase));
	    foreach (var wizard in aWizards)
	    {
		    //wizard.Label.Replace()
		    if (!string.IsNullOrWhiteSpace(groupKey))
		    {
			    var treeNode = GetWizardTreeNode(wizard, data);
			    if (!string.IsNullOrWhiteSpace(HiddenFilterPrefix))
			    {
				    var reLabel = new Regex("^" + HiddenFilterPrefix, RegexOptions.IgnoreCase);
				    treeNode.title = reLabel.Replace(treeNode.title, "");
			    }
			    wizardList.Add(treeNode);
		    }
	    }
	    return wizardList;
    }


	#endregion


	#region Screen Actions

	[HttpPost("NewScreen")]
    public virtual object NewScreen(LocalizedContentFieldRecordInput o) 
    {
	    var auth = TestAuthDenial(); // Check to see if user has permissions. (Method returns null if authorized, or an object if not authorized)
	    if (auth != null) return auth;
	    if ((auth = TestAuthDenial(_settings.CanInsertScreen, _settings)) != null) return auth;

		var bResult = false;
        o.GroupFilter = _groupFilter;
        o.FieldKey = _reFieldName.Replace(o.FieldKey, "_");
        o.FieldType = "SCREEN";
        o.FieldData ??= "{}";
        o.FieldClass ??= "row g-3 mb-3";
        o.DefaultLabel ??= "";
        o.WrapperClass ??= "";
        o.WrapperHtmlEnd ??= "";
        o.WrapperHtmlStart ??= "";

        ILocalizedContentField oResult = _fieldRepository.GetRecordByName(o);
        if (oResult?.Id > 0)
        {
            return JToken.FromObject(new ApiStatusMessage("A Record with that name already exists, please choose a new name and try again.", "Screen already exists", ApiMessageType.Alert));
        }
        
        try
        {
            o.AuditChangeBy = User.Identity?.Name ?? "";
            oResult = _fieldRepository.SaveRecord(o);
            bResult = oResult != null;
            if (bResult)
            {
                // Add the 2 special fields (Body and Heading)
                o.FieldType = "HEADING";
                o.FieldKey = "Heading";
                o.ParentKey = oResult.FieldKey;
                o.WrapperClass = "col-12 h3";
                o.Order = 1;
                _fieldRepository.SaveRecord(o);
                o.FieldType = "HTML";
                o.FieldKey = "Body";
                o.WrapperClass = "col-12";
                o.ParentKey = oResult.FieldKey;
                o.Order = 2;
                _fieldRepository.SaveRecord(o);
            }
            //_LocalizedContentFieldList.Refresh(); 
        }
        catch (Exception ex)
        {
            return JToken.FromObject(new ApiExceptionMessage("An error has occurred", ex));
        }

        if (bResult == true)
            return JToken.FromObject(new ApiObjectMessage(oResult, "Record has been saved successfully.", "Record Saved", ApiMessageType.Success));

        // Else 
        return JToken.FromObject(new ApiStatusMessage("Unable to save record. Please check the data and try again.", "Error while saving", ApiMessageType.Danger));
    }

    #endregion



    [HttpGet("Components/{groupKey?}")]
    public virtual object GetComponentList(string groupKey="")
    {
	    var auth = TestAuthDenial(); // Check to see if user has permissions. (Method returns null if authorized, or an object if not authorized)
	    if (auth != null) return auth;



		if (groupKey.Equals("jlwNativeHtmlControls", StringComparison.InvariantCultureIgnoreCase))
            return GetDefaultHtmlControls();

        return DataRepository.GetComponentList(groupKey).Select(o=>new WizardField(o));  //GetWizardFields(groupKey);
    }



    [HttpPost("Localization/DtList")]
    public virtual object LocalizationDtList([FromForm] LocalizedContentTextDataTablesInput o)
    {
	    var auth = TestAuthDenial(); // Check to see if user has permissions. (Method returns null if authorized, or an object if not authorized)
	    if (auth != null) return auth;
        if ((auth = TestAuthDenial(_settings.CanReadField, _settings)) != null) return auth;


        o.GroupFilter = _groupFilter;
        o.Language = null;

        return JToken.FromObject(_languageRepository.GetDataTableList(o));
    }

    [HttpPost("Localization/Data")]
    public virtual object LocalizationData(LocalizedContentTextRecordInput o)
    {
		var auth = TestAuthDenial(); // Check to see if user has permissions. (Method returns null if authorized, or an object if not authorized)
		if (auth != null) return auth;
        if ((auth = TestAuthDenial(_settings.CanReadField, _settings)) != null) return auth;


        ILocalizedContentText oResult;
        o.GroupFilter = _groupFilter;

        try
        {
            oResult = _languageRepository.GetRecord(o);
        }
        catch (Exception ex)
        {
            return JToken.FromObject(new ApiExceptionMessage("An error has occurred", ex));
        }

        return JToken.FromObject(oResult ?? new object());
    }

    [HttpPost("Localization/Save")]
    public virtual object LocalizationSave(LocalizedContentTextRecordInput o)
    {
	    var auth = TestAuthDenial(); // Check to see if user has permissions. (Method returns null if authorized, or an object if not authorized)
	    if (auth != null) return auth;
        if ((auth = TestAuthDenial(_settings.CanEditLabelText, _settings)) != null) return auth;

        var bResult = false;

        o.GroupFilter = _groupFilter;
        o.AuditChangeBy = User?.Identity?.Name ?? "";

        var oResult = _languageRepository.GetRecord(o);

        if (oResult == null)
        {
            if ((auth = TestAuthDenial(_settings.CanInsertLabelText, _settings)) != null) return auth;
        }

        try
        {
            oResult = _languageRepository.SaveRecord(o);
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


    [HttpPost("Localization/Delete")]
    public virtual object LocalizationDelete(LocalizedContentTextRecordInput o)
    {
	    var auth = TestAuthDenial(); // Check to see if user has permissions. (Method returns null if authorized, or an object if not authorized)
	    if (auth != null) return auth;
        if ((auth = TestAuthDenial(_settings.CanDeleteLabelText, _settings)) != null) return auth;


        var bResult = false;

        o.GroupFilter = _groupFilter;
        o.AuditChangeBy = User?.Identity?.Name ?? "";
        try
        {
            //bResult = 
            var oResult = _languageRepository.DeleteRecord(o);
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



    [HttpPost("ErrorMessage/DtList")]
    public virtual object ErrorMessageDtList([FromForm] LocalizedContentTextDataTablesInput o)
    {
	    var auth = TestAuthDenial(); // Check to see if user has permissions. (Method returns null if authorized, or an object if not authorized)
	    if (auth != null) return auth;
        if ((auth = TestAuthDenial(_settings.CanReadField, _settings)) != null) return auth;



        o.GroupFilter = _groupFilter;
        o.GroupKey = _errorMessageGroup;
        o.Language = null;

        return JToken.FromObject(_languageRepository.GetDataTableList(o));
    }

    [HttpPost("ErrorMessage/Data")]
    public virtual object ErrorMessageData(LocalizedContentTextRecordInput o)
    {
        var auth = TestAuthDenial(); // Check to see if user has permissions. (Method returns null if authorized, or an object if not authorized)
        if (auth != null) return auth;
        if ((auth = TestAuthDenial(_settings.CanReadField, _settings)) != null) return auth;

        o.GroupKey = _errorMessageGroup;
        return LocalizationData(o);
    }

    [HttpPost("ErrorMessage/Save")]
    public virtual object ErrorMessageSave(LocalizedContentTextRecordInput o)
    {
        var auth = TestAuthDenial(); // Check to see if user has permissions. (Method returns null if authorized, or an object if not authorized)
        if (auth != null) return auth;
        if ((auth = TestAuthDenial(_settings.CanEditErrorText, _settings)) != null) return auth;

        o.GroupKey = _errorMessageGroup;
        var oResult = _languageRepository.GetRecord(o);

        if (oResult == null)
        {
            if ((auth = TestAuthDenial(_settings.CanInsertErrorText, _settings)) != null) return auth;
        }
        return LocalizationSave(o);
    }

    [HttpPost("ErrorMessage/Delete")]
    public virtual object ErrorMessageDelete(LocalizedContentTextRecordInput o)
    {
        var auth = TestAuthDenial(); // Check to see if user has permissions. (Method returns null if authorized, or an object if not authorized)
        if (auth != null) return auth;
        if ((auth = TestAuthDenial(_settings.CanDeleteErrorText, _settings)) != null) return auth;

        o.GroupKey = _errorMessageGroup;
        return LocalizationDelete(o);
    }



    #region NonActions

    [NonAction]
    public JToken TestAuthDenial(bool? testValue = true, IWizardAdminSettings settings = null)
    {
	    if (settings is null)
	    {
		    PopulateDefaultSettings();
		    settings = _settings;
	    }
		if (!(testValue ?? false) || !settings.IsAuthorized ) return GetStatusObject("Permission Denied", "You do not have permissions to perform that action", ApiMessageType.Alert);

		return null;
    }

	[NonAction]
	public void InitializeControls(IWizardAdminSettings settings)
	{
		DefaultWizardControls.Clear();
		// Add Controls
		DefaultWizardControls.AddRange(new[] {
            // Add Button
            new WizardField(new { Label = "Button", FieldKey = "Button_", FieldType = "BUTTON", FieldClass = "btn btn-primary btn-sm w-100", WrapperClass = "col", FieldData = @"{'icon':'', 'action': {'type':'', 'screen':''}}" }),
            // Add Embedded Form
            new WizardField(new { Label = "Embeded Screen Form", FieldKey = "EmbedForm_", FieldType = "EMBED", FieldClass = "", WrapperClass = "col mb-3", FieldData = "{'screen': '', 'form': '', 'disabled':1,'useCardLayout': true, 'hasEditButton':false}" }),
            // Add Form
            new WizardField(new { Label = "Form", FieldKey = "Form_", FieldType = "FORM", FieldClass = "row", WrapperClass = "col mb-3", FieldData = "{'useCardLayout': 1}" }),
            // Add HTML Block
            new WizardField(new { Label = "HTML Text Block. Lorem ipsum dolor sit amet, consectetur adipiscing elit.", FieldKey = "HtmlBlock_", FieldType = "HTML", FieldClass = "", WrapperClass = "col", FieldData = "{}" }),
            // Add Horizontal Separator
            new WizardField(new { Label = "Horizontal Separator", FieldKey = "Separator_", FieldType = "SEPARATOR", FieldClass = "", WrapperClass = "col-12", FieldData = "{}" }),
            // Add Vertical Separator
            new WizardField(new { Label = "Vertical Separator", FieldKey = "Separator_", FieldType = "VSEPARATOR", FieldClass = "h-100", WrapperClass = "col-auto", FieldData = "{}" }),
            // Add Separator
            new WizardField(new { Label = "Form Drop-down", FieldKey = "DropDownSelect_", FieldType = "SELECT", FieldClass = "form-select form-select-sm", WrapperClass = "col", FieldData = "{'values': {}, 'labelClass': '', 'groupClass':'', 'inline':false }" }),
            // Add Textarea
            new WizardField(new { Label = "Multi-line Text Input", FieldKey = "TextArea_", FieldType = "TEXTAREA", FieldClass = "form-control form-control-sm", WrapperClass = "col", FieldData = "{'rows': 3}" }),
            // Add Date Input
            new WizardField(new { Label = "Date Input", FieldKey = "DateInput_", FieldType = "INPUT", FieldClass = "form-control form-control-sm", WrapperClass = "col", FieldData = "{'type': 'date'}" }),
            // Add Date/Time Input
            new WizardField(new { Label = "Date/Time Input", FieldKey = "DateTimeInput_", FieldType = "INPUT", FieldClass = "form-control form-control-sm", WrapperClass = "col", FieldData = "{'type': 'datetime-local'}" }),
            // Add Month Input
            new WizardField(new { Label = "Month Input", FieldKey = "MonthInput_", FieldType = "INPUT", FieldClass = "form-control form-control-sm", WrapperClass = "co", FieldData = "{'type': 'month'}" }),
            // Add Time Input
            new WizardField(new { Label = "Time Input", FieldKey = "TimeInput_", FieldType = "INPUT", FieldClass = "form-control form-control-sm", WrapperClass = "col", FieldData = "{'type': 'time'}" }),
            // Add Week Input
            new WizardField(new { Label = "Week Input", FieldKey = "WeekInput_", FieldType = "INPUT", FieldClass = "form-control form-control-sm", WrapperClass = "col", FieldData = "{'type': 'week'}" }),
            // Add Color Input
            new WizardField(new { Label = "Color Input", FieldKey = "ColorInput_", FieldType = "INPUT", FieldClass = "form-control form-control-sm", WrapperClass = "col", FieldData = "{'type': 'color', defaultValue: '#0000FF'}" }),
            // Add Checkbox Input
            new WizardField(new { Label = "Checkbox Input", FieldKey = "CheckboxInput_", FieldType = "INPUT", FieldClass = "", WrapperClass = "col", FieldData = "{'type': 'checkbox', 'labelClass': '', 'groupClass':'', 'inline':false }" }),
            // Add Radio Button Input
            new WizardField(new { Label = "Radio Button Input", FieldKey = "RadioInput_", FieldType = "INPUT", FieldClass = "", WrapperClass = "col", FieldData = "{'type': 'radio','values': { }, 'labelClass': '', 'groupClass':'', 'inline':false }" }),
            // Add Checkbox Input
            new WizardField(new { Label = "Hidden Input", FieldKey = "HiddenInput_", FieldType = "INPUT", FieldClass = "", WrapperClass = "col", FieldData = "{'type': 'hidden'}" }),
            // Add Slider Input
            new WizardField(new { Label = "Slider Input", FieldKey = "SliderInput_", FieldType = "INPUT", FieldClass = "form-range", WrapperClass = "col", FieldData = "{'type': 'range'}" }),
            // Add Text Input
            new WizardField(new { Label = "Text Input", FieldKey = "TextInput_", FieldType = "INPUT", FieldClass = "form-control form-control-sm", WrapperClass = "col", FieldData = "{'type': 'text', 'maxlength': 40}" }),
            // Add Password Input
            new WizardField(new { Label = "Password Input", FieldKey = "PasswordInput_", FieldType = "INPUT", FieldClass = "form-control form-control-sm", WrapperClass = "col", FieldData = "{'type': 'password', 'maxlength': 40}" }),
            // Add Phone Input
            new WizardField(new { Label = "Phone Input", FieldKey = "TextInput_", FieldType = "INPUT", FieldClass = "form-control form-control-sm", WrapperClass = "col", FieldData = "{'type': 'phone', 'maxlength': 20}" }),
            // Add URL Input
            new WizardField(new { Label = "URL Input", FieldKey = "UrlInput_", FieldType = "INPUT", FieldClass = "form-control form-control-sm", WrapperClass = "col", FieldData = "{'type': 'url', 'maxlength': 100}" }),
            // Add Email Input
            new WizardField(new { Label = "Email Input", FieldKey = "EmailInput_", FieldType = "INPUT", FieldClass = "form-control form-control-sm", WrapperClass = "col", FieldData = "{'type': 'email', 'maxlength': 100}" }),
            // Add Number Input
            new WizardField(new { Label = "Number Input", FieldKey = "NumberInput_", FieldType = "INPUT", FieldClass = "form-control form-control-sm", WrapperClass = "col", FieldData = "{'type': 'number', 'maxlength': 11}" }),
            // Add Search Input
            new WizardField(new { Label = "Search Input", FieldKey = "SearchInput_", FieldType = "INPUT", FieldClass = "form-control form-control-sm", WrapperClass = "col", FieldData = "{'type': 'search', 'maxlength': 40}" }),
		});
	}

	[NonAction]
    protected WizardTreeNode GetWizardTreeNode(WizardField currentNode, IEnumerable<WizardField> fieldData, int nDepth = 0)
    {
        var childList = fieldData.Where(o => o.GroupKey.Equals(currentNode.GroupKey, StringComparison.InvariantCultureIgnoreCase) && o.ParentKey.Equals(currentNode.FieldKey, StringComparison.InvariantCultureIgnoreCase));
        var childNodes = new List<WizardTreeNode>();
        var buttonNodes = new List<WizardTreeNode>();
        if (nDepth > nMaxTreeDepth)
            childList = new WizardField[]{};

        foreach (var child in childList)
        {
            if (nDepth == 1 && child.FieldType.Equals("BUTTON", StringComparison.InvariantCulture))
            {
                buttonNodes.Add(GetWizardTreeNode(child, fieldData, nDepth + 1));
            }
            else
            {
                childNodes.Add(GetWizardTreeNode(child, fieldData, nDepth + 1));
            }
        }

        if (nDepth == 1)
        {
            var buttonNode = new WizardTreeNode()
            {
                key = -(currentNode.Id),
                title = "Buttons",
                field_data = new WizardField(new { GroupKey = currentNode.GroupKey, FieldType = "BUTTONGROUP", FieldKey = currentNode.FieldKey, Id = -(currentNode.Id), ParentKey = currentNode.ParentKey }),
                folder = true,
                children = buttonNodes
            };
            childNodes.Add(buttonNode);
        }


        var oData = new WizardTreeNode()
        {
            key = currentNode.Id,
            title = currentNode.FieldType.Equals("WIZARD", StringComparison.InvariantCultureIgnoreCase) ? currentNode.GroupKey : currentNode.FieldKey,
            field_data = currentNode,
            folder = childNodes.Any() || currentNode.FieldType.Equals("FORM", StringComparison.InvariantCultureIgnoreCase) || currentNode.FieldType.Equals("SCREEN", StringComparison.InvariantCultureIgnoreCase) || currentNode.FieldType.Equals("WIZARD", StringComparison.InvariantCultureIgnoreCase),
            children = childNodes
        };



        return oData;
    }

    [NonAction]
    public virtual IEnumerable<WizardField> GetDefaultHtmlControls()
    {
        return DefaultWizardControls;
    }

    [NonAction]
    protected virtual void PopulateDefaultSettings() { }

    [NonAction]
    protected JToken GetStatusObject(string message, ApiMessageType msgType = ApiMessageType.Info) => GetStatusObject("", message, msgType);

    [NonAction]
    protected JToken GetStatusObject(string title, string message, ApiMessageType msgType = ApiMessageType.Info, Exception ex = null, string redirectUrl="")
    {
	    return JToken.FromObject(
		    ex != null ? 
			    new ApiExceptionMessage(message, ex, redirectUrl) : 
			    new ApiStatusMessage(message, title, msgType)
		    );
    }

	[NonAction]
    protected virtual IWizardContentField InitNewWizardField(IWizardContentField o)
    {
        switch (o.FieldType)
        {
            case nameof(WizardFieldTypes.EMBED):
            case nameof(WizardFieldTypes.FORM):
                return new WizardContentField(new
                {
                    o.Id,
                    o.GroupKey,
                    o.FieldKey,
                    o.FieldType,
                    o.FieldData,// = "{useCardLayout:1}",
                    o.FieldClass,
                    o.ParentKey,
                    o.DefaultLabel,
                    o.Label,
                    o.WrapperClass,
                    o.WrapperHtmlStart,
                    o.WrapperHtmlEnd,
                    o.AuditChangeType,
                    o.AuditChangeBy,
                    o.AuditChangeDate,
                    o.GroupFilter,
                    o.Order
                });
        }

        return o;
    }
    #endregion


    #region Member Classes
    public class WizardTreeNode
    {
        public long key { get; set; }
        public string title { get; set; }
        public IWizardContentField field_data { get; set; }
        public bool folder { get; set; }
        public IEnumerable<WizardTreeNode> children { get; set; }

    }

    public class WizardField : WizardComponentField
    {
        public new string AuditChangeBy
        {
            get => base.AuditChangeBy; 
            set => base.AuditChangeBy = value;
        }

        public new string FieldKey
        {
            get => base.FieldKey;
            set => base.FieldKey = value;
        }

        public string NewFieldKey { get; set; }

        public WizardField() : base(null)
        {

        }
        public WizardField(object o) : base(o)
        {
            NewFieldKey = DataUtility.ParseString(o, "NewFieldKey");
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        public new JToken FieldData
        {
            get => GetFieldData();
            set => base.FieldData = JsonConvert.SerializeObject(value);
        }
    }

    public class WizardInputModel : WizardField, IWizardModelBase
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<string>))]
        public string Wizard { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<string>))]
        public string Screen { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<int>))]
        public int Section { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<int>))]
        public int Step { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<bool>))]
        public bool IsLivePreview { get; set; }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        public Dictionary<string, string> ValidFields { get; } = new Dictionary<string, string>();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        public Dictionary<string, string> InvalidFields { get; } = new Dictionary<string, string>();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<string>))]
        public new string AuditChangeBy { get; set; }

        public WizardInputModel() => Initialize(null);

        public WizardInputModel(object o) => Initialize(o);

        public void Validate(ValidationOptions opts)
        {
        }

        protected void Initialize(object o)
        {
            Section = DataUtility.Parse<int>(o, "Section");
            Step = DataUtility.Parse<int>(o, "Step");
            //base.Initialize(o);
        }
    }


    public class LocalizedContentTextRecordInput : LocalizedContentText
    {
        public string EditToken { get; set; }

        public new string GroupKey
        {
            get => base.GroupKey;
            set => base.GroupKey = value;
        }

        public new string FieldKey
        {
            get => base.FieldKey;
            set => base.FieldKey = value;
        }

        public new string Language
        {
            get => base.Language;
            set => base.Language = value;
        }

        public new string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        public new string AuditChangeType
        {
            get => base.AuditChangeType;
            set => base.AuditChangeType = value;
        }

        public new string AuditChangeBy
        {
            get => base.AuditChangeBy;
            set => base.AuditChangeBy = value;
        }

        public new DateTime AuditChangeDate
        {
            get => base.AuditChangeDate;
            set => base.AuditChangeDate = value;
        }
        public string GroupFilter { get; set; }
    }


    /// <summary>
    /// Class LocalizedContentFieldRecordInput.
    /// Implements the <see cref="LocalizedContentField" /> model with public property setters. This class will be used to pass input data from the client to the API methods.
    /// </summary>
    public class LocalizedContentFieldRecordInput : LocalizedContentField, ILocalizedContentField
    {
        /// <summary>
        /// Gets or sets the edit token.
        /// </summary>
        /// <value>The edit token.</value>
        /// TODO Edit XML Comment Template for EditToken
        public string EditToken { get; set; }

        /// <inheritdoc />
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
        [JsonConverter(typeof(JlwJsonConverter<long>))]
        public new long Id
        {
            get => base.Id;
            set => base.Id = value;
        }

        /// <inheritdoc />
        public new string GroupKey
        {
            get => base.GroupKey;
            set => base.GroupKey = value;
        }

        /// <inheritdoc />
        public new string FieldKey
        {
            get => base.FieldKey;
            set => base.FieldKey = value;
        }

        /// <inheritdoc />
        public new string FieldType
        {
            get => base.FieldType;
            set => base.FieldType = value;
        }

        /// <inheritdoc />
        public new string FieldData
        {
            get => base.FieldData;
            set => base.FieldData = value;
        }

        /// <inheritdoc />
        public new string FieldClass
        {
            get => base.FieldClass;
            set => base.FieldClass = value;
        }

        /// <inheritdoc />
        public new string ParentKey
        {
            get => base.ParentKey;
            set => base.ParentKey = value;
        }

        /// <inheritdoc />
        public new string DefaultLabel
        {
            get => base.DefaultLabel;
            set => base.DefaultLabel = value;
        }

        /// <inheritdoc />
        public new string WrapperClass
        {
            get => base.WrapperClass;
            set => base.WrapperClass = value;
        }

        /// <inheritdoc />
        public new string WrapperHtmlStart
        {
            get => base.WrapperHtmlStart;
            set => base.WrapperHtmlStart = value;
        }

        /// <inheritdoc />
        public new string WrapperHtmlEnd
        {
            get => base.WrapperHtmlEnd;
            set => base.WrapperHtmlEnd = value;
        }

        /// <inheritdoc />
        public new string AuditChangeType
        {
            get => base.AuditChangeType;
            set => base.AuditChangeType = value;
        }

        /// <inheritdoc />
        public new string AuditChangeBy
        {
            get => base.AuditChangeBy;
            set => base.AuditChangeBy = value;
        }

        /// <inheritdoc />
        public new DateTime AuditChangeDate
        {
            get => base.AuditChangeDate;
            set => base.AuditChangeDate = value;
        }

        /// <inheritdoc />
        public new int Order
        {
            get => base.Order;
            set => base.Order = value;
        }

        /// <inheritdoc />
        public new string GroupFilter
        {
            get => base.GroupFilter;
            set => base.GroupFilter = value;
        }
    }


    #endregion



    [Flags]
    public enum ValidationOptions
    {
        None,
        RequireId = 1,
        PersonalInfo = 2,
        PersonalAddress = 4,
        AdditionalInfo = 8,
        ApplicationOptions = 16,
        DigitalSignature = 32
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Schema;
using Jlw.Data.LocalizedContent;
using Jlw.Utilities.Data;
using Jlw.Utilities.Data.DataTables;
using Jlw.Utilities.WebApiUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Jlw.Web.Rcl.LocalizedContent.Areas.ModularWizardAdmin.Controllers;

[ApiController]
[Authorize]
[Produces("application/json")]
[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[JsonConverter(typeof(DefaultContractResolver))]
[JsonObject(NamingStrategyType = typeof(DefaultNamingStrategy))]
public abstract class ApiController : WizardApiBaseController
{
    protected string _groupFilter = "";
    protected string _errorMessageGroup = "";
    protected bool _unlockApi = false; // Set this flag to true when overriding API in order to enable access to API methods
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

    protected ApiController(IWizardFactoryRepository repository, IWizardFactory wizardFactory, ILocalizedContentFieldRepository fieldRepository, ILocalizedContentTextRepository languageRepository, IWizardAdminSettings settings) : base(repository, wizardFactory)
    {
        _fieldRepository = fieldRepository;
        _languageRepository = languageRepository;
        _settings = settings;
        HiddenFilterPrefix = settings.HiddenFilterPrefix ?? "";
        PreviewRecordData = settings.PreviewRecordData ?? new {};
        InitializeControls(_settings);
    }

    public void InitializeControls(IWizardAdminSettings settings) {
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
            // Add Separator
            new WizardField(new { Label = "Separator", FieldKey = "Separator_", FieldType = "SEPARATOR", FieldClass = "", WrapperClass = "col-12", FieldData = "{}" }),
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

    [HttpGet("")]
    public virtual object Index()
    {
        return new { };
    }

    [HttpPost("")]
    public virtual object GetWizard(WizardInputModel model)
    {
        if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));

        if (String.IsNullOrWhiteSpace(model.Screen))
        {
            var fields = DataRepository.GetWizardFields(model.Wizard, model.Wizard, _groupFilter);
            var screen = fields.OrderBy(o => o.Order).FirstOrDefault(o => o.FieldType.Equals("SCREEN", StringComparison.InvariantCultureIgnoreCase));

            model.Screen = screen?.FieldKey;
        }
        return WizardFactory.CreateWizardScreenContent(model.Wizard, model.Screen, model.IsLivePreview ? PreviewRecordData : new { });
    }

    /// TODO Edit XML Comment Template for Data
    [HttpPost("GetField")]
    public virtual object GetFieldData(WizardField o)
    {
        ILocalizedContentField oResult;
        o.GroupFilter = _groupFilter;
        if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));

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



    /// <summary>Saves the submitted data from the wizard.</summary>
    /// <param name="model">The model.</param>
    /// <returns>System.Object.</returns>
    [Route("Save")]
    [HttpPost]
    public virtual object SaveWizard(WizardInputModel model)
    {
        if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));

        return ProcessWizard(model, true);
    }
    

    [HttpPost("SaveNode")]
    public virtual object SaveNode(WizardFieldUpdateData o)
    {
        var bResult = false;
        o.GroupFilter = _groupFilter;

        if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));

        try
        {
            o.AuditChangeBy = User.Identity?.Name ?? "";
            if (o.FieldName.Equals("FieldKey", StringComparison.InvariantCultureIgnoreCase))
            {
                var field = _fieldRepository.GetRecord(new Data.LocalizedContent.LocalizedContentField(o));
                //return DataRepository.RenameWizardFieldRecursive(new WizardContentField(new { Id = o.Id }), o.FieldValue);
                return RenameField(new WizardField(field){NewFieldKey = o.FieldValue});
            }
            else {}
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

    [HttpPost("NewWizard")]
    public virtual object NewWizard(LocalizedContentField.Controllers.ApiController.LocalizedContentFieldRecordInput o)
    {
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


        if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));

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

    [HttpPost("NewScreen")]
    public virtual object NewScreen(LocalizedContentField.Controllers.ApiController.LocalizedContentFieldRecordInput o)
    {
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
        //o.ParentKey ??= "";

        if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));


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


    /// <summary>
    /// Saves the specified o.
    /// </summary>
    /// <param name="o">The o.</param>
    /// <returns>System.Object.</returns>
    /// TODO Edit XML Comment Template for Save
    [HttpPost("SaveField")]
    public virtual object SaveField(WizardField o)
    {
        var bResult = false;
        o.AuditChangeBy = User.Identity?.Name ?? "";
        o.FieldKey = _reFieldName.Replace(o.FieldKey, "_");
        //o.GroupFilter = _groupFilter;
        IWizardContentField field = o;
        if (field.Id < 1)
            field = InitNewWizardField(field);
        
        if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));

        ILocalizedContentField oResult = null;
        try
        {
            oResult = _fieldRepository.SaveRecord(new Data.LocalizedContent.LocalizedContentField(field));
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
        var bResult = false;
        o.GroupFilter = _groupFilter;

        if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));

        try
        {
            o.AuditChangeBy = User.Identity?.Name ?? "";
            var oResult = this.DataRepository.DeleteWizardFieldRecursive(o);//.DeleteRecord(o);
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
        var bResult = false;
        o.GroupFilter = _groupFilter;

        if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));
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
        var bResult = false;
        o.GroupFilter = _groupFilter;

        if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));
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
    public virtual object SaveWizard(IEnumerable<WizardInputModel> nodeList)
    {
        bool bSuccess = true;
        if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));

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

    [HttpGet("List")]
    public virtual IEnumerable<object> GetWizardList()
    {
        if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));

        //WizardFactory.CreateWizardContent("");
        var data = DataRepository.GetWizardFields(null, _groupFilter);

        List<Object> wizardList = new List<object>();

        var aWizards = data.Where(o => o.FieldType.Equals("WIZARD", StringComparison.InvariantCultureIgnoreCase));
        foreach (var currentNode in aWizards)
        {
            wizardList.Add(currentNode);
        }

        return wizardList;
    }

    [HttpGet("Components/{groupKey?}")]
    public virtual object GetComponentList(string groupKey="")
    {
        if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));

        if (groupKey.Equals("jlwNativeHtmlControls", StringComparison.InvariantCultureIgnoreCase))
            return GetDefaultHtmlControls();

        return DataRepository.GetComponentList(groupKey).Select(o=>new WizardField(o));  //GetWizardFields(groupKey);
    }

    [HttpGet("Tree/{groupKey?}")]
    public virtual object GetWizardTree(string groupKey = "")
    {
        if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));

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

    [HttpPost("Localization/DtList")]
    public virtual object LocalizationDtList([FromForm] LocalizedContentTextDataTablesInput o)
    {
        o.GroupFilter = _groupFilter;
        o.Language = null;

        if (!_unlockApi) return JToken.FromObject(new DataTablesOutput(o));

        return JToken.FromObject(_languageRepository.GetDataTableList(o));
    }

    [HttpPost("Localization/Data")]
    public virtual object LocalizationData(LocalizedContentText.Controllers.ApiController.LocalizedContentTextRecordInput o)
    {
        ILocalizedContentText oResult;
        if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));
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
    public virtual object LocalizationSave(LocalizedContentText.Controllers.ApiController.LocalizedContentTextRecordInput o)
    {
        var bResult = false;
        if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));

        o.GroupFilter = _groupFilter;
        o.AuditChangeBy = User.Identity.Name;
        try
        {
            var oResult = _languageRepository.SaveRecord(o);
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
    public virtual object LocalizationDelete(LocalizedContentText.Controllers.ApiController.LocalizedContentTextRecordInput o)
    {
        var bResult = false;

        if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));

        o.GroupFilter = _groupFilter;
        o.AuditChangeBy = User.Identity.Name;
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
        o.GroupFilter = _groupFilter;
        o.GroupKey = _errorMessageGroup;
        o.Language = null;
        if (!_unlockApi) return JToken.FromObject(new DataTablesOutput(o));

        return JToken.FromObject(_languageRepository.GetDataTableList(o));
    }

    [HttpPost("ErrorMessage/Data")]
    public virtual object ErrorMessageData(LocalizedContentText.Controllers.ApiController.LocalizedContentTextRecordInput o)
    {
        o.GroupKey = _errorMessageGroup;
        return LocalizationData(o);
    }

    [HttpPost("ErrorMessage/Save")]
    public virtual object ErrorMessageSave(LocalizedContentText.Controllers.ApiController.LocalizedContentTextRecordInput o)
    {
        o.GroupKey = _errorMessageGroup;
        return LocalizationSave(o);
    }

    [HttpPost("ErrorMessage/Delete")]
    public virtual object ErrorMessageDelete(LocalizedContentText.Controllers.ApiController.LocalizedContentTextRecordInput o)
    {
        o.GroupKey = _errorMessageGroup;
        return LocalizationDelete(o);
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
        //var data = DataRepository.GetComponentList("jlwNativeHtmlControls").Select(o => new WizardField(o));


        return DefaultWizardControls;
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

    //public class 
    
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

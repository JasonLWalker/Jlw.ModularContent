using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jlw.Data.LocalizedContent;
using Jlw.Utilities.Data;
using Jlw.Utilities.WebApiUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Jlw.Web.Rcl.LocalizedContent.Areas.ModularWizardAdmin.Controllers
{
    //[Area("ModularWizardAdmin")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    //[Route("admin/[area]/[controller]")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [JsonConverter(typeof(DefaultContractResolver))]
    [JsonObject(NamingStrategyType = typeof(DefaultNamingStrategy))]
    public abstract class ApiController : WizardApiBaseController
    {
        //private readonly ILocalizedContentTextRepository _repo;
        protected string _groupFilter;
        protected bool _unlockApi = false; // Set this flag to true when overriding API in order to enable access to API methods
        protected int nMaxTreeDepth = 10;


        private ILocalizedContentFieldRepository _fieldRepository { get; set; }

        public ApiController(IWizardFactoryRepository repository, IWizardFactory wizardFactory, ILocalizedContentFieldRepository fieldRepository) : base(repository, wizardFactory)
        {
            _groupFilter = "";
            _fieldRepository = fieldRepository;
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
            
            return WizardFactory.CreateWizardScreenContent(model.Wizard, model.Screen);
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
                o.AuditChangeBy = User.Identity.Name;
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
            var bResult = false;
            //o.GroupFilter = _groupFilter;

            if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));

            ILocalizedContentField oResult = null;
            try
            {
                o.AuditChangeBy = User.Identity.Name;
                oResult = _fieldRepository.SaveRecord(new Data.LocalizedContent.LocalizedContentField(o));
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
                o.AuditChangeBy = User.Identity.Name;
                //bResult = 
                var oResult = _fieldRepository.DeleteRecord(o);
                bResult = oResult != null;
                //_LocalizedContentFieldList.Refresh(); 
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

        [Route("SaveOrder")]
        [HttpPost]
        public virtual object SaveWizard(IEnumerable<WizardInputModel> nodeList)
        {
            bool bSuccess = true;
            if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));

            foreach (var node in nodeList)
            {
                node.AuditChangeBy = User.Identity.Name;
                var result = DataRepository.SaveFieldParentOrder(node);
                if (result.ParentKey != node.ParentKey || result.Order != node.Order)
                    bSuccess = false;
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
            var data = DataRepository.GetWizardFields(null);

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

            return DataRepository.GetComponentList(groupKey).Select(o=>new WizardField(o));  //GetWizardFields(groupKey);
        }

        [HttpGet("Tree/{groupKey?}")]
        public virtual object GetWizardTree(string groupKey = "")
        {
            if (!_unlockApi) return JToken.FromObject(new ApiStatusMessage("You do not have permissions to perform that action", "Permissions Denied", ApiMessageType.Alert));

            var data = DataRepository.GetWizardFields(groupKey).Select(o => new WizardField(o));

            var wizardList = new List<WizardTreeNode>();

            var aWizards = data.Where(o => o.FieldType.Equals("WIZARD", StringComparison.InvariantCultureIgnoreCase));
            foreach (var wizard in aWizards)
            {
                if (!string.IsNullOrWhiteSpace(groupKey))
                    wizardList.Add(GetWizardTreeNode(wizard, data));
            }
            return wizardList;
        }

        [NonAction]
        protected WizardTreeNode GetWizardTreeNode(WizardField currentNode, IEnumerable<WizardField> fieldData, int nDepth = 0)
        {
            var childList = fieldData.Where(o => o.GroupKey.Equals(currentNode.GroupKey, StringComparison.InvariantCultureIgnoreCase) && o.ParentKey.Equals(currentNode.FieldKey, StringComparison.InvariantCultureIgnoreCase));
            var childNodes = new List<WizardTreeNode>();

            if (nDepth > nMaxTreeDepth)
                childList = new WizardField[]{};

            foreach (var child in childList)
            {
                childNodes.Add(GetWizardTreeNode(child, fieldData, nDepth + 1));
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

        public class WizardTreeNode
        {
            public long key { get; set; }
            public string title { get; set; }
            public IWizardContentField field_data { get; set; }
            public bool folder { get; set; }
            public IEnumerable<WizardTreeNode> children { get; set; }

        }

        public class WizardField : WizardContentField
        {
            public new string AuditChangeBy { get; set; }

            public WizardField() : base(null)
            {

            }
            public WizardField(object o) : base(o)
            {

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
}

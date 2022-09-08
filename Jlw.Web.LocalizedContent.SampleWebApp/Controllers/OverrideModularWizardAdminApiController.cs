using Jlw.Data.LocalizedContent;
using Jlw.Utilities.Data.DataTables;
using Jlw.Web.Rcl.LocalizedContent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Jlw.Web.LocalizedContent.SampleWebApp.Controllers
{
    [Authorize("ContentOverrideAdmin")]
    [Route("admin/api/OverrideModularWizard/")]
    public class OverrideModularWizardAdminApiController : Jlw.Web.Rcl.LocalizedContent.Areas.ModularWizardAdmin.Controllers.ApiController
    {
        public OverrideModularWizardAdminApiController(IWizardFactoryRepository repository, IWizardFactory wizardFactory, ILocalizedContentFieldRepository fieldRepository, ILocalizedContentTextRepository languageRepository, IWizardAdminSettings settings) : base (repository, wizardFactory, fieldRepository, languageRepository, settings)
        {
            _groupFilter = "Sample%";
            _errorMessageGroup = "SampleWizard_Errors";
            _unlockApi = true;
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

        public override object ErrorMessageDtList([FromForm] LocalizedContentTextDataTablesInput o)
        {
            o.GroupFilter = _groupFilter;
            o.GroupKey = "SampleWizard_Errors";
            o.Language = null;
            if (!_unlockApi) return JToken.FromObject(new DataTablesOutput(o));

            return JToken.FromObject(_languageRepository.GetDataTableList(o));
        }


    }
}
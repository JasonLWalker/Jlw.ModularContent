using Jlw.Data.LocalizedContent;
using Jlw.Web.Rcl.LocalizedContent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.Web.LocalizedContent.SampleWebApp.Controllers
{
    [Authorize("ContentOverrideAdmin")]
    [Route("admin/api/OverrideModularWizard/")]
    public class OverrideModularWizardAdminApiController : Jlw.Web.Rcl.LocalizedContent.Areas.ModularWizardAdmin.Controllers.ApiController
    {
        public OverrideModularWizardAdminApiController(IWizardFactoryRepository repository, IWizardFactory wizardFactory, ILocalizedContentFieldRepository fieldRepository, ILocalizedContentTextRepository languageRepository, IWizardAdminSettings settings) : base (repository, wizardFactory, fieldRepository, languageRepository, settings)
        {
            _groupFilter = "Sample%";
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
        
    }
}
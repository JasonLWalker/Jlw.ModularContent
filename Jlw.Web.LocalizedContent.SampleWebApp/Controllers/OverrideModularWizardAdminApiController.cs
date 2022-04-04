using Jlw.Data.LocalizedContent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.Web.LocalizedContent.SampleWebApp.Controllers
{
    [Authorize("ContentOverrideAdmin")]
    [Route("admin/api/OverrideModularWizard/")]
    public class OverrideModularWizardAdminApiController : Jlw.Web.Rcl.LocalizedContent.Areas.ModularWizardAdmin.Controllers.ApiController
    {
        public OverrideModularWizardAdminApiController(IWizardFactoryRepository repository, IWizardFactory wizardFactory, ILocalizedContentFieldRepository fieldRepository, ILocalizedContentTextRepository languageRepository) : base (repository, wizardFactory, fieldRepository, languageRepository)
        {
            _groupFilter = "Sample%";
            _unlockApi = true;
        }

        [HttpGet("")]
        public override object Index()
        {
            return base.Index();
        }
        
    }
}
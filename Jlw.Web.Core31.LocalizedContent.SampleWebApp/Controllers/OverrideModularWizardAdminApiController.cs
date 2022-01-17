using Jlw.Data.LocalizedContent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.Web.Core31.LocalizedContent.SampleWebApp.Controllers
{
    [Authorize("ContentOverrideAdmin")]
    [Route("admin/api/OverrideModularWizard/")]
    public class OverrideModularWizardAdminApiController : Jlw.Web.Rcl.LocalizedContent.Areas.ModularWizardAdmin.Controllers.ApiController
    {
        public OverrideModularWizardAdminApiController(IWizardFactoryRepository repository, IWizardFactory wizardFactory, ILocalizedContentFieldRepository fieldRepository) : base (repository, wizardFactory, fieldRepository)
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
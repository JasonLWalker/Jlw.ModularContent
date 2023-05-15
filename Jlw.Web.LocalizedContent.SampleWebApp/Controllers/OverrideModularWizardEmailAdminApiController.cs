using Jlw.Data.LocalizedContent;
using Jlw.Web.Rcl.LocalizedContent.Areas.ModularWizardEmailAdmin.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.Web.LocalizedContent.SampleWebApp.Controllers
{
    [Authorize("ContentOverrideAdmin")]
    [Route("admin/api/OverrideModularWizardEmail/")]
    public class OverrideModularWizardEmailAdminApiController : ApiController
    {
        public OverrideModularWizardEmailAdminApiController(ILocalizedContentFieldRepository fieldRepository, ILocalizedContentTextRepository languageRepository, IWizardFactoryRepository factoryRepository) : base (fieldRepository, languageRepository, factoryRepository)
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
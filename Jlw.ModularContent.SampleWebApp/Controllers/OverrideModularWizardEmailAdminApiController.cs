using Jlw.ModularContent;
using Jlw.ModularContent.Areas.ModularWizardEmailAdmin.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.Web.ModularContent.SampleWebApp.Controllers
{
    [Authorize("ContentOverrideAdmin")]
    [Route("admin/api/OverrideModularWizardEmail/")]
    public class OverrideModularWizardEmailAdminApiController : ApiController
    {
        public OverrideModularWizardEmailAdminApiController(IModularContentFieldRepository fieldRepository, IModularContentTextRepository languageRepository, IModularWizardFactoryRepository factoryRepository) : base (fieldRepository, languageRepository, factoryRepository)
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
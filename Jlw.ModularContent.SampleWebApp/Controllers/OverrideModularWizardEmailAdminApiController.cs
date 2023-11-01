using Jlw.ModularContent;
using Jlw.ModularContent.Areas.ModularContentEmailAdmin.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.Web.ModularContent.SampleWebApp.Controllers
{
    [Authorize("ContentOverrideAdmin")]
    [Route("admin/api/OverrideModularWizardEmail/")]
    public class OverrideModularWizardEmailAdminApiController : EmailApiController
    {
        public OverrideModularWizardEmailAdminApiController(IModularContentFieldRepository fieldRepository, IModularContentTextRepository languageRepository, IModularWizardFactoryRepository factoryRepository, IModularWizardFactory factory) : base (fieldRepository, languageRepository, factoryRepository, factory)
        {
            _groupFilter = "Sample%";
            _unlockApi = true;


            PreviewRecordData = new
            {
                FirstName = "John",
                LastName = "Doe",
            };
        }

        [HttpGet("")]
        public override object Index()
        {
            return base.Index();
        }
        
    }
}
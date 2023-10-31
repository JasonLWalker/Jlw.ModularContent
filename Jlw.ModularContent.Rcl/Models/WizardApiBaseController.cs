using Microsoft.AspNetCore.Mvc;

namespace Jlw.ModularContent
{
    [ApiController]
    public abstract class WizardApiBaseController : ControllerBase
    {
        protected IModularWizardFactoryRepository DataRepository { get; set; }
        protected virtual IWizardFactory WizardFactory { get; set; }
        protected string WizardPrefix { get; set; }


        public WizardApiBaseController(IModularWizardFactoryRepository repository, IWizardFactory wizardFactory)
        {
            DataRepository = repository;
            WizardFactory = wizardFactory;
            WizardPrefix = "";
        }

        [NonAction]
        protected virtual object ProcessWizard(object model, bool isSave = false)
        {
            return WizardFactory.CreateWizardContent(null, model);
        }

    }
}
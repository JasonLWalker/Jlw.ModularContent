using Microsoft.AspNetCore.Mvc;

namespace Jlw.ModularContent
{
    [ApiController]
    public abstract class ModularWizardApiBaseController : ControllerBase
    {
        protected IModularWizardFactoryRepository DataRepository { get; set; }
        protected virtual IModularWizardFactory WizardFactory { get; set; }
        protected string WizardPrefix { get; set; }


        public ModularWizardApiBaseController(IModularWizardFactoryRepository repository, IModularWizardFactory wizardFactory)
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
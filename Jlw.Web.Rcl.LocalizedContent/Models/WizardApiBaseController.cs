using Jlw.Data.LocalizedContent;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.Web.Rcl.LocalizedContent
{
    [ApiController]
    public abstract class WizardApiBaseController : ControllerBase
    {
        protected IWizardFactoryRepository DataRepository { get; set; }
        protected virtual IWizardFactory WizardFactory { get; set; }
        protected string WizardPrefix { get; set; }


        public WizardApiBaseController(IWizardFactoryRepository repository, IWizardFactory wizardFactory)
        {
            DataRepository = repository;
            WizardFactory = wizardFactory;
            WizardPrefix = "";
        }

        [HttpPost]
        public virtual object Index(object model)
        {
            //TODO: add configuration

            return ProcessWizard(model, false);
        }

        /// <summary>Saves the submitted data from the wizard.</summary>
        /// <param name="model">The model.</param>
        /// <returns>System.Object.</returns>
        [Route("Wizard/Save")]
        [HttpPost]
        public virtual object Save(object model)
        {
            //TODO: add configuration
            //TODO: add configuration checking

            return ProcessWizard(model, true);
        }
        
        [NonAction]
        protected virtual object ProcessWizard(object model, bool isSave = false)
        {
            return WizardFactory.CreateWizardContent(null, model);
        }

    }
}
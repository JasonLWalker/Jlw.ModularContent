using Jlw.Data.LocalizedContent;
using Jlw.Utilities.Data;
using Jlw.Web.Rcl.LocalizedContent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Jlw.Web.Core31.LocalizedContent.SampleWebApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("~/api/Wizard")]
    public class SampleWizardApi : WizardApiBaseController
    {
        public SampleWizardApi(IWizardFactoryRepository repository, IWizardFactory wizardFactory) : base(repository, wizardFactory)
        {
            WizardPrefix = "SampleWizard";
        }

        [HttpPost]
        public virtual object Index(WizardInputModel model)
        {
            //TODO: add configuration

            return ProcessWizard(model, false);
        }

        /// <summary>Saves the submitted data from the wizard.</summary>
        /// <param name="model">The model.</param>
        /// <returns>System.Object.</returns>
        [Route("Wizard/Save")]
        [HttpPost]
        public virtual object Save(WizardInputModel model)
        {
            //TODO: add configuration
            //TODO: add configuration checking

            return ProcessWizard(model, true);
        }

        [NonAction]
        protected override object ProcessWizard(object inputModel, bool isSave = false)
        {
            var model = new WizardInputModel(inputModel);

            string groupKey = $"{WizardPrefix}_{model.Section}.{model.Step}";

            switch (model.Section)
            {
                case 0:
                    groupKey = $"{WizardPrefix}_0";
                    break;
                case 1:
                    if (model.Step < 1)
                        groupKey = $"{WizardPrefix}_1.1";
                    break;
                case 4:
                    if (model.Step < 1)
                        groupKey = $"{WizardPrefix}_4";
                    break;
            }

            //var fields = DataRepository.GetFieldData(groupKey);


            return WizardFactory.CreateWizardContent(groupKey, model);
        }


        public class WizardInputModel : IWizardModelBase
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
            [JsonConverter(typeof(JlwJsonConverter<int>))]
            public int Section { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
            [JsonConverter(typeof(JlwJsonConverter<int>))]
            public int Step { get; set; }

            public WizardInputModel() => Initialize(null);

            public WizardInputModel(object o) => Initialize(o);

            protected void Initialize(object o)
            {
                Section = DataUtility.Parse<int>(o, "Section");
                Step = DataUtility.Parse<int>(o, "Step");
                //base.Initialize(o);
            }
        }

    }
}

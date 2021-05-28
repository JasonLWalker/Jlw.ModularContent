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

        [NonAction]
        protected override object ProcessWizard(object inputModel, bool isSave = false)
        {
            var model = new PageWizardModel(inputModel);

            string groupKey = $"{WizardPrefix}_{model.Section}.{model.Step}";

            switch (model.Section)
            {
                case 0:
                    groupKey = $"{WizardPrefix}_0";
                    break;
            }

            //var fields = DataRepository.GetFieldData(groupKey);


            return WizardFactory.CreateWizardContent(groupKey, model);
        }


        public class PageWizardModel : IWizardModelBase
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
            [JsonConverter(typeof(JlwJsonConverter<int>))]
            public int Section { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
            [JsonConverter(typeof(JlwJsonConverter<int>))]
            public int Step { get; set; }

            public PageWizardModel() => Initialize(null);

            public PageWizardModel(object o) => Initialize(o);

            protected new void Initialize(object o)
            {
                //base.Initialize(o);
            }
        }

    }
}

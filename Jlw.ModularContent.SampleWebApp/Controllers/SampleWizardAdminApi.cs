using System;
using System.Collections.Generic;
using Jlw.ModularContent;
using Jlw.Utilities.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Jlw.Web.ModularContent.SampleWebApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("~/admin/api/SampleWizard")]
    public class SampleWizardAdminApi : ModularWizardApiBaseController
    {
        public SampleWizardAdminApi(IModularWizardFactoryRepository repository, IModularWizardFactory wizardFactory) : base(repository, wizardFactory)
        {
            //WizardPrefix = "SampleWizard";
        }

        [HttpPost]
        public virtual object Index(WizardInputModel model)
        {
            //TODO: add configuration

            return ProcessWizard(model, false);
        }

        /*
        /// <summary>Saves the submitted data from the wizard.</summary>
        /// <param name="model">The model.</param>
        /// <returns>System.Object.</returns>
        [Route("Save")]
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

                    if (isSave)
                    {
                        model.Validate(ValidationOptions.PersonalInfo);
                    }
                    break;
                case 3:
                    if (isSave)
                    {
                        model.Validate(ValidationOptions.ApplicationOptions);
                    }

                    break;
                case 4:
                    if (model.Step < 1)
                        groupKey = $"{WizardPrefix}_4";
                    break;
            }

            //var fields = DataRepository.GetFieldData(groupKey);


            return WizardFactory.CreateWizardContent(groupKey, model);
        }
        */

        public class WizardInputModel : IModularWizardModelBase
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
            [JsonConverter(typeof(JlwJsonConverter<int>))]
            public int Section { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
            [JsonConverter(typeof(JlwJsonConverter<int>))]
            public int Step { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
            public Dictionary<string, string> ValidFields { get; } = new Dictionary<string, string>();

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(DefaultNamingStrategy))]
            public Dictionary<string, string> InvalidFields { get; } = new Dictionary<string, string>();

            public WizardInputModel() => Initialize(null);

            public WizardInputModel(object o) => Initialize(o);

            public void Validate(ValidationOptions opts)
            {
                /*
                if ((opts & ValidationOptions.PersonalInfo) != 0)
                {
                    InvalidFields["StudentFirstName"] = "Please select a reason for this request";
                }

                if (opts == ValidationOptions.ApplicationOptions)
                {
                    InvalidFields["SchoolYear"] = "Please select a reason for this request";
                    InvalidFields["TransferReason"] = "Please select a reason for this request";
                    InvalidFields["TransferComments"] = "Please provide additional details for the reason of this request";
                }
                */
            }

            protected void Initialize(object o)
            {
                Section = DataUtility.Parse<int>(o, "Section");
                Step = DataUtility.Parse<int>(o, "Step");
                //base.Initialize(o);
            }
        }

        [Flags]
        public enum ValidationOptions
        {
            None,
            RequireId = 1,
            PersonalInfo = 2,
            PersonalAddress = 4,
            AdditionalInfo = 8,
            ApplicationOptions = 16,
            DigitalSignature = 32
        }
        
    }
}

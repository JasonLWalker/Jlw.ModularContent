using Jlw.LocalizedContent;
using Jlw.Web.Rcl.LocalizedContent.Areas.LocalizedContentText.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.Web.LocalizedContent.SampleWebApp.Controllers
{
    [Authorize("ContentOverrideAdmin")]
    [Route("admin/api/OverrideLocalizedText/")]
    public class OverrideLocalizedTextApiController : ApiController
    {
        public OverrideLocalizedTextApiController(ILocalizedContentTextRepository repository) : base (repository)
        {
            _groupFilter = "Sample%";
            _unlockApi = true;
        }

    }
}
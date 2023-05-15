using Jlw.Data.LocalizedContent;
using Jlw.Web.Rcl.LocalizedContent.Areas.LocalizedGroupDataItem.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.Web.LocalizedContent.SampleWebApp.Controllers
{
    [Authorize("ContentOverrideAdmin")]
    [Route("admin/api/OverrideLocalizedDataItems")]
    public class OverrideLocalizedDataItemsApiController : ApiController
    {
        public OverrideLocalizedDataItemsApiController(ILocalizedGroupDataItemRepository repository) : base (repository)
        {
            _groupFilter = "Sample%";
            _unlockApi = true;
        }

    }
}
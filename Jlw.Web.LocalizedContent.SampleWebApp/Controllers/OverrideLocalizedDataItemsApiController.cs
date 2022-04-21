using Jlw.Data.LocalizedContent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.Web.LocalizedContent.SampleWebApp.Controllers
{
    [Authorize("ContentOverrideAdmin")]
    [Route("admin/api/OverrideLocalizedDataItems")]
    public class OverrideLocalizedDataItemsApiController : Jlw.Web.Rcl.LocalizedContent.Areas.LocalizedGroupDataItem.Controllers.ApiController
    {
        public OverrideLocalizedDataItemsApiController(ILocalizedGroupDataItemRepository repository) : base (repository)
        {
            _groupFilter = "Sample%";
            _unlockApi = true;
        }

    }
}
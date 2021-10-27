using Jlw.Data.LocalizedContent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.Web.Core31.LocalizedContent.SampleWebApp.Controllers
{
    [Authorize("ContentOverrideAdmin")]
    [Route("admin/api/OverrideLocalizedDataItems/")]
    public class OverrideLocalizedDataItemsApiController : Jlw.Web.Rcl.LocalizedContent.Areas.LocalizedGroupDataItem.Controllers.ApiController
    {
        public OverrideLocalizedDataItemsApiController(ILocalizedGroupDataItemRepository repository) : base (repository)
        {
            _groupFilter = "Sample%";
            //_unlockApi = true;
        }

        /*

        public override object DtList(LocalizedContentFieldDataTablesInput o)
        {
            return base.DtList(o);
        }

        public override object Data(LocalizedContentFieldRecordInput o)
        {
            return base.Data(o);
        }

        public override object Save(LocalizedContentFieldRecordInput o)
        {
            return base.Save(o);
        }

        public override object Delete(LocalizedContentFieldRecordInput o)
        {
            return base.Delete(o);
        }
        */
    }
}
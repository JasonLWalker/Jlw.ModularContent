using Jlw.Data.LocalizedContent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.Web.LocalizedContent.SampleWebApp.Controllers
{
    [Authorize("ContentOverrideAdmin")]
    [Route("admin/api/OverrideLocalizedField/")]
    public class OverrideLocalizedFieldApiController : Jlw.Web.Rcl.LocalizedContent.Areas.LocalizedContentField.Controllers.ApiController
    {
        public OverrideLocalizedFieldApiController(ILocalizedContentFieldRepository repository) : base (repository)
        {
            _groupFilter = "Sample%";
            _unlockApi = true;
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
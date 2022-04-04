using Jlw.Data.LocalizedContent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.Web.LocalizedContent.SampleWebApp.Controllers
{
    [Authorize("ContentOverrideAdmin")]
    [Route("admin/api/OverrideLocalizedText/")]
    public class OverrideLocalizedTextApiController : Jlw.Web.Rcl.LocalizedContent.Areas.LocalizedContentText.Controllers.ApiController
    {
        public OverrideLocalizedTextApiController(ILocalizedContentTextRepository repository) : base (repository)
        {
            _groupFilter = "Sample%";
            _unlockApi = true;
        }

        /*
        public override object DtList(LocalizedContentTextDataTablesInput o)
        {
            return base.DtList(o);
        }

        public override object Data(LocalizedContentTextRecordInput o)
        {
            return base.Data(o);
        }

        public override object Save(LocalizedContentTextRecordInput o)
        {
            return base.Save(o);
        }

        public override object Delete(LocalizedContentTextRecordInput o)
        {
            return base.Delete(o);
        }
        */
    }
}
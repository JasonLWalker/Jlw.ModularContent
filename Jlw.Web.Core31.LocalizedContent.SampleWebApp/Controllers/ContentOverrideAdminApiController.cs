using Jlw.Data.LocalizedContent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.Web.Core31.LocalizedContent.SampleWebApp.Controllers
{
    [Authorize("ContentOverrideAdmin")]
    [Route("admin/ContentOverride/api/")]
    public class ContentOverrideAdminApiController : Jlw.Web.Rcl.LocalizedContent.Areas.LocalizedContentField.Controllers.ApiController
    {
        public ContentOverrideAdminApiController(ILocalizedContentFieldRepository repository) : base (repository)
        {
            _groupFilter = "%";
        }

        public override object DtList(LocalizedContentFieldDataTablesInput o)
        {
            return GetDataTableList(o);
        }

        public override object Data(LocalizedContentFieldRecordInput o)
        {
            return GetRecordData(o);
        }

        public override object Save(LocalizedContentFieldRecordInput o)
        {
            return SaveRecordData(o);
        }

        public override object Delete(LocalizedContentFieldRecordInput o)
        {
            return DeleteRecordData(o);
        }
    }
}
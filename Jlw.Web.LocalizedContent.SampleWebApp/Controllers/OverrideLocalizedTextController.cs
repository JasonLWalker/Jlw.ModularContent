using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.Web.LocalizedContent.SampleWebApp.Controllers
{
    //[Route("~/Admin/[controller]")]
    [Authorize("ContentOverrideAdmin")]
    public class OverrideLocalizedTextController : Controller
    {
        [HttpGet("~/Admin/[controller]/{groupKey?}/{parentKey?}")]
        public ActionResult Index(string groupKey, string parentKey)
        {
            ViewData["groupKey"] = groupKey;
            ViewData["apiOverrideUrl"] = Url.Content("~/admin/api/OverrideLocalizedText/");
            ViewData["adminOverrideUrl"] = Url.Action("Index", "OverrideLocalizedText", new { groupKey = (string)null, fieldKey = (string)null });
            ViewData["adminFieldOverrideUrl"] = Url.Action("Index", "OverrideLocalizedField", new { groupKey = (string)null, fieldKey = (string)null });
            ViewData["fieldKey"] = parentKey;
            ViewData["PageTitle"] = "Content Override Admin";

            ViewData["tinyMceAdditionalToolbar"] = "| customMenuButton";
            ViewData["tinyMceAdditionalMenu"] = "custom: { title: 'Custom Menu', items: 'alert btext itext ntext' }";
            ViewData["tinyMceAdditionalOptions"] = @"
menubar: 'file edit view custom format tools',
setup: function (editor) {
    editor.on('change', function () {
        editor.save();
    });

    editor.ui.registry.addMenuItem('alert', {
      text: 'My Custom Menu Item',
      onAction: function() {
        alert('Menu item clicked');
      }
    });

    editor.ui.registry.addMenuItem('btext', {
      text: 'Bold Text',
      onAction: function() {
        editor.insertContent('&nbsp;<strong>Some bold text!</strong>');
      }
    });
    editor.ui.registry.addMenuItem('itext', {
      text: 'Italic Text',
      onAction: function() {
        editor.insertContent('&nbsp;<em>Some italic text!</em>');
      }
    });
    editor.ui.registry.addMenuItem('ntext', {
      text: 'Normal Text',
      onAction: function() {
        editor.insertContent('&nbsp;Some plain text ...');
      }
    });

  editor.ui.registry.addMenuButton('customMenuButton', {
    text: 'Custom Listbox',
    fetch: function (callback) {
        var items = [
            { 
                text: 'Bold Text', 
                type: 'menuitem',
                onAction: function(_) {
                    editor.insertContent('&nbsp;<strong>Some bold text!</strong>');
                }
            },
            {
                text: 'Italic Text',
                type: 'menuitem',
                onAction: function(_) {
                    editor.insertContent('&nbsp;<em>Some italic text!</em>');
                }
            },
            {
                text: 'Regular Text', 
                type: 'menuitem',
                onAction: function(_) {
                    editor.insertContent('&nbsp;Some plain text ...');
                }
            }
        ];
        callback(items);
    }
  });
}
";

            return View("~/Areas/LocalizedContentText/Views/Admin/Index.cshtml");
        }

    }
}

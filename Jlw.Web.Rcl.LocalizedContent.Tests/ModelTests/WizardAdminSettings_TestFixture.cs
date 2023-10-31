using Jlw.ModularContent;
using Jlw.Utilities.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace Jlw.Web.Rcl.LocalizedContent.Tests.ModelTests
{
    [TestClass]
    public class WizardAdminSettings_TestFixture : BaseModelFixture<ModularWizardAdminSettings, WizardAdminSettingsTestSchema>
    {
        [TestMethod]
        [DataRow("{id:'value'}", "id", "value")]
        [DataRow("{myField:'myData'}", "myField", "myData")]
        public void TinyMceSettings_ShouldMatch_WhenSet(string jsonString, string fieldName, string expected)
        {
            var sut = new ModularWizardAdminSettings();
            sut.TinyMceSettings = JToken.Parse(jsonString);

            Assert.AreEqual(expected, sut.TinyMceSettings[fieldName]?.ToString());
        }
    }
}
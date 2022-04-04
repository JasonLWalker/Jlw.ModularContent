using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Jlw.Extensions.Testing.WebApp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Data.LocalizedContent.Tests.IntegrationTests
{
    //[TestClass]
    public class AnonymousClient_RouteTests : WebAppIntegrationFixtureBase<Jlw.Web.LocalizedContent.SampleWebApp.Startup>
    {

        [TestMethod]
        #region URI Data
        [DataRow("", HttpStatusCode.OK)]
        [DataRow("/", HttpStatusCode.OK)]
        [DataRow("/Home", HttpStatusCode.OK)]
        [DataRow("/Home/Index", HttpStatusCode.OK)]
        [DataRow("/Admin", HttpStatusCode.NotFound)]
        [DataRow("/Admin/LocalizedContentField", HttpStatusCode.NotFound)]
        [DataRow("/Admin/LocalizedContentField/", HttpStatusCode.NotFound)]
        [DataRow("/Admin/LocalizedContentField/Index", HttpStatusCode.NotFound)]
        [DataRow("/Admin/LocalizedContentField/Wizard", HttpStatusCode.NotFound)]
        [DataRow("/Admin/LocalizedContentField/Email", HttpStatusCode.NotFound)]

        [DataRow("/Admin/LocalizedContentField/api", HttpStatusCode.NotFound)]
        [DataRow("/Admin/LocalizedContentField/api/", HttpStatusCode.NotFound)]
        [DataRow("/Admin/LocalizedContentField/api/DtList", HttpStatusCode.NotFound)]
        [DataRow("/Admin/LocalizedContentField/api/Data", HttpStatusCode.NotFound)]
        [DataRow("/Admin/LocalizedContentField/api/Save", HttpStatusCode.NotFound)]
        [DataRow("/Admin/LocalizedContentField/api/Delete", HttpStatusCode.NotFound)]

        [DataRow("/Admin/api/OverrideLocalizedField", HttpStatusCode.MethodNotAllowed)]
        [DataRow("/Admin/api/OverrideLocalizedField/", HttpStatusCode.MethodNotAllowed)]
        [DataRow("/Admin/api/OverrideLocalizedField/DtList", HttpStatusCode.MethodNotAllowed)]
        [DataRow("/Admin/api/OverrideLocalizedField/Data", HttpStatusCode.MethodNotAllowed)]
        [DataRow("/Admin/api/OverrideLocalizedField/Save", HttpStatusCode.MethodNotAllowed)]
        [DataRow("/Admin/api/OverrideLocalizedField/Delete", HttpStatusCode.MethodNotAllowed)]
        #endregion
        public async Task StatusCode_Should_Match_Route_For_HttpGet(string uri, HttpStatusCode? expectedResponse)
        {
            var client = this.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            client.Timeout = TimeSpan.FromSeconds(5);
            var response = await client.GetAsync(uri);
            var actual = await response.Content.ReadAsStringAsync();

            if (expectedResponse is null || expectedResponse == HttpStatusCode.OK)
                Assert.IsTrue(response.IsSuccessStatusCode, $"Status Code of {response.StatusCode} did not match expected response of {expectedResponse}\n\nContents of Response:\n{actual}");
            else
                Assert.AreEqual(expectedResponse, response.StatusCode, $"Status Code of {response.StatusCode} did not match expected response of {expectedResponse}\n\nContents of Response:\n{actual}");
        }

        [TestMethod]
        #region URI Data
        [DataRow("", HttpStatusCode.OK)]
        [DataRow("/", HttpStatusCode.OK)]
        [DataRow("/Home", HttpStatusCode.OK)]
        [DataRow("/Home/Index", HttpStatusCode.OK)]
        [DataRow("/Admin", HttpStatusCode.NotFound)]
        [DataRow("/Admin/LocalizedContentField", HttpStatusCode.NotFound)]
        [DataRow("/Admin/LocalizedContentField/", HttpStatusCode.NotFound)]
        [DataRow("/Admin/LocalizedContentField/Index", HttpStatusCode.NotFound)]
        [DataRow("/Admin/LocalizedContentField/Wizard", HttpStatusCode.NotFound)]
        [DataRow("/Admin/LocalizedContentField/Email", HttpStatusCode.NotFound)]

        [DataRow("/Admin/LocalizedContentField/api", HttpStatusCode.NotFound)]
        [DataRow("/Admin/LocalizedContentField/api/", HttpStatusCode.NotFound)]
        [DataRow("/Admin/LocalizedContentField/api/DtList", HttpStatusCode.NotFound)]
        [DataRow("/Admin/LocalizedContentField/api/Data", HttpStatusCode.NotFound)]
        [DataRow("/Admin/LocalizedContentField/api/Save", HttpStatusCode.NotFound)]
        [DataRow("/Admin/LocalizedContentField/api/Delete", HttpStatusCode.NotFound)]

        [DataRow("/Admin/api/OverrideLocalizedField", HttpStatusCode.Redirect)]
        [DataRow("/Admin/api/OverrideLocalizedField/", HttpStatusCode.Redirect)]
        [DataRow("/Admin/api/OverrideLocalizedField/DtList", HttpStatusCode.Redirect)]
        [DataRow("/Admin/api/OverrideLocalizedField/Data", HttpStatusCode.Redirect)]
        [DataRow("/Admin/api/OverrideLocalizedField/Save", HttpStatusCode.Redirect)]
        [DataRow("/Admin/api/OverrideLocalizedField/Delete", HttpStatusCode.Redirect)]
        #endregion
        public async Task StatusCode_Should_Match_Route_For_HttpPost(string uri, HttpStatusCode? expectedResponse)
        {
            var client = this.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            client.Timeout = TimeSpan.FromSeconds(5);
            var response = await client.PostAsync(uri, new StringContent("", Encoding.UTF8, "application/x-www-form-urlencoded"));
            var actual = await response.Content.ReadAsStringAsync();

            if (expectedResponse is null || expectedResponse == HttpStatusCode.OK)
                Assert.IsTrue(response.IsSuccessStatusCode, $"Status Code of {response.StatusCode} did not match expected response of {expectedResponse}\n\nContents of Response:\n{actual}");
            else
                Assert.AreEqual(expectedResponse, response.StatusCode, $"Status Code of {response.StatusCode} did not match expected response of {expectedResponse}\n\nContents of Response:\n{actual}");
        }

        [TestMethod]
        #region URI Data
        [DataRow("/LocalizedContent/css/bootstrap.min.css")]
        [DataRow("/LocalizedContent/css/dataTables.bootstrap4.min.css")]
        [DataRow("/LocalizedContent/css/jquery.dataTables.min.css")]
        [DataRow("/LocalizedContent/js/bootbox.min.js")]
        [DataRow("/LocalizedContent/js/bootstrap.min.js")]
        [DataRow("/LocalizedContent/js/jquery.dataTables.min.js")]
        [DataRow("/LocalizedContent/js/jquery.min.js")]
        [DataRow("/LocalizedContent/js/libJlwAppBuilder.min.js")]
        [DataRow("/LocalizedContent/js/libJlwUtility.min.js")]
        [DataRow("/LocalizedContent/js/prism.min.js")]
        [DataRow("/LocalizedContent/js/toastr.min.js")]
        [DataRow("/LocalizedContent/tinymce/tinymce.min.js")]
        [DataRow("/LocalizedContent/tinymce/jquery.tinymce.min.js")]
        [DataRow("/LocalizedContent/font-awesome/js/all.js")]
        [DataRow("/LocalizedContent/font-awesome/js/fontawesome.js")]
        [DataRow("/LocalizedContent/font-awesome/js/regular.js")]
        [DataRow("/LocalizedContent/font-awesome/js/solid.js")]
        #endregion
        public async Task EmbeddedResources_Should_Be_Reachable(string uri)
        {
            var client = this.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            client.Timeout = TimeSpan.FromSeconds(5);
            var response = await client.GetAsync(uri);
            var actual = await response.Content.ReadAsStringAsync();

                Assert.IsTrue(response.IsSuccessStatusCode, $"Status Code of {response.StatusCode} did not match expected response.\n\nContents of Response:\n{actual}");
        }

        /*
        [TestMethod]
        [DataRow("/Admin/LocalizedContentField")]
        [DataRow("/Admin/LocalizedContentField/")]
        [DataRow("/Admin/LocalizedContentField/Index")]
        [DataRow("/Admin/LocalizedContentField/Wizard")]
        [DataRow("/Admin/LocalizedContentField/Email")]

        [DataRow("/Admin/LocalizedContentField/api")]
        [DataRow("/Admin/LocalizedContentField/api/")]
        [DataRow("/Admin/LocalizedContentField/api/DtList")]
        [DataRow("/Admin/LocalizedContentField/api/Data")]
        [DataRow("/Admin/LocalizedContentField/api/Save")]
        [DataRow("/Admin/LocalizedContentField/api/Delete")]
        public async Task Should_Redirect_To_Login(string uri)
        {
            var client = this.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            client.Timeout = TimeSpan.FromSeconds(5);
            var response = await client.GetAsync(uri);
            var actual = await response.Content.ReadAsStringAsync();


            StringAssert.EndsWith(response?.Headers?.Location?.LocalPath, "/Login", StringComparison.InvariantCultureIgnoreCase);
            

        }
        */
    }
}

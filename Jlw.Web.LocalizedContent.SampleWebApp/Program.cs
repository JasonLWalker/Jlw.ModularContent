using System;
using System.Linq;
using Jlw.Utilities.Data.DbUtility;
using Jlw.Web.Rcl.LocalizedContent;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MartinCostello.SqlLocalDb;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using TUser = Jlw.Extensions.Identity.Stores.ModularBaseUser;

namespace Jlw.Web.LocalizedContent.SampleWebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.WebHost.UseStaticWebAssets();

        #region Service Configuration
        builder.Services.AddMockedUsers();

        builder.Services.AddIdentityMocking<TUser>();

        builder.Services.AddSqlLocalDB(options =>
        {
            options.AutomaticallyDeleteInstanceFiles = true;
            options.StopTimeout = TimeSpan.FromMinutes(1);
        });

        builder.Services.AddSqlLocalDbInstance(options =>
        {
            options.InstanceName = builder.Configuration.GetConnectionString("LocalDbInstanceName");
            options.DatabaseName = builder.Configuration.GetConnectionString("LocalDatabaseName");
        });

        // local variable to hold connection string. Will be initialized with the IModularDbClient
        string connString = "";
        
        builder.Services.TryAddSingleton<IModularDbClient>(provider =>
        {
            var options = provider.GetService<IOptions<SqlLocalDbInstanceExtensions.LocalDbInstanceOptions>>() ?? new OptionsWrapper<SqlLocalDbInstanceExtensions.LocalDbInstanceOptions>(provider.GetRequiredService<SqlLocalDbInstanceExtensions.LocalDbInstanceOptions>());
            var dbInstanceInfo = provider.GetRequiredService<ISqlLocalDbInstanceInfo>();
            var stringbuilder = dbInstanceInfo.CreateConnectionStringBuilder();
            stringbuilder.InitialCatalog = options?.Value.DatabaseName ?? "";

            // Initialize connection string variable
            connString = stringbuilder.ConnectionString;

            return new ModularDbClient<SqlConnection, SqlCommand, SqlParameter, SqlConnectionStringBuilder>();
        });

        builder.Services.TryAddSingleton<IWizardSettings>(provider =>
        {
            var defaultSettings = new WizardSettings
            {
                Area = "",
                LinkGenerator = provider.GetRequiredService<LinkGenerator>(),
                ShowSideNav = true
            };
            defaultSettings.JsRoot = LocalizedContentExtensions.AreaName;
            return defaultSettings;
        });

        builder.Services.TryAddSingleton<IWizardAdminSettings>(provider =>
        {
            var linkGenerator = provider.GetRequiredService<LinkGenerator>();

            var DefaultSettings = new WizardAdminSettings();
            DefaultSettings.Area = "";
            DefaultSettings.LinkGenerator = linkGenerator;
            DefaultSettings.JsRoot = LocalizedContentExtensions.AreaName;
            DefaultSettings.ApiControllerName = "OverrideModularWizardAdminApi";
            DefaultSettings.ControllerName = "OverrideModularWizardAdmin";
            //DefaultSettings.ToolboxHeight = "calc(100vh - 58px)";
            DefaultSettings.ShowSideNav = true;
            //DefaultSettings.SideNavDefault = false;
            DefaultSettings.ShowWireFrame = true;
            //DefaultSettings.IsAdmin = true;
            //DefaultSettings.CanEdit = true;
            //DefaultSettings.CanInsert = true;
            //DefaultSettings.CanDelete = true;
            DefaultSettings.HiddenFilterPrefix = "Sample";
            DefaultSettings.LanguageList.Add(new SelectListItem("Chinese", "CN"));
            DefaultSettings.TinyMceSettings = JObject.Parse(@"
                {
                    useTinyMce: true,
                    plugins: 'image imagetools paste link code help',
                    height: 'calc(90vh - 350px)',
                    image_advtab: true,
                    paste_data_images: true,
                    toolbar: 'undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | outdent indent | code ',
                    menu: {
                        file: { title: 'File', items: 'newdocument ' },
                        edit: { title: 'Edit', items: 'undo redo | cut copy paste | selectall | searchreplace' },
                        view: { title: 'View', items: 'code | visualaid visualchars visualblocks | spellchecker | preview fullscreen' },
                        insert: { title: 'Insert', items: 'image link media template codesample inserttable | charmap emoticons hr | pagebreak nonbreaking anchor toc | insertdatetime' },
                        format: { title: 'Format', items: 'bold italic underline strikethrough superscript subscript codeformat | formats blockformats fontformats fontsizes align lineheight | forecolor backcolor | removeformat' },
                        tools: { title: 'Tools', items: 'spellchecker spellcheckerlanguage | code wordcount' },
                        table: { title: 'Table', items: 'inserttable | cell row column | tableprops deletetable' },
                        help: { title: 'Help', items: 'help' }
                    }
                }");
            return DefaultSettings;
        });

        
        
        builder.Services.AddLocalizedContentAdmin(options=>options.ConnectionString = connString);




        var mvcBuilder = builder.Services.AddControllersWithViews();

        if (builder.Environment.IsDevelopment())
        {
            mvcBuilder.AddRazorRuntimeCompilation();
        }

        mvcBuilder.AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.IgnoreNullValues = true;
        });


        mvcBuilder.AddNewtonsoftJson(o =>
        {
            // Added to test different naming resolver
            o.SerializerSettings.Converters.Add(new StringEnumConverter());
            o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        });


        builder.Services.AddAuthorization(options =>
        {
            options.AddDefaultLocalizedContentAdminAuthorizationPolicy();

            options.AddPolicy("ContentOverrideAdmin", policy =>
            {
                policy.RequireAssertion(context =>
                {
                    return context.User.Claims.Any(claim => claim.Type.Equals(nameof(LocalizedContentAccess), StringComparison.InvariantCultureIgnoreCase) && claim.Value.Equals(nameof(LocalizedContentAccess.Authorized), StringComparison.InvariantCultureIgnoreCase));
                });
            });
        });


        #endregion


        #region Application Configuration
        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthentication();    // Initializes MS Identity Authentication Middleware
        app.UseAuthorization();     // Initialized MS Identity Authorization Middleware

        app.UseSqlLocalDbInstance();

        app.UseDefaultLocalizedContentAdmin();


        app.UseEndpoints(endpoints =>
        {

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            endpoints.MapRazorPages();
            endpoints.MapControllers();
        });

        #endregion
        app.Run();
    }

}


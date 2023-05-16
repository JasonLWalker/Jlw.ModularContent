using System;
using System.Data;
using System.IO;
using Jlw.Utilities.Data.DbUtility;
using MartinCostello.SqlLocalDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using TOptions = Jlw.Web.LocalizedContent.SampleWebApp.SqlLocalDbInstanceExtensions.LocalDbInstanceOptions;

namespace Jlw.Web.LocalizedContent.SampleWebApp
{
    public static class SqlLocalDbInstanceExtensions
    {
        public class LocalDbInstanceOptions
        {
            public string InstanceName { get; set; }
            public string DatabaseName { get; set; }


        }

        public static void ImportSqlObject(string name, Server server, IModularDbClient dbClient)
        {
            string filename = "";
            if (dbClient.GetRecordScalar<int>(null, server.ConnectionContext.ConnectionString, new RepositoryMethodDefinition($"SELECT OBJECT_ID('{name}')", CommandType.Text, null, null, null)) < 1)
            {

                if (name.StartsWith("dbo.sp_", StringComparison.InvariantCultureIgnoreCase))
                    filename = $"../SqlSchema/StoredProcedure/{name}.SQL";
                else if (name.StartsWith("dbo.vw", StringComparison.InvariantCultureIgnoreCase))
                    filename = $"../SqlSchema/View/{name}.SQL";
                else 
                    filename = $"../SqlSchema/Table/{name}.SQL";

                string initScript = File.ReadAllText(filename);

                Console.WriteLine($"Executing SQL script '{filename}'");

                server.ConnectionContext.ExecuteNonQuery(initScript);
            }
        }


        public static IServiceCollection AddSqlLocalDbInstance(this IServiceCollection services, Action<TOptions> setupAction = null)
        {
            if (setupAction != null)
                services.Configure(setupAction);

            services.AddSingleton<ISqlLocalDbInstanceInfo>(provider =>
            {
                var options = provider.GetService<IOptions<TOptions>>() ?? new OptionsWrapper<TOptions>(provider.GetRequiredService<TOptions>());

                ISqlLocalDbApi localDB = provider.GetRequiredService<ISqlLocalDbApi>();

                if (!localDB.IsLocalDBInstalled())
                {
                    throw new NotSupportedException("SQL LocalDB is not installed.");
                }

                // Get the configured SQL LocalDB instance to store the TODO items in, creating it if it does not exist

                ISqlLocalDbInstanceInfo instance = localDB.GetOrCreateInstance(options?.Value?.InstanceName ?? "SqlLocalDb-DefaultDb");

                // Ensure that the SQL LocalDB instance is running and start it if not already running
                if (!instance.IsRunning)
                {
                    instance.Manage().Start();
                }

                // Get the SQL connection string to use to connect to the LocalDB instance
                //string connectionString = instance.GetConnectionString();
                 return instance;
            });

            services.AddSingleton<Server>(provider =>
            {
                var options = provider.GetService<IOptions<TOptions>>() ?? new OptionsWrapper<TOptions>(provider.GetRequiredService<TOptions>());

                var instance = provider.GetRequiredService<ISqlLocalDbInstanceInfo>();
                string connString = instance.GetConnectionString();
                //var conn = new SqlConnection(connString);
                var builder = instance.CreateConnectionStringBuilder();
                builder.InitialCatalog = options?.Value.DatabaseName ?? "";
                var conn = new SqlConnection(builder.ConnectionString);

                var server = new Server(new ServerConnection(conn));
                return server;
            });


            return services;

        }

        public static IApplicationBuilder UseSqlLocalDbInstance(this IApplicationBuilder app)
        {
            var server = app.ApplicationServices.GetRequiredService<Server>();
            
            var dbClient = app.ApplicationServices.GetRequiredService<IModularDbClient>();

            // Import Databases
            ImportSqlObject("dbo.DatabaseAuditTrail", server, dbClient);
            ImportSqlObject("dbo.LocalizedContentFields", server, dbClient);
            ImportSqlObject("dbo.LocalizedContentText", server, dbClient);
            ImportSqlObject("dbo.LocalizedGroupDataItems", server, dbClient);

            // Import Audit trail Stored Procs
            ImportSqlObject("dbo.sp_AuditTrailSave_LocalizedContentText", server, dbClient);
            ImportSqlObject("dbo.sp_AuditTrailSave_LocalizedContentField", server, dbClient);
            ImportSqlObject("dbo.sp_AuditTrailSave_LocalizedGroupDataItems", server, dbClient);

            // Import Content Field Stored Procs
            ImportSqlObject("dbo.sp_GetLocalizedContentFieldsDt", server, dbClient);
            ImportSqlObject("dbo.sp_SaveLocalizedContentFieldRecord", server, dbClient);
            ImportSqlObject("dbo.sp_SaveLocalizedContentFieldData", server, dbClient);
            ImportSqlObject("dbo.sp_DeleteLocalizedContentFieldRecord", server, dbClient);

            // Import Content Text Stored Procs
            ImportSqlObject("dbo.sp_SaveLocalizedContentTextRecord", server, dbClient);
            ImportSqlObject("dbo.sp_GetLocalizedContentTextDt", server, dbClient);
            ImportSqlObject("dbo.sp_GetLocalizedContentTextRecord", server, dbClient);
            ImportSqlObject("dbo.sp_DeleteLocalizedContentTextRecord", server, dbClient);

            // Import Content Text Stored Procs
            ImportSqlObject("dbo.sp_GetLocalizedGroupDataItems", server, dbClient);
            ImportSqlObject("dbo.sp_GetLocalizedGroupDataItemsDt", server, dbClient);
            ImportSqlObject("dbo.sp_GetLocalizedGroupDataItemRecord", server, dbClient);
            ImportSqlObject("dbo.sp_SaveLocalizedGroupDataItemRecord", server, dbClient);
            ImportSqlObject("dbo.sp_DeleteLocalizedGroupDataItemRecord", server, dbClient);


            // Import Wizard Stored Procs
            ImportSqlObject("dbo.sp_DeleteWizardFieldRecursive", server, dbClient);
            ImportSqlObject("dbo.sp_GetFormFields", server, dbClient);
            ImportSqlObject("dbo.sp_GetWizardFields", server, dbClient);
            ImportSqlObject("dbo.sp_GetWizardContentFieldRecord", server, dbClient);
            ImportSqlObject("dbo.sp_GetComponentList", server, dbClient);
            ImportSqlObject("dbo.sp_SaveLocalizedContentFieldParentOrder", server, dbClient);
            
            return app;
        }

    }
}

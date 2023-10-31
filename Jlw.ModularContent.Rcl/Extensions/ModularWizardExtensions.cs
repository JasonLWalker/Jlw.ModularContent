using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Jlw.Utilities.Data.DbUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace Jlw.ModularContent
{
    public static partial class ModularWizardExtensions
    {
        public const string AreaName = "ModularContent";
        private static readonly AssemblyName AssemblyName = typeof(ModularWizardExtensions).Assembly.GetName();
        public static readonly string FileVersion = FileVersionInfo.GetVersionInfo(typeof(ModularWizardExtensions).Assembly.Location).ProductVersion ?? "";
        public static readonly string Version = string.IsNullOrWhiteSpace(AssemblyName?.Version?.ToString()) || AssemblyName?.Version?.ToString() == "0.0.0.1" ? DateTime.Now.Ticks.ToString() : AssemblyName?.Version?.ToString();

        internal class LocalizedContentAdminConfigureOptions : IPostConfigureOptions<StaticFileOptions>
        {
            private readonly IWebHostEnvironment _environment;

            public LocalizedContentAdminConfigureOptions(IWebHostEnvironment environment)
            {
                _environment = environment;
            }

            public void PostConfigure(string name, StaticFileOptions options)
            {
                // Basic initialization in case the options weren't initialized by any other component
                options.ContentTypeProvider ??= new FileExtensionContentTypeProvider();
                if (options.FileProvider == null && _environment.WebRootFileProvider == null)
                {
                    throw new InvalidOperationException("Missing FileProvider.");
                }
                options.FileProvider ??= _environment.WebRootFileProvider;
                // Add our provider
                var filesProvider = new ManifestEmbeddedFileProvider(GetType().Assembly, "resources");
                options.FileProvider = new CompositeFileProvider(options.FileProvider, filesProvider);
            }
        }

        public static IServiceCollection AddModularWizardAdmin(this IServiceCollection services, Action<IModularDbOptions> options = null)
        {

            services.TryAddSingleton<IModularWizardAdminSettings>(new ModularWizardAdminSettings());
            services.AddModularGroupDataItemRepository(options);
            services.AddModularContentFieldRepository(options);
            services.AddModularContentTextRepository(options);

            services.AddModularWizardFactoryRepository(options);
            services.TryAddSingleton<IWizardFactory>(provider =>
            {
                return new WizardFactory(provider.GetRequiredService<IModularWizardFactoryRepository>());
            });

            services.TryAddSingleton<ILanguageListModel>(provider =>
            {
                var repo = provider.GetRequiredService<IModularGroupDataItemRepository>();
                return new LanguageListModel(repo);
            });

            services.TryAddSingleton<ICommonControlListModel>(provider =>
            {
                var repo = provider.GetRequiredService<IModularGroupDataItemRepository>();
                return new CommonControlListModel(repo);
            });

            services.ConfigureOptions(typeof(LocalizedContentAdminConfigureOptions));
            return services;
        }

        public static AuthorizationOptions AddDefaultModularContentAdminAuthorizationPolicy(this AuthorizationOptions options)
        {
            options.AddPolicy("LocalizedContentUser", policy =>
            {
                policy.RequireAssertion(context =>
                {
                    return context.User.Claims.Any(claim => claim.Type.Equals("LocalizedContentAccess", StringComparison.InvariantCultureIgnoreCase));
                });
            });

            options.AddPolicy("LocalizedContentCreate", policy =>
            {
                policy.RequireAssertion(context =>
                {
                    return context.User.HasClaim(claim => claim.Type.Equals("LocalizedContentAccess", StringComparison.InvariantCultureIgnoreCase) && claim.Value.Equals("Create", StringComparison.InvariantCultureIgnoreCase));
                });
            });

            options.AddPolicy("LocalizedContentRead", policy =>
            {
                policy.RequireAssertion(context =>
                {
                    return context.User.HasClaim(claim => claim.Type.Equals("LocalizedContentAccess", StringComparison.InvariantCultureIgnoreCase) && claim.Value.Equals("Read", StringComparison.InvariantCultureIgnoreCase));
                });
            });

            options.AddPolicy("LocalizedContentUpdate", policy =>
            {
                policy.RequireAssertion(context =>
                {
                    return context.User.HasClaim(claim => claim.Type.Equals("LocalizedContentAccess", StringComparison.InvariantCultureIgnoreCase) && claim.Value.Equals("Update", StringComparison.InvariantCultureIgnoreCase));
                });
            });

            options.AddPolicy("LocalizedContentDelete", policy =>
            {
                policy.RequireAssertion(context =>
                {
                    return context.User.HasClaim(claim => claim.Type.Equals("LocalizedContentAccess", StringComparison.InvariantCultureIgnoreCase) && claim.Value.Equals("Delete", StringComparison.InvariantCultureIgnoreCase));
                });
            });

            return options;
        }

        public static IApplicationBuilder UseDefaultModularContentAdmin(this IApplicationBuilder app)
        {

            app.UseEndpoints(endpoints =>
            {
            });

            return app;
        }
    }
}

using System;
using Jlw.Data.LocalizedContent;
using Jlw.Utilities.Data.DbUtility;
using Microsoft.Extensions.Options;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class LocalizedGroupDataItemExtensions
    {
        public static IServiceCollection AddLocalizedGroupDataItemRepository(this IServiceCollection services, Action<LocalizedGroupDataItemRepositoryOptions> setupAction = null)
        {
            if (setupAction != null)
                services.Configure(setupAction);

            services.AddSingleton<ILocalizedGroupDataItemRepository>(provider =>
            {
                var options = provider.GetService<IOptions<LocalizedGroupDataItemRepositoryOptions>>() ?? new OptionsWrapper<LocalizedGroupDataItemRepositoryOptions>(provider.GetRequiredService<LocalizedGroupDataItemRepositoryOptions>());

                var dbClient = options?.Value?.DbClient ?? provider.GetRequiredService<IModularDbClient>();
                var connString = options?.Value?.ConnectionString ?? "";
                return new LocalizedGroupDataItemRepository(dbClient, connString);
            });

            return services;
        }
    }
}
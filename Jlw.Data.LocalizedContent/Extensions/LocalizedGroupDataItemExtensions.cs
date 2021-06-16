// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-20-2021
//
// Last Modified By : jlwalker
// Last Modified On : 06-15-2021
// ***********************************************************************
// <copyright file="LocalizedGroupDataItemExtensions.cs" company="Jason L. Walker">
//     Copyright ©2012-2021 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Jlw.Data.LocalizedContent;
using Jlw.Utilities.Data.DbUtility;
using Microsoft.Extensions.Options;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Class LocalizedGroupDataItemExtensions.
    /// </summary>
    /// TODO Edit XML Comment Template for LocalizedGroupDataItemExtensions
    public static class LocalizedGroupDataItemExtensions
    {
        /// <summary>
        /// Adds the localized group data item repository.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="setupAction">The setup action.</param>
        /// <returns>IServiceCollection.</returns>
        /// TODO Edit XML Comment Template for AddLocalizedGroupDataItemRepository
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
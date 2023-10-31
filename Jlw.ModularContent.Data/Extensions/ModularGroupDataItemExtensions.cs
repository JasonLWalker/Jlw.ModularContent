// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-20-2021
//
// Last Modified By : jlwalker
// Last Modified On : 05-15-2023
// ***********************************************************************
// <copyright file="ModularGroupDataItemExtensions.cs" company="Jason L. Walker">
//     Copyright ©2012-2023 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Jlw.Utilities.Data.DbUtility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Jlw.ModularContent
{
    /// <summary>
    /// Class ModularGroupDataItemExtensions.
    /// </summary>
    /// TODO Edit XML Comment Template for ModularGroupDataItemExtensions
    public static partial class ModularContentExtensions
    {
        /// <summary>
        /// Adds the localized group data item repository.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="setupAction">The setup action.</param>
        /// <returns>IServiceCollection.</returns>
        /// TODO Edit XML Comment Template for AddLocalizedGroupDataItemRepository
        public static IServiceCollection AddModularGroupDataItemRepository(this IServiceCollection services, Action<LocalizedGroupDataItemRepositoryOptions> setupAction = null)
        {
            if (setupAction != null) services.Configure(setupAction);

            services.TryAddSingleton<IModularGroupDataItemRepository>(provider =>
            {
                IModularDbOptions options = provider.GetService<IOptions<LocalizedGroupDataItemRepositoryOptions>>()?.Value;
                options ??= (new OptionsWrapper<LocalizedGroupDataItemRepositoryOptions>(provider.GetService<LocalizedGroupDataItemRepositoryOptions>()))?.Value;
                options ??= provider.GetService<IModularDbOptions>();
                options ??= provider.GetService<ModularDbOptions>();

                var dbClient = options?.DbClient ?? provider.GetRequiredService<IModularDbClient>();
                var connString = options?.ConnectionString ?? "";
                return new LocalizedGroupDataItemRepository(dbClient, connString);
            });

            return services;
        }
    }
}
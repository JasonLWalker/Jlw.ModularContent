// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-20-2021
//
// Last Modified By : jlwalker
// Last Modified On : 05-15-2023
// ***********************************************************************
// <copyright file="LocalizedContentFieldExtensions.cs" company="Jason L. Walker">
//     Copyright �2012-2023 Jason L. Walker
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
    /// Class LocalizedContentFieldExtensions.
    /// </summary>
    /// TODO Edit XML Comment Template for LocalizedContentFieldExtensions
    public static partial class ModularContentExtensions
    {
        public const string AreaName = "ModularContent";

        /// <summary>
        /// Adds the localized content field repository to the service collection as a singleton instance.
        /// </summary>
        /// <param name="services">Service collection instance that this extension will act upon</param>
        /// <param name="setupAction">The setup action options used to initialize the repository singleton.</param>
        /// <returns>Returns the <paramref name="services">services</paramref> service collection to allow for method chaining.<br /></returns>
        public static IServiceCollection AddModularContentFieldRepository(this IServiceCollection services, Action<IModularDbOptions> setupAction = null)
        {
            if (setupAction != null) services.Configure(setupAction);

            services.TryAddSingleton<IModularContentFieldRepository>(provider =>
            {
                var options = (provider.GetService<IOptions<ModularDbOptions>>() ?? new OptionsWrapper<ModularDbOptions>(provider.GetRequiredService<ModularDbOptions>())).Value;
                var client = options?.DbClient ?? provider.GetRequiredService<IModularDbClient>();
                var connString = options?.ConnectionString ?? "";
                return new ModularContentFieldRepository(client, connString);
            });

            return services;
        }
    } 
} 

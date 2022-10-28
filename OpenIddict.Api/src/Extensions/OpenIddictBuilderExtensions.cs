using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.EntityFrameworkCore.Models;
using SharpGrip.OpenIddict.Api.Authorization;
using SharpGrip.OpenIddict.Api.Controllers;
using SharpGrip.OpenIddict.Api.Mapping;
using SharpGrip.OpenIddict.Api.Options;
using SharpGrip.OpenIddict.Api.Routing;
using SharpGrip.OpenIddict.Api.Utilities;

namespace SharpGrip.OpenIddict.Api.Extensions
{
    public static class OpenIddictBuilderExtensions
    {
        public static OpenIddictEntityFrameworkCoreBuilder AddApi<TKey>(this OpenIddictEntityFrameworkCoreBuilder openIddictEntityFrameworkCoreBuilder) where TKey : struct, IEquatable<TKey>
        {
            return openIddictEntityFrameworkCoreBuilder
                .AddApi<OpenIddictEntityFrameworkCoreApplication<TKey>, OpenIddictEntityFrameworkCoreAuthorization<TKey>, OpenIddictEntityFrameworkCoreScope<TKey>, OpenIddictEntityFrameworkCoreToken<TKey>, TKey>();
        }

        public static OpenIddictEntityFrameworkCoreBuilder AddApi<TApplication, TAuthorization, TScope, TToken, TKey>(this OpenIddictEntityFrameworkCoreBuilder openIddictEntityFrameworkCoreBuilder,
            Action<OpenIddictApiOptions>? openIddictApiOptions = null)
            where TApplication : OpenIddictEntityFrameworkCoreApplication<TKey, TAuthorization, TToken>
            where TAuthorization : OpenIddictEntityFrameworkCoreAuthorization<TKey, TApplication, TToken>
            where TScope : OpenIddictEntityFrameworkCoreScope<TKey>
            where TToken : OpenIddictEntityFrameworkCoreToken<TKey, TApplication, TAuthorization>
            where TKey : struct, IEquatable<TKey>
        {
            var defaultOpenIddictApiOptions = new OpenIddictApiOptions();

            if (openIddictApiOptions != null)
            {
                openIddictApiOptions.Invoke(defaultOpenIddictApiOptions);
                openIddictEntityFrameworkCoreBuilder.Services.Configure(openIddictApiOptions);
            }

            openIddictEntityFrameworkCoreBuilder.Services.AddSingleton<Mapper<TApplication, TAuthorization, TScope, TToken, TKey>, Mapper<TApplication, TAuthorization, TScope, TToken, TKey>>();
            openIddictEntityFrameworkCoreBuilder.Services.AddScoped<ModelValidator, ModelValidator>();
            openIddictEntityFrameworkCoreBuilder.AddApiControllers<TApplication, TAuthorization, TScope, TToken, TKey>(defaultOpenIddictApiOptions);

            return openIddictEntityFrameworkCoreBuilder;
        }

        private static void AddApiControllers<TApplication, TAuthorization, TScope, TToken, TKey>(this OpenIddictEntityFrameworkCoreBuilder openIddictEntityFrameworkCoreBuilder,
            OpenIddictApiOptions openIddictApiOptions)
            where TApplication : OpenIddictEntityFrameworkCoreApplication<TKey, TAuthorization, TToken>
            where TAuthorization : OpenIddictEntityFrameworkCoreAuthorization<TKey, TApplication, TToken>
            where TScope : OpenIddictEntityFrameworkCoreScope<TKey>
            where TToken : OpenIddictEntityFrameworkCoreToken<TKey, TApplication, TAuthorization>
            where TKey : struct, IEquatable<TKey>
        {
            openIddictEntityFrameworkCoreBuilder.Services.AddMvcCore().AddApplicationPart(typeof(Api).Assembly).ConfigureApplicationPartManager(applicationPartManager =>
            {
                applicationPartManager.FeatureProviders.Add(new ControllerFeatureProvider<TApplication, TAuthorization, TScope, TToken, TKey>());
            });
            openIddictEntityFrameworkCoreBuilder.Services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add<ApiAccessFilter<TApplication, TAuthorization, TScope, TToken, TKey>>();
                options.Conventions.Insert(0, new RoutePrefixConvention(
                    openIddictApiOptions,
                    new Dictionary<string, Type>
                    {
                        {openIddictApiOptions.ApplicationApiRoute, typeof(ApplicationController<TApplication, TAuthorization, TScope, TToken, TKey>)},
                        {openIddictApiOptions.AuthorizationApiRoute, typeof(AuthorizationController<TApplication, TAuthorization, TScope, TToken, TKey>)},
                        {openIddictApiOptions.ScopeApiRoute, typeof(ScopeController<TApplication, TAuthorization, TScope, TToken, TKey>)},
                        {openIddictApiOptions.TokenApiRoute, typeof(TokenController<TApplication, TAuthorization, TScope, TToken, TKey>)}
                    }
                ));
            });
        }
    }
}
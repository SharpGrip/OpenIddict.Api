using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using OpenIddict.EntityFrameworkCore.Models;
using SharpGrip.OpenIddict.Api.Controllers;
using SharpGrip.OpenIddict.Api.Options;

namespace SharpGrip.OpenIddict.Api.Authorization
{
    public class ApiAccessFilter<TApplication, TAuthorization, TScope, TToken, TKey> : IAsyncActionFilter
        where TApplication : OpenIddictEntityFrameworkCoreApplication<TKey, TAuthorization, TToken>
        where TAuthorization : OpenIddictEntityFrameworkCoreAuthorization<TKey, TApplication, TToken>
        where TScope : OpenIddictEntityFrameworkCoreScope<TKey>
        where TToken : OpenIddictEntityFrameworkCoreToken<TKey, TApplication, TAuthorization>
        where TKey : struct, IEquatable<TKey>
    {
        private readonly OpenIddictApiConfiguration openIddictApiOptions;

        public ApiAccessFilter(IOptions<OpenIddictApiConfiguration> openIddictApiOptions)
        {
            this.openIddictApiOptions = openIddictApiOptions.Value;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.Controller is BaseController<TApplication, TAuthorization, TScope, TToken, TKey>)
            {
                var scope = context.Controller switch
                {
                    ApplicationController<TApplication, TAuthorization, TScope, TToken, TKey> _ => openIddictApiOptions.ApplicationApiAccessScope,
                    AuthorizationController<TApplication, TAuthorization, TScope, TToken, TKey> _ => openIddictApiOptions.AuthorizationApiAccessScope,
                    ScopeController<TApplication, TAuthorization, TScope, TToken, TKey> _ => openIddictApiOptions.ScopeApiAccessScope,
                    TokenController<TApplication, TAuthorization, TScope, TToken, TKey> _ => openIddictApiOptions.TokenApiAccessScope,
                    _ => ""
                };

                if (!context.HttpContext.User.HasScope(scope))
                {
                    context.Result = new UnauthorizedResult();

                    return;
                }
            }

            await next();
        }
    }
}
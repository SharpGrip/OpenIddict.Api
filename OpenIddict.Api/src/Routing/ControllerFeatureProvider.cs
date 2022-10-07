using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using OpenIddict.EntityFrameworkCore.Models;
using SharpGrip.OpenIddict.Api.Controllers;

namespace SharpGrip.OpenIddict.Api.Routing
{
    public class ControllerFeatureProvider<TApplication, TAuthorization, TScope, TToken, TKey> : IApplicationFeatureProvider<ControllerFeature>
        where TApplication : OpenIddictEntityFrameworkCoreApplication<TKey, TAuthorization, TToken>
        where TAuthorization : OpenIddictEntityFrameworkCoreAuthorization<TKey, TApplication, TToken>
        where TScope : OpenIddictEntityFrameworkCoreScope<TKey>
        where TToken : OpenIddictEntityFrameworkCoreToken<TKey, TApplication, TAuthorization>
        where TKey : struct, IEquatable<TKey>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> applicationParts, ControllerFeature controllerFeature)
        {
            controllerFeature.Controllers.Add(typeof(ApplicationController<TApplication, TAuthorization, TScope, TToken, TKey>).GetTypeInfo());
            controllerFeature.Controllers.Add(typeof(AuthorizationController<TApplication, TAuthorization, TScope, TToken, TKey>).GetTypeInfo());
            controllerFeature.Controllers.Add(typeof(ScopeController<TApplication, TAuthorization, TScope, TToken, TKey>).GetTypeInfo());
            controllerFeature.Controllers.Add(typeof(TokenController<TApplication, TAuthorization, TScope, TToken, TKey>).GetTypeInfo());
        }
    }
}
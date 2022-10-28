using System;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.EntityFrameworkCore.Models;
using SharpGrip.OpenIddict.Api.Mapping;
using SharpGrip.OpenIddict.Api.Utilities;

namespace SharpGrip.OpenIddict.Api.Controllers
{
    public abstract class BaseController<TApplication, TAuthorization, TScope, TToken, TKey> : ControllerBase
        where TApplication : OpenIddictEntityFrameworkCoreApplication<TKey, TAuthorization, TToken>
        where TAuthorization : OpenIddictEntityFrameworkCoreAuthorization<TKey, TApplication, TToken>
        where TScope : OpenIddictEntityFrameworkCoreScope<TKey>
        where TToken : OpenIddictEntityFrameworkCoreToken<TKey, TApplication, TAuthorization>
        where TKey : struct, IEquatable<TKey>
    {
        protected readonly Mapper<TApplication, TAuthorization, TScope, TToken, TKey> Mapper;
        protected readonly ModelValidator ModelValidator;

        protected BaseController(Mapper<TApplication, TAuthorization, TScope, TToken, TKey> mapper, ModelValidator modelValidator)
        {
            Mapper = mapper;
            ModelValidator = modelValidator;
        }
    }
}
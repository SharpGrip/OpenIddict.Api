using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;
using OpenIddict.Validation.AspNetCore;
using SharpGrip.OpenIddict.Api.Mapping;
using SharpGrip.OpenIddict.Api.Utilities;

namespace SharpGrip.OpenIddict.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public class AuthorizationController<TApplication, TAuthorization, TScope, TToken, TKey> : BaseController<TApplication, TAuthorization, TScope, TToken, TKey>
        where TApplication : OpenIddictEntityFrameworkCoreApplication<TKey, TAuthorization, TToken>
        where TAuthorization : OpenIddictEntityFrameworkCoreAuthorization<TKey, TApplication, TToken>
        where TScope : OpenIddictEntityFrameworkCoreScope<TKey>
        where TToken : OpenIddictEntityFrameworkCoreToken<TKey, TApplication, TAuthorization>
        where TKey : struct, IEquatable<TKey>
    {
        private readonly OpenIddictAuthorizationManager<TAuthorization> openIddictAuthorizationManager;

        public AuthorizationController(Mapper<TApplication, TAuthorization, TScope, TToken, TKey> mapper, ModelValidator modelValidator, OpenIddictAuthorizationManager<TAuthorization> openIddictAuthorizationManager) 
            : base(mapper, modelValidator)
        {
            this.openIddictAuthorizationManager = openIddictAuthorizationManager;
        }

        [HttpGet("")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Get()
        {
            var authorizations = new List<TAuthorization>();

            await foreach (var authorization in openIddictAuthorizationManager.ListAsync())
            {
                authorizations.Add(authorization);
            }

            var authorizationViewModels = Mapper.Map(authorizations);

            return Ok(authorizationViewModels);
        }

        [HttpGet("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Get(string id)
        {
            var authorization = await openIddictAuthorizationManager.FindByIdAsync(id);

            if (authorization == null)
            {
                return NotFound();
            }

            var authorizationViewModel = Mapper.Map(authorization);

            return Ok(authorizationViewModel);
        }
    }
}
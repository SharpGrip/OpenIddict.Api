using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;
using OpenIddict.Validation.AspNetCore;
using SharpGrip.OpenIddict.Api.Mapping;

namespace SharpGrip.OpenIddict.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public class TokenController<TApplication, TAuthorization, TScope, TToken, TKey> : BaseController<TApplication, TAuthorization, TScope, TToken, TKey>
        where TApplication : OpenIddictEntityFrameworkCoreApplication<TKey, TAuthorization, TToken>
        where TAuthorization : OpenIddictEntityFrameworkCoreAuthorization<TKey, TApplication, TToken>
        where TScope : OpenIddictEntityFrameworkCoreScope<TKey>
        where TToken : OpenIddictEntityFrameworkCoreToken<TKey, TApplication, TAuthorization>
        where TKey : struct, IEquatable<TKey>
    {
        private readonly OpenIddictTokenManager<TToken> openIddictTokenManager;

        public TokenController(Mapper<TApplication, TAuthorization, TScope, TToken, TKey> mapper, OpenIddictTokenManager<TToken> openIddictTokenManager) : base(mapper)
        {
            this.openIddictTokenManager = openIddictTokenManager;
        }

        [HttpGet("")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Get()
        {
            var tokens = new List<TToken>();

            await foreach (var token in openIddictTokenManager.ListAsync())
            {
                tokens.Add(token);
            }

            var tokenViewModels = Mapper.Map(tokens);

            return Ok(tokenViewModels);
        }

        [HttpGet("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Get(string id)
        {
            var token = await openIddictTokenManager.FindByIdAsync(id);

            if (token == null)
            {
                return NotFound();
            }

            return Ok(token);
        }
    }
}
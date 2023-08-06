using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;
using OpenIddict.Validation.AspNetCore;
using SharpGrip.OpenIddict.Api.Mapping;
using SharpGrip.OpenIddict.Api.Models.Scope;
using SharpGrip.OpenIddict.Api.Utilities;

namespace SharpGrip.OpenIddict.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public class ScopeController<TApplication, TAuthorization, TScope, TToken, TKey> : BaseController<TApplication, TAuthorization, TScope, TToken, TKey>
        where TApplication : OpenIddictEntityFrameworkCoreApplication<TKey, TAuthorization, TToken>
        where TAuthorization : OpenIddictEntityFrameworkCoreAuthorization<TKey, TApplication, TToken>
        where TScope : OpenIddictEntityFrameworkCoreScope<TKey>
        where TToken : OpenIddictEntityFrameworkCoreToken<TKey, TApplication, TAuthorization>
        where TKey : struct, IEquatable<TKey>
    {
        private readonly OpenIddictScopeManager<TScope> openIddictScopeManager;

        public ScopeController(Mapper<TApplication, TAuthorization, TScope, TToken, TKey> mapper, ModelValidator modelValidator, OpenIddictScopeManager<TScope> openIddictScopeManager)
            : base(mapper, modelValidator)
        {
            this.openIddictScopeManager = openIddictScopeManager;
        }

        [HttpGet("")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Get()
        {
            var scopes = new List<TScope>();

            await foreach (var scope in openIddictScopeManager.ListAsync())
            {
                scopes.Add(scope);
            }

            var scopeViewModels = Mapper.Map(scopes);

            return Ok(scopeViewModels);
        }

        [HttpGet("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Get(string id)
        {
            var scope = await openIddictScopeManager.FindByIdAsync(id);

            if (scope == null)
            {
                return NotFound();
            }

            var scopeViewModel = Mapper.Map(scope);

            return Ok(scopeViewModel);
        }

        [HttpPost("")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Create(ScopeCreateModel scopeCreateModel)
        {
            var validationResult = await ModelValidator.Validate(scopeCreateModel);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                var openIddictScopeDescriptor = Mapper.Map(scopeCreateModel);
                var scope = await openIddictScopeManager.CreateAsync(openIddictScopeDescriptor);
                var scopeViewModel = Mapper.Map(scope);

                return Ok(scopeViewModel);
            }
            catch (OpenIddictExceptions.ValidationException validationException)
            {
                return BadRequest(validationException.Results);
            }
        }

        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Update(TKey id, ScopeUpdateModel scopeUpdateModel)
        {
            var scope = await openIddictScopeManager.FindByIdAsync(id.ToString()!);

            if (scope == null)
            {
                return NotFound();
            }

            var validationResult = await ModelValidator.Validate(scopeUpdateModel);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                var openIddictScopeDescriptor = Mapper.Map(scopeUpdateModel);
                await openIddictScopeManager.UpdateAsync(scope, openIddictScopeDescriptor);
                var scopeViewModel = Mapper.Map(scope);

                return Ok(scopeViewModel);
            }
            catch (OpenIddictExceptions.ValidationException validationException)
            {
                return BadRequest(validationException.Results);
            }
        }

        [HttpPost("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Delete(TKey id)
        {
            var scope = await openIddictScopeManager.FindByIdAsync(id.ToString()!);

            if (scope == null)
            {
                return NotFound();
            }

            await openIddictScopeManager.DeleteAsync(scope);

            return Ok();
        }
    }
}
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
using SharpGrip.OpenIddict.Api.Models.Application;

namespace SharpGrip.OpenIddict.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public class ApplicationController<TApplication, TAuthorization, TScope, TToken, TKey> : BaseController<TApplication, TAuthorization, TScope, TToken, TKey>
        where TApplication : OpenIddictEntityFrameworkCoreApplication<TKey, TAuthorization, TToken>
        where TAuthorization : OpenIddictEntityFrameworkCoreAuthorization<TKey, TApplication, TToken>
        where TScope : OpenIddictEntityFrameworkCoreScope<TKey>
        where TToken : OpenIddictEntityFrameworkCoreToken<TKey, TApplication, TAuthorization>
        where TKey : struct, IEquatable<TKey>
    {
        private readonly OpenIddictApplicationManager<TApplication> openIddictApplicationManager;

        public ApplicationController(Mapper<TApplication, TAuthorization, TScope, TToken, TKey> mapper, OpenIddictApplicationManager<TApplication> openIddictApplicationManager) : base(mapper)
        {
            this.openIddictApplicationManager = openIddictApplicationManager;
        }

        [HttpGet("")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Get()
        {
            var applications = new List<TApplication>();

            await foreach (var application in openIddictApplicationManager.ListAsync())
            {
                applications.Add(application);
            }

            var applicationViewModels = Mapper.Map(applications);

            return Ok(applicationViewModels);
        }

        [HttpGet("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Get(string id)
        {
            var application = await openIddictApplicationManager.FindByIdAsync(id);

            if (application == null)
            {
                return NotFound();
            }

            var applicationViewModel = Mapper.Map(application);

            return Ok(applicationViewModel);
        }

        [HttpPost("")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Create(ApplicationCreateModel applicationCreateModel)
        {
            try
            {
                var openIddictApplicationDescriptor = Mapper.Map(applicationCreateModel);
                var application = await openIddictApplicationManager.CreateAsync(openIddictApplicationDescriptor);
                var applicationViewModel = Mapper.Map(application);

                return Ok(applicationViewModel);
            }
            catch (OpenIddictExceptions.ValidationException validationException)
            {
                return BadRequest(validationException.Results);
            }
        }

        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Update(TKey id, ApplicationUpdateModel applicationUpdateModel)
        {
            var application = await openIddictApplicationManager.FindByIdAsync(id.ToString()!);

            if (application == null)
            {
                return NotFound();
            }

            try
            {
                application = Mapper.Map(applicationUpdateModel, application);
                await openIddictApplicationManager.UpdateAsync(application);
                var applicationViewModel = Mapper.Map(application);

                return Ok(applicationViewModel);
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
            var application = await openIddictApplicationManager.FindByIdAsync(id.ToString()!);

            if (application == null)
            {
                return NotFound();
            }

            await openIddictApplicationManager.DeleteAsync(application);

            return Ok();
        }
    }
}
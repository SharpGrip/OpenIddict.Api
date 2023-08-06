using SharpGrip.OpenIddict.Api.Models.Constants;

namespace SharpGrip.OpenIddict.Api.Options
{
    /// <summary>
    /// Exposes the configuration options to customize the OpenIddict API.
    /// </summary>
    public class OpenIddictApiConfiguration
    {
        /// <summary>
        /// The API route prefix prefixes all API routes present. Defaults to the value of <see cref="OpenIddictApiConstants.Api.ApiRoutePrefix"/>.
        /// </summary>
        /// <see cref="OpenIddictApiConstants.Api"/>
        public string ApiRoutePrefix { get; set; } = OpenIddictApiConstants.Api.ApiRoutePrefix;

        /// <summary>
        /// The base route for the <c>Application</c> endpoints. Defaults to the value of <see cref="OpenIddictApiConstants.Api.ApplicationApiRoute"/>.
        /// </summary>
        /// <see cref="OpenIddictApiConstants.Api"/>
        public string ApplicationApiRoute { get; set; } = OpenIddictApiConstants.Api.ApplicationApiRoute;

        /// <summary>
        /// The access scope required to be present in the claims principal in order to access the <c>Application</c> endpoints. Default to <see cref="OpenIddictApiConstants.Api.ApplicationApiAccessScope"/>.
        /// </summary>
        /// <see cref="OpenIddictApiConstants.Api"/>
        public string ApplicationApiAccessScope { get; set; } = OpenIddictApiConstants.Api.ApplicationApiAccessScope;

        /// <summary>
        /// The base route for the <c>Authorization</c> endpoints. Defaults to the value of <see cref="OpenIddictApiConstants.Api.AuthorizationApiRoute"/>.
        /// </summary>
        /// <see cref="OpenIddictApiConstants.Api"/>
        public string AuthorizationApiRoute { get; set; } = OpenIddictApiConstants.Api.AuthorizationApiRoute;

        /// <summary>
        /// The access scope required to be present in the claims principal in order to access the <c>Authorization</c> endpoints. Default to <see cref="OpenIddictApiConstants.Api.AuthorizationApiAccessScope"/>.
        /// </summary>
        /// <see cref="OpenIddictApiConstants.Api"/>
        public string AuthorizationApiAccessScope { get; set; } = OpenIddictApiConstants.Api.AuthorizationApiAccessScope;

        /// <summary>
        /// The base route for the <c>Scope</c> endpoints. Defaults to the value of <see cref="OpenIddictApiConstants.Api.ScopeApiRoute"/>.
        /// </summary>
        /// <see cref="OpenIddictApiConstants.Api"/>
        public string ScopeApiRoute { get; set; } = OpenIddictApiConstants.Api.ScopeApiRoute;

        /// <summary>
        /// The access scope required to be present in the claims principal in order to access the <c>Scope</c> endpoints. Default to <see cref="OpenIddictApiConstants.Api.ScopeApiAccessScope"/>. 
        /// </summary>
        /// <see cref="OpenIddictApiConstants.Api"/>
        public string ScopeApiAccessScope { get; set; } = OpenIddictApiConstants.Api.ScopeApiAccessScope;

        /// <summary>
        /// The base route for the <c>Token</c> endpoints. Defaults to the value of <see cref="OpenIddictApiConstants.Api.TokenApiRoute"/>.
        /// </summary>
        /// <see cref="OpenIddictApiConstants.Api"/>
        public string TokenApiRoute { get; set; } = OpenIddictApiConstants.Api.TokenApiRoute;

        /// <summary>
        /// The access scope required to be present in the claims principal in order to access the <c>Token</c> endpoints. Default to <see cref="OpenIddictApiConstants.Api.TokenApiAccessScope"/>. 
        /// </summary>
        /// <see cref="OpenIddictApiConstants.Api"/>
        public string TokenApiAccessScope { get; set; } = OpenIddictApiConstants.Api.TokenApiAccessScope;
    }
}
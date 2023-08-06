using System.Collections.Generic;
using OpenIddict.Abstractions;

namespace SharpGrip.OpenIddict.Api.Models.Constants
{
    public static class OpenIddictApiConstants
    {
        public static class Application
        {
            public static IDictionary<string, string> ClientTypes => new Dictionary<string, string>
            {
                {OpenIddictConstants.ClientTypes.Confidential, "Confidential"},
                {OpenIddictConstants.ClientTypes.Public, "Public"}
            };

            public static IDictionary<string, string> ConsentTypes => new Dictionary<string, string>
            {
                {OpenIddictConstants.ConsentTypes.Explicit, "Explicit"},
                {OpenIddictConstants.ConsentTypes.External, "External"},
                {OpenIddictConstants.ConsentTypes.Implicit, "Implicit"},
                {OpenIddictConstants.ConsentTypes.Systematic, "Systematic"}
            };

            public static class Permissions
            {
                public static IDictionary<string, string> Endpoints => new Dictionary<string, string>
                {
                    {OpenIddictConstants.Permissions.Endpoints.Authorization, "Authorization"},
                    {OpenIddictConstants.Permissions.Endpoints.Device, "Device"},
                    {OpenIddictConstants.Permissions.Endpoints.Introspection, "Introspection"},
                    {OpenIddictConstants.Permissions.Endpoints.Logout, "Logout"},
                    {OpenIddictConstants.Permissions.Endpoints.Revocation, "Revocation"},
                    {OpenIddictConstants.Permissions.Endpoints.Token, "Token"}
                };

                public static IDictionary<string, string> GrantTypes => new Dictionary<string, string>
                {
                    {OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode, "Authorization code"},
                    {OpenIddictConstants.Permissions.GrantTypes.ClientCredentials, "Client credentials"},
                    {OpenIddictConstants.Permissions.GrantTypes.DeviceCode, "Device code"},
                    {OpenIddictConstants.Permissions.GrantTypes.Implicit, "Implicit"},
                    {OpenIddictConstants.Permissions.GrantTypes.Password, "Password"},
                    {OpenIddictConstants.Permissions.GrantTypes.RefreshToken, "Refresh token"}
                };

                public static IDictionary<string, string> ResponseTypes => new Dictionary<string, string>
                {
                    {OpenIddictConstants.Permissions.ResponseTypes.Code, "Code"},
                    {OpenIddictConstants.Permissions.ResponseTypes.CodeIdToken, "Code + ID token"},
                    {OpenIddictConstants.Permissions.ResponseTypes.CodeIdTokenToken, "Code + ID token + token"},
                    {OpenIddictConstants.Permissions.ResponseTypes.CodeToken, "Code + token"},
                    {OpenIddictConstants.Permissions.ResponseTypes.IdToken, "ID token"},
                    {OpenIddictConstants.Permissions.ResponseTypes.IdTokenToken, "ID token + token"},
                    {OpenIddictConstants.Permissions.ResponseTypes.None, "None"},
                    {OpenIddictConstants.Permissions.ResponseTypes.Token, "Token"}
                };

                public static IDictionary<string, string> Scopes => new Dictionary<string, string>
                {
                    {OpenIddictConstants.Permissions.Scopes.Address, "Address"},
                    {OpenIddictConstants.Permissions.Scopes.Email, "Email"},
                    {OpenIddictConstants.Permissions.Scopes.Phone, "Phone"},
                    {OpenIddictConstants.Permissions.Scopes.Profile, "Profile"},
                    {OpenIddictConstants.Permissions.Scopes.Roles, "Roles"}
                };
            }

            public static class Requirements
            {
                public static IDictionary<string, string> Features => new Dictionary<string, string>
                {
                    {OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange, "Proof Key for Code Exchange (PKCE)"}
                };
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using OpenIddict.Abstractions;
using OpenIddict.EntityFrameworkCore.Models;
using SharpGrip.OpenIddict.Api.Models.Application;
using SharpGrip.OpenIddict.Api.Models.Authorization;
using SharpGrip.OpenIddict.Api.Models.Constants;
using SharpGrip.OpenIddict.Api.Models.Scope;
using SharpGrip.OpenIddict.Api.Models.Token;

namespace SharpGrip.OpenIddict.Api.Mapping
{
    public class Mapper<TApplication, TAuthorization, TScope, TToken, TKey>
        where TApplication : OpenIddictEntityFrameworkCoreApplication<TKey, TAuthorization, TToken>
        where TAuthorization : OpenIddictEntityFrameworkCoreAuthorization<TKey, TApplication, TToken>
        where TScope : OpenIddictEntityFrameworkCoreScope<TKey>
        where TToken : OpenIddictEntityFrameworkCoreToken<TKey, TApplication, TAuthorization>
        where TKey : struct, IEquatable<TKey>
    {
        public ApplicationViewModel<TKey> Map(TApplication application)
        {
            var applicationViewModel = new ApplicationViewModel<TKey>
            {
                Id = application.Id,
                DisplayName = application.DisplayName,
                Type = application.Type,
                ConsentType = application.ConsentType,
                ClientId = application.ClientId,
                PostLogoutRedirectUris = JsonSerializer.Deserialize<List<string>>(application.PostLogoutRedirectUris ?? "[]")!,
                RedirectUris = JsonSerializer.Deserialize<List<string>>(application.RedirectUris ?? "[]")!
            };

            foreach (var permission in JsonSerializer.Deserialize<string[]>(application.Permissions ?? "[]"))
            {
                if (OpenIddictApiConstants.Application.Permissions.Endpoints.Keys.Contains(permission))
                {
                    applicationViewModel.EndpointPermissions.Add(permission);

                    continue;
                }

                if (OpenIddictApiConstants.Application.Permissions.GrantTypes.Keys.Contains(permission))
                {
                    applicationViewModel.GrantTypePermissions.Add(permission);

                    continue;
                }

                if (OpenIddictApiConstants.Application.Permissions.ResponseTypes.Keys.Contains(permission))
                {
                    applicationViewModel.ResponseTypesPermissions.Add(permission);

                    continue;
                }

                if (OpenIddictApiConstants.Application.Permissions.Scopes.Keys.Contains(permission))
                {
                    applicationViewModel.ScopePermissions.Add(permission);

                    continue;
                }

                applicationViewModel.CustomScopePermissions.Add(permission);
            }

            foreach (var requirement in JsonSerializer.Deserialize<string[]>(application.Requirements ?? "[]"))
            {
                if (requirement.StartsWith(OpenIddictConstants.Requirements.Prefixes.Feature))
                {
                    applicationViewModel.FeatureRequirements.Add(requirement);
                }
            }

            return applicationViewModel;
        }

        public IEnumerable<ApplicationViewModel<TKey>> Map(IEnumerable<TApplication> applications)
        {
            return applications.Select(Map);
        }

        public OpenIddictApplicationDescriptor Map(IApplicationWriteModel applicationWriteModel)
        {
            var openIddictApplicationDescriptor = new OpenIddictApplicationDescriptor
            {
                DisplayName = applicationWriteModel.DisplayName,
                Type = applicationWriteModel.Type,
                ConsentType = applicationWriteModel.ConsentType,
                ClientId = applicationWriteModel.ClientId,
                ClientSecret = applicationWriteModel.ClientSecret
            };

            foreach (var postLogoutRedirectUri in applicationWriteModel.PostLogoutRedirectUris)
            {
                openIddictApplicationDescriptor.PostLogoutRedirectUris.Add(new Uri(postLogoutRedirectUri));
            }

            foreach (var redirectUri in applicationWriteModel.RedirectUris)
            {
                openIddictApplicationDescriptor.RedirectUris.Add(new Uri(redirectUri));
            }

            foreach (var permission in applicationWriteModel.GetPermissions())
            {
                openIddictApplicationDescriptor.Permissions.Add(permission);
            }

            foreach (var requirement in applicationWriteModel.FeatureRequirements)
            {
                openIddictApplicationDescriptor.Requirements.Add(requirement);
            }

            return openIddictApplicationDescriptor;
        }

        public AuthorizationViewModel<TKey> Map(TAuthorization authorization)
        {
            return new AuthorizationViewModel<TKey>
            {
                Id = authorization.Id,
                Subject = authorization.Subject,
                Type = authorization.Type,
                Status = authorization.Status,
                ConcurrencyToken = authorization.ConcurrencyToken,
                CreationDate = authorization.CreationDate,
                Scopes = JsonSerializer.Deserialize<string[]>(authorization.Scopes ?? "[]")!,
                Properties = JsonSerializer.Deserialize<string[]>(authorization.Properties ?? "[]")!,
                ApplicationId = authorization.Application?.Id ?? default
            };
        }

        public IEnumerable<AuthorizationViewModel<TKey>> Map(IEnumerable<TAuthorization> authorizations)
        {
            return authorizations.Select(Map);
        }

        public ScopeViewModel<TKey> Map(TScope scope)
        {
            return new ScopeViewModel<TKey>
            {
                Id = scope.Id,
                Name = scope.Name,
                DisplayName = scope.DisplayName,
                Description = scope.Description,
                ConcurrencyToken = scope.ConcurrencyToken,
                Resources = JsonSerializer.Deserialize<List<string>>(scope.Resources ?? "[]")!
            };
        }

        public ICollection<ScopeViewModel<TKey>> Map(IEnumerable<TScope> scopes)
        {
            return scopes.Select(Map).ToList();
        }

        public OpenIddictScopeDescriptor Map(IScopeWriteModel scopeWriteModel)
        {
            var openIddictScopeDescriptor = new OpenIddictScopeDescriptor
            {
                Name = scopeWriteModel.Name,
                DisplayName = scopeWriteModel.DisplayName,
                Description = scopeWriteModel.Description
            };

            foreach (var resource in scopeWriteModel.Resources)
            {
                openIddictScopeDescriptor.Resources.Add(resource);
            }

            return openIddictScopeDescriptor;
        }

        public TokenViewModel<TKey> Map(TToken token)
        {
            return new TokenViewModel<TKey>
            {
                Id = token.Id,
                Subject = token.Subject,
                Type = token.Type,
                Status = token.Status,
                Payload = token.Payload,
                ReferenceId = token.ReferenceId,
                ConcurrencyToken = token.ConcurrencyToken,
                CreationDate = token.CreationDate,
                ExpirationDate = token.ExpirationDate,
                RedemptionDate = token.RedemptionDate,
                Properties = JsonSerializer.Deserialize<string[]>(token.Properties ?? "[]")!,
                ApplicationId = token.Application?.Id ?? default,
                AuthorizationId = token.Authorization?.Id ?? default,
            };
        }

        public ICollection<TokenViewModel<TKey>> Map(IEnumerable<TToken> tokens)
        {
            return tokens.Select(Map).ToList();
        }
    }
}
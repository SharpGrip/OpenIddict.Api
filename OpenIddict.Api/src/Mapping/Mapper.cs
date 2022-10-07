using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using OpenIddict.Abstractions;
using OpenIddict.EntityFrameworkCore.Models;
using SharpGrip.OpenIddict.Api.Models.Application;
using SharpGrip.OpenIddict.Api.Models.Authorization;
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
            return new ApplicationViewModel<TKey>
            {
                Id = application.Id,
                DisplayName = application.DisplayName,
                Type = application.Type,
                ConsentType = application.ConsentType,
                ClientId = application.ClientId,
                ClientSecret = application.ClientSecret,
                Permissions = JsonSerializer.Deserialize<string[]>(application.Permissions ?? "[]")!,
                PostLogoutRedirectUris = JsonSerializer.Deserialize<string[]>(application.PostLogoutRedirectUris ?? "[]")!,
                RedirectUris = JsonSerializer.Deserialize<string[]>(application.RedirectUris ?? "[]")!,
                Requirements = JsonSerializer.Deserialize<string[]>(application.Requirements ?? "[]")!,
                Properties = JsonSerializer.Deserialize<string[]>(application.Properties ?? "[]")!
            };
        }

        public IEnumerable<ApplicationViewModel<TKey>> Map(IEnumerable<TApplication> applications)
        {
            return applications.Select(Map);
        }

        public OpenIddictApplicationDescriptor Map(ApplicationCreateModel applicationCreateModel)
        {
            var openIddictApplicationDescriptor = new OpenIddictApplicationDescriptor
            {
                DisplayName = applicationCreateModel.DisplayName,
                Type = applicationCreateModel.Type,
                ConsentType = applicationCreateModel.ConsentType,
                ClientId = applicationCreateModel.ClientId,
                ClientSecret = applicationCreateModel.ClientSecret
            };

            foreach (var permission in applicationCreateModel.Permissions)
            {
                openIddictApplicationDescriptor.Permissions.Add(permission);
            }

            foreach (var postLogoutRedirectUri in applicationCreateModel.PostLogoutRedirectUris)
            {
                openIddictApplicationDescriptor.PostLogoutRedirectUris.Add(new Uri(postLogoutRedirectUri));
            }

            foreach (var redirectUri in applicationCreateModel.RedirectUris)
            {
                openIddictApplicationDescriptor.RedirectUris.Add(new Uri(redirectUri));
            }

            foreach (var requirement in applicationCreateModel.Requirements)
            {
                openIddictApplicationDescriptor.Requirements.Add(requirement);
            }

            return openIddictApplicationDescriptor;
        }

        public TApplication Map(ApplicationUpdateModel applicationUpdateModel, TApplication application)
        {
            application.DisplayName = applicationUpdateModel.DisplayName;
            application.Type = applicationUpdateModel.Type;
            application.ConsentType = applicationUpdateModel.ConsentType;
            application.ClientId = applicationUpdateModel.ClientId;
            application.ClientSecret = applicationUpdateModel.ClientSecret;

            return application;
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
                ApplicationId = authorization.Application != null ? authorization.Application.Id : default
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
                DisplayNames = JsonSerializer.Deserialize<string[]>(scope.DisplayNames ?? "[]")!,
                Descriptions = JsonSerializer.Deserialize<string[]>(scope.Descriptions ?? "[]")!,
                Resources = JsonSerializer.Deserialize<string[]>(scope.Resources ?? "[]")!,
                Properties = JsonSerializer.Deserialize<string[]>(scope.Properties ?? "[]")!
            };
        }

        public ICollection<ScopeViewModel<TKey>> Map(IEnumerable<TScope> scopes)
        {
            return scopes.Select(Map).ToList();
        }

        public OpenIddictScopeDescriptor Map(ScopeCreateModel scopeCreateModel)
        {
            var openIddictScopeDescriptor = new OpenIddictScopeDescriptor
            {
                Name = scopeCreateModel.Name,
                DisplayName = scopeCreateModel.DisplayName,
                Description = scopeCreateModel.Description
            };

            foreach (var resource in scopeCreateModel.Resources)
            {
                openIddictScopeDescriptor.Resources.Add(resource);
            }

            return openIddictScopeDescriptor;
        }

        public TScope Map(ScopeUpdateModel scopeUpdateModel, TScope scope)
        {
            scope.Name = scopeUpdateModel.Name;
            scope.DisplayName = scopeUpdateModel.DisplayName;
            scope.Description = scopeUpdateModel.Description;

            return scope;
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
                ApplicationId = token.Application != null ? token.Application.Id : default,
                AuthorizationId = token.Authorization != null ? token.Authorization.Id : default,
            };
        }

        public ICollection<TokenViewModel<TKey>> Map(IEnumerable<TToken> tokens)
        {
            return tokens.Select(Map).ToList();
        }
    }
}
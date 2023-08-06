using System;
using FluentValidation;
using FluentValidation.Validators;
using OpenIddict.Abstractions;
using SharpGrip.OpenIddict.Api.Models.Constants;

namespace SharpGrip.OpenIddict.Api.Validation.Extensions
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> IsRequired<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            var ruleBuilderOptions = ruleBuilder.SetValidator(new NotEmptyValidator<T, TProperty>());
            ruleBuilderOptions.WithMessage("{PropertyName} is required.");

            return ruleBuilderOptions;
        }

        public static IRuleBuilderOptions<T, string?> IsValidClientType<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            var ruleBuilderOptions = ruleBuilder.Must(property => property != null && OpenIddictApiConstants.Application.ClientTypes.ContainsKey(property));
            ruleBuilderOptions.WithMessage($"Value '{{PropertyValue}}' is not a valid client type. Valid values are: {string.Join(", ", OpenIddictApiConstants.Application.ClientTypes.Keys)}.");

            return ruleBuilderOptions;
        }

        public static IRuleBuilderOptions<T, string?> IsValidConsentType<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            var ruleBuilderOptions = ruleBuilder.Must(property => property != null && OpenIddictApiConstants.Application.ConsentTypes.ContainsKey(property));
            ruleBuilderOptions.WithMessage($"Value '{{PropertyValue}}' is not a valid content type. Valid values are: {string.Join(", ", OpenIddictApiConstants.Application.ConsentTypes.Keys)}.");

            return ruleBuilderOptions;
        }

        public static IRuleBuilderOptions<T, string?> IsValidEndpointPermission<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            var ruleBuilderOptions = ruleBuilder.Must(property => property != null && OpenIddictApiConstants.Application.Permissions.Endpoints.ContainsKey(property));
            ruleBuilderOptions.WithMessage($"Value '{{PropertyValue}}' is not a valid endpoint permission. Valid values are: {string.Join(", ", OpenIddictApiConstants.Application.Permissions.Endpoints.Keys)}.");

            return ruleBuilderOptions;
        }

        public static IRuleBuilderOptions<T, string?> IsValidGrantTypePermission<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            var ruleBuilderOptions = ruleBuilder.Must(property => property != null && OpenIddictApiConstants.Application.Permissions.GrantTypes.ContainsKey(property));
            ruleBuilderOptions.WithMessage($"Value '{{PropertyValue}}' is not a valid grant type permission. Valid values are: {string.Join(", ", OpenIddictApiConstants.Application.Permissions.GrantTypes.Keys)}.");

            return ruleBuilderOptions;
        }

        public static IRuleBuilderOptions<T, string?> IsValidResponseTypePermission<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            var ruleBuilderOptions = ruleBuilder.Must(property => property != null && OpenIddictApiConstants.Application.Permissions.ResponseTypes.ContainsKey(property));
            ruleBuilderOptions.WithMessage($"Value '{{PropertyValue}}' is not a valid response type permission. Valid values are: {string.Join(", ", OpenIddictApiConstants.Application.Permissions.ResponseTypes.Keys)}.");

            return ruleBuilderOptions;
        }

        public static IRuleBuilderOptions<T, string?> IsValidScopePermission<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            var ruleBuilderOptions = ruleBuilder.Must(property => property != null && OpenIddictApiConstants.Application.Permissions.Scopes.ContainsKey(property));
            ruleBuilderOptions.WithMessage($"Value '{{PropertyValue}}' is not a valid scope permission. Valid values are: {string.Join(", ", OpenIddictApiConstants.Application.Permissions.Scopes.Keys)}.");

            return ruleBuilderOptions;
        }

        public static IRuleBuilderOptions<T, string?> IsValidCustomScopePermission<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            var ruleBuilderOptions = ruleBuilder.Must(property => property != null && property.StartsWith(OpenIddictConstants.Permissions.Prefixes.Scope));
            ruleBuilderOptions.WithMessage($"Value '{{PropertyValue}}' is not a valid custom scope permission. Custom scopes must start with '{OpenIddictConstants.Permissions.Prefixes.Scope}'.");

            return ruleBuilderOptions;
        }

        public static IRuleBuilderOptions<T, string?> IsValidFeatureRequirement<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            var ruleBuilderOptions = ruleBuilder.Must(property => property != null && OpenIddictApiConstants.Application.Requirements.Features.ContainsKey(property));
            ruleBuilderOptions.WithMessage($"Value '{{PropertyValue}}' is not a valid feature requirement. Valid values are: {string.Join(", ", OpenIddictApiConstants.Application.Requirements.Features.Keys)}.");

            return ruleBuilderOptions;
        }

        public static IRuleBuilderOptions<T, string> IsValidUri<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var ruleBuilderOptions = ruleBuilder.Must(property =>
            {
                try
                {
                    _ = new Uri(property);
                }
                catch (FormatException)
                {
                    return false;
                }

                return true;
            });
            ruleBuilderOptions.WithMessage("Value '{PropertyValue}' is not a valid URI.");

            return ruleBuilderOptions;
        }
    }
}
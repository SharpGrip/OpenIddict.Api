using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Validators;
using OpenIddict.Abstractions;

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

        public static IRuleBuilderOptions<T, string?> IsValidApplicationType<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            var applicationTypes = new List<string> {OpenIddictConstants.ClientTypes.Public, OpenIddictConstants.ClientTypes.Confidential};

            var ruleBuilderOptions = ruleBuilder.Must(property => property != null && applicationTypes.Contains(property));
            ruleBuilderOptions.WithMessage($"{{PropertyName}} contains an invalid value. Valid values are: {string.Join(", ", applicationTypes)}.");

            return ruleBuilderOptions;
        }

        public static IRuleBuilderOptions<T, string?> IsValidConsentType<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            var consentTypes = new List<string> {OpenIddictConstants.ConsentTypes.Explicit, OpenIddictConstants.ConsentTypes.External, OpenIddictConstants.ConsentTypes.Implicit, OpenIddictConstants.ConsentTypes.Systematic};

            var ruleBuilderOptions = ruleBuilder.Must(property => property != null && consentTypes.Contains(property));
            ruleBuilderOptions.WithMessage($"{{PropertyName}} contains an invalid value. Valid values are: {string.Join(", ", consentTypes)}.");

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
            ruleBuilderOptions.WithMessage("{PropertyName} is not a valid URI.");

            return ruleBuilderOptions;
        }
    }
}
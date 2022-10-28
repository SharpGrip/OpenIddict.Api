using FluentValidation;
using OpenIddict.Abstractions;
using SharpGrip.OpenIddict.Api.Models.Application;
using SharpGrip.OpenIddict.Api.Validation.Extensions;

namespace SharpGrip.OpenIddict.Api.Validation.ModelValidators.Application
{
    public class ApplicationCreateModelValidator : ModelValidator<ApplicationCreateModel>
    {
        public ApplicationCreateModelValidator()
        {
            RuleFor(m => m.DisplayName).NotEmpty().IsRequired();
            RuleFor(m => m.Type).IsRequired().IsValidApplicationType();
            RuleFor(m => m.ConsentType).IsRequired().IsValidConsentType();
            RuleFor(m => m.ClientId).IsRequired();
            RuleFor(m => m.ClientSecret).IsRequired().When(m => m.Type == OpenIddictConstants.ClientTypes.Confidential);

            RuleForEach(m => m.PostLogoutRedirectUris).IsValidUri();
            RuleForEach(m => m.RedirectUris).IsValidUri();
        }
    }
}
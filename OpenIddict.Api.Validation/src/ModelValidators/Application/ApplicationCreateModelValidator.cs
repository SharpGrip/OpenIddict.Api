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
            RuleFor(m => m.DisplayName).IsRequired();
            RuleFor(m => m.Type).IsRequired().IsValidClientType();
            RuleFor(m => m.ConsentType).IsRequired().IsValidConsentType();
            RuleFor(m => m.ClientId).IsRequired();

            When(m => m.Type == OpenIddictConstants.ClientTypes.Public,
                () => { RuleFor(m => m.ClientSecret).Empty().WithMessage("A client secret cannot be associated with a public application."); });
            When(m => m.Type == OpenIddictConstants.ClientTypes.Confidential,
                () => { RuleFor(m => m.ClientSecret).NotEmpty().WithMessage("A client secret must be associated with a confidential application."); });

            RuleForEach(m => m.EndpointPermissions).IsValidEndpointPermission();
            RuleForEach(m => m.GrantTypePermissions).IsValidGrantTypePermission();
            RuleForEach(m => m.ResponseTypesPermissions).IsValidResponseTypePermission();
            RuleForEach(m => m.ScopePermissions).IsValidScopePermission();
            RuleForEach(m => m.CustomScopePermissions).IsValidCustomScopePermission();

            RuleForEach(m => m.FeatureRequirements).IsValidFeatureRequirement();

            RuleForEach(m => m.PostLogoutRedirectUris).IsValidUri();
            RuleForEach(m => m.RedirectUris).IsValidUri();
        }
    }
}
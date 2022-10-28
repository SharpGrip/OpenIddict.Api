using SharpGrip.OpenIddict.Api.Models.Scope;
using SharpGrip.OpenIddict.Api.Validation.Extensions;

namespace SharpGrip.OpenIddict.Api.Validation.ModelValidators.Scope
{
    public class ScopeUpdateModelValidator : ModelValidator<ScopeUpdateModel>
    {
        public ScopeUpdateModelValidator()
        {
            RuleFor(m => m.Name).IsRequired();
            RuleFor(m => m.DisplayName).IsRequired();
        }
    }
}
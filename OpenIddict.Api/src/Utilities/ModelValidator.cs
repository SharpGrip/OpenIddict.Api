using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using SharpGrip.OpenIddict.Api.Models.Application;
using SharpGrip.OpenIddict.Api.Models.Scope;
using SharpGrip.OpenIddict.Api.Validation.ModelValidators.Application;
using SharpGrip.OpenIddict.Api.Validation.ModelValidators.Scope;

namespace SharpGrip.OpenIddict.Api.Utilities
{
    public class ModelValidator
    {
        private readonly IValidator<ApplicationCreateModel> applicationCreateModelValidator = new ApplicationCreateModelValidator();
        private readonly IValidator<ApplicationUpdateModel> applicationUpdateModelValidator = new ApplicationUpdateModelValidator();
        private readonly IValidator<ScopeCreateModel> scopeCreateModelValidator = new ScopeCreateModelValidator();
        private readonly IValidator<ScopeUpdateModel> scopeUpdateModelValidator = new ScopeUpdateModelValidator();

        public async Task<ValidationResult> Validate(ApplicationCreateModel applicationCreateModel)
        {
            return await applicationCreateModelValidator.ValidateAsync(applicationCreateModel);
        }

        public async Task<ValidationResult> Validate(ApplicationUpdateModel applicationUpdateModel)
        {
            return await applicationUpdateModelValidator.ValidateAsync(applicationUpdateModel);
        }

        public async Task<ValidationResult> Validate(ScopeCreateModel scopeCreateModel)
        {
            return await scopeCreateModelValidator.ValidateAsync(scopeCreateModel);
        }

        public async Task<ValidationResult> Validate(ScopeUpdateModel scopeUpdateModel)
        {
            return await scopeUpdateModelValidator.ValidateAsync(scopeUpdateModel);
        }
    }
}
using System.Collections.Generic;

namespace SharpGrip.OpenIddict.Api.Models.Application
{
    public class ApplicationCreateModel : CreateModel, IApplicationWriteModel
    {
        public string? DisplayName { get; set; }
        public string? Type { get; set; }
        public string? ConsentType { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }

        public List<string> PostLogoutRedirectUris { get; set; } = new List<string>();
        public List<string> RedirectUris { get; set; } = new List<string>();

        public List<string> EndpointPermissions { get; set; } = new List<string>();
        public List<string> GrantTypePermissions { get; set; } = new List<string>();
        public List<string> ResponseTypesPermissions { get; set; } = new List<string>();
        public List<string> ScopePermissions { get; set; } = new List<string>();
        public List<string> CustomScopePermissions { get; set; } = new List<string>();

        public List<string> FeatureRequirements { get; set; } = new List<string>();

        public IEnumerable<string> GetPermissions()
        {
            foreach (var permission in EndpointPermissions)
            {
                yield return permission;
            }

            foreach (var permission in GrantTypePermissions)
            {
                yield return permission;
            }

            foreach (var permission in ResponseTypesPermissions)
            {
                yield return permission;
            }

            foreach (var permission in ScopePermissions)
            {
                yield return permission;
            }

            foreach (var permission in CustomScopePermissions)
            {
                yield return permission;
            }
        }

        public IEnumerable<string> GetRequirements()
        {
            return FeatureRequirements;
        }
    }
}
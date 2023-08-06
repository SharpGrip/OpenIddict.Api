using System.Collections.Generic;

namespace SharpGrip.OpenIddict.Api.Models.Application
{
    public interface IApplicationWriteModel
    {
        public string? DisplayName { get; set; }
        public string? Type { get; set; }
        public string? ConsentType { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }

        public List<string> PostLogoutRedirectUris { get; set; }
        public List<string> RedirectUris { get; set; }

        public List<string> EndpointPermissions { get; set; }
        public List<string> GrantTypePermissions { get; set; }
        public List<string> ResponseTypesPermissions { get; set; }
        public List<string> ScopePermissions { get; set; }
        public List<string> CustomScopePermissions { get; set; }

        public List<string> FeatureRequirements { get; set; }

        public IEnumerable<string> GetPermissions();
        public IEnumerable<string> GetRequirements();
    }
}
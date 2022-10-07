using System;

namespace SharpGrip.OpenIddict.Api.Models.Application
{
    public class ApplicationCreateModel : CreateModel
    {
        public string? DisplayName { get; set; }
        public string? Type { get; set; }
        public string? ConsentType { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }

        public string[] Permissions { get; set; } = Array.Empty<string>();
        public string[] PostLogoutRedirectUris { get; set; } = Array.Empty<string>();
        public string[] RedirectUris { get; set; } = Array.Empty<string>();
        public string[] Requirements { get; set; } = Array.Empty<string>();
    }
}
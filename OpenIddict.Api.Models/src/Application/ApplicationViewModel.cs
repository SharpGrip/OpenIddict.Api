using System;

namespace SharpGrip.OpenIddict.Api.Models.Application
{
    public class ApplicationViewModel<TKey> : ViewModel<TKey> where TKey : struct, IEquatable<TKey>
    {
        public string? DisplayName { get; set; }
        public string? Type { get; set; }
        public string? ConsentType { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }

        public string[] Permissions { get; set; } = null!;
        public string[] PostLogoutRedirectUris { get; set; } = null!;
        public string[] RedirectUris { get; set; } = null!;
        public string[] Requirements { get; set; } = null!;
        public string[] Properties { get; set; } = null!;
    }
}
using System;

namespace SharpGrip.OpenIddict.Api.Models.Scope
{
    public class ScopeCreateModel : CreateModel
    {
        public string Name { get; set; } = null!;
        public string? DisplayName { get; set; }
        public string? Description { get; set; }

        public string[] Resources { get; set; } = Array.Empty<string>();
    }
}
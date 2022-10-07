using System;

namespace SharpGrip.OpenIddict.Api.Models.Scope
{
    public class ScopeViewModel<TKey> : ViewModel<TKey> where TKey : struct, IEquatable<TKey>
    {
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public string? ConcurrencyToken { get; set; }

        public string[] DisplayNames { get; set; } = null!;
        public string[] Descriptions { get; set; } = null!;
        public string[] Resources { get; set; } = null!;
        public string[] Properties { get; set; } = null!;
    }
}
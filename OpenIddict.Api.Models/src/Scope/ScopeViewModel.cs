using System;
using System.Collections.Generic;

namespace SharpGrip.OpenIddict.Api.Models.Scope
{
    public class ScopeViewModel<TKey> : ViewModel<TKey> where TKey : struct, IEquatable<TKey>
    {
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public string? ConcurrencyToken { get; set; }

        public List<string> Resources { get; set; } = new List<string>();
    }
}
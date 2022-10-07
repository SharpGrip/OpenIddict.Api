using System;

namespace SharpGrip.OpenIddict.Api.Models.Authorization
{
    public class AuthorizationViewModel<TKey> : ViewModel<TKey> where TKey : struct, IEquatable<TKey>
    {
        public string? Subject { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
        public string? ConcurrencyToken { get; set; }
        public DateTime? CreationDate { get; set; }

        public string[] Scopes { get; set; } = null!;
        public string[] Properties { get; set; } = null!;

        public TKey? ApplicationId { get; set; }
    }
}
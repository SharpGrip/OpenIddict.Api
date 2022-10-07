using System;

namespace SharpGrip.OpenIddict.Api.Models.Token
{
    public class TokenViewModel<TKey> : ViewModel<TKey> where TKey : struct, IEquatable<TKey>
    {
        public string? Subject { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
        public string? Payload { get; set; }
        public string? ReferenceId { get; set; }
        public string? ConcurrencyToken { get; set; }
        public DateTime? CreationDate { set; get; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? RedemptionDate { get; set; }

        public string[] Properties { get; set; } = null!;

        public TKey ApplicationId { get; set; }
        public TKey AuthorizationId { get; set; }
    }
}
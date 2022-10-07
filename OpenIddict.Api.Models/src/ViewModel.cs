using System;

namespace SharpGrip.OpenIddict.Api.Models
{
    public abstract class ViewModel<TKey> : Model where TKey : struct, IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }
}
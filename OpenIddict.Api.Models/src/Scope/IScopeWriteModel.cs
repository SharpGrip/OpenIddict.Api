using System.Collections.Generic;

namespace SharpGrip.OpenIddict.Api.Models.Scope
{
    public interface IScopeWriteModel
    {
        public string Name { get; set; }
        public string? DisplayName { get; set; }
        public string? Description { get; set; }

        public List<string> Resources { get; set; }
    }
}
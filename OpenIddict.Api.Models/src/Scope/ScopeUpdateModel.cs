using System.Collections.Generic;

namespace SharpGrip.OpenIddict.Api.Models.Scope
{
    public class ScopeUpdateModel : UpdateModel, IScopeWriteModel
    {
        public string Name { get; set; } = null!;
        public string? DisplayName { get; set; }
        public string? Description { get; set; }

        public List<string> Resources { get; set; } = new List<string>();
    }
}
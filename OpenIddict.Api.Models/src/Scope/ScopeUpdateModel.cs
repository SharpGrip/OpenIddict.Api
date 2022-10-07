namespace SharpGrip.OpenIddict.Api.Models.Scope
{
    public class ScopeUpdateModel : UpdateModel
    {
        public string Name { get; set; } = null!;
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
    }
}
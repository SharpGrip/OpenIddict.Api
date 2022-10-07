namespace SharpGrip.OpenIddict.Api.Models.Application
{
    public class ApplicationUpdateModel : UpdateModel
    {
        public string? DisplayName { get; set; }
        public string? Type { get; set; }
        public string? ConsentType { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
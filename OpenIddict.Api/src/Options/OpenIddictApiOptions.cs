namespace SharpGrip.OpenIddict.Api.Options
{
    public class OpenIddictApiOptions
    {
        public string ApiRoutePrefix { get; set; } = "api/open-id";

        public string ApplicationApiRoute { get; set; } = "application";
        public string ApplicationApiAccessScope { get; set; } = "open_id_application_api_access";
        public string AuthorizationApiRoute { get; set; } = "authorization";
        public string AuthorizationApiAccessScope { get; set; } = "open_id_authorization_api_access";
        public string ScopeApiRoute { get; set; } = "scope";
        public string ScopeApiAccessScope { get; set; } = "open_id_scope_api_access";
        public string TokenApiRoute { get; set; } = "token";
        public string TokenApiAccessScope { get; set; } = "open_id_token_api_access";
    }
}
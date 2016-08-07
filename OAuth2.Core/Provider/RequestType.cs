namespace OAuth2.Core.Provider
{
    public class RequestType
    {
        public OAuth2ProviderDefinition OAuth2ProviderDefinition { get; set; }
    }

    public class AccessTokenRequestType : RequestType
    {
        public string AuthorizationCode { get; set; }

    }
}

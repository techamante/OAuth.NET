using System;

namespace OAuth2.Core.Provider
{
    public class OAuth2ProviderAttribute:Attribute
    {
        public string Type { get; set; }

        public OAuth2ProviderAttribute(string type)
        {
            Type = type;
        }
    }
}

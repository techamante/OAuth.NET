using System;
using System.Collections.Generic;
using System.Linq;
using OAuth2.Core.Provider;

namespace OAuth2.Core
{
    public class OAuth2ProviderDefinitionService:IOAuth2ProviderDefinitionService
    {
        private IList<OAuth2ProviderDefinition> _providerDefinitions;
        public OAuth2ProviderDefinitionService()
        {
            _providerDefinitions =  new List<OAuth2ProviderDefinition> {
                    new OAuth2ProviderDefinition
                    {
                        ProviderId = Guid.Parse("e95331d1-1d8b-4c78-90b1-b73166646ef7"),
                        Name = "Facebook",
                        ClientId="1017503644971265",
                        ClientSecret = "264d17ab1be3149112732d44ce4bcf52",
                        AuthorizeEndpoint = new Endpoint
                        {
                            BaseUri = new Uri("https://www.facebook.com"),
                            Resource = "/dialog/oauth"
                        },
                        AccessTokenEndPoint = new Endpoint
                        {
                            BaseUri = new Uri("https://graph.facebook.com"),
                            Resource = "/oauth/access_token"
                        },
                        UserInfoEndPoint = new Endpoint
                        {
                             BaseUri = new Uri("https://graph.facebook.com"),
                             Resource = "/me"
                        },
                        Scopes = new List<Scope>
                        {
                            new Scope
                            {
                                Name ="public_profile"

                            }
                        }

                    }};

        }
        public OAuth2ProviderDefinition Get(Guid providerId)
        {
            return _providerDefinitions.FirstOrDefault(pD => pD.ProviderId == providerId);
        }
    }
}

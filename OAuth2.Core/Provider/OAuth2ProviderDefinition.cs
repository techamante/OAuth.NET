using System;
using System.Collections.Generic;
using System.Linq;
using OAuth2.Core.Provider;
using Techamante.Base;

namespace OAuth2.Core.Provider
{
    public class OAuth2ProviderDefinition
    {

        public Guid ProviderId { get; set; }

        public string Name { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public Endpoint AuthorizeEndpoint { get; set; }

        public Endpoint AccessTokenEndPoint { get; set; }

        public Endpoint UserInfoEndPoint { get; set; }

        public IEnumerable<Scope> Scopes { get; set; }

        public string ConcatenatedScope=> Scopes.Select(scope => scope.Name).Concatenate(" ");
        
        public Uri RedirectUri { get; set; }
    }
}

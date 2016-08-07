using System;

namespace OAuth2.Core
{
    public class OAuth2Token
    {       
        public OAuth2TokenType OAuth2TokenType { get; set; }

        public string Token { get; set; }

        public DateTime ExpirationDt { get; set; }
    }
}
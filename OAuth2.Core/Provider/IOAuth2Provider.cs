using System;
using OAuth2.Core.Models;

namespace OAuth2.Core.Provider
{
    
    public interface IOAuth2Provider
    {

        string State { get; }

        Uri GenerateLoginUri(string state = null);
    
        OAuth2Response Authorize(string authorizationCode);

        OAuth2Token GetAccessToken(OAuth2Response oAuth2Response);

        OAuth2Token GetRefreshToken(OAuth2Response oAuth2Response);

        UserInfo GetUserInfo(OAuth2Token accessToken);

        OAuth2Token RefreshAccessToken(OAuth2Token refreshToken);

    }
}
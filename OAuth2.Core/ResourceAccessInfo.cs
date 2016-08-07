using OAuth2.Core.Models;

namespace OAuth2.Core
{
    public class UserResourceAccessInfo
    {
        public UserInfo UserInfo { get; set; }

        public OAuth2Token AccessToken { get; set; }

        public OAuth2Token RefreshToken { get; set; }
    }
}
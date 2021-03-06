using System;

namespace OAuth2.Core.Provider
{
    /// <summary>
    /// Defines endpoint URI (service address).
    /// </summary>
    public class Endpoint
    {
        /// <summary>
        /// Base URI (service host address).
        /// </summary>
        public Uri BaseUri { get; set; }

        /// <summary>
        /// Resource URI (service method).
        /// </summary>
        public string Resource { get; set; }

        /// <summary>
        /// Complete URI of endpoint (base URI combined with resource URI).
        /// </summary>
        public string Uri { get { return BaseUri + Resource; } }
    }
}
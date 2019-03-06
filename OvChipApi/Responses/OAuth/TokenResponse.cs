using Newtonsoft.Json;

namespace OvChipApi.Responses.OAuth
{
    /// <summary>
    /// OAuth 2.0 login response data.
    /// More information: https://tools.ietf.org/html/draft-ietf-oauth-v2-22#section-4.2.2
    /// </summary>
    public class TokenResponse
    {
        [JsonProperty("scope", Required = Required.Always)]
        public string Scope { get; set; }

        [JsonProperty("token_type", Required = Required.Always)]
        public string TokenType { get; set; }

        /// <summary>
        /// The lifetime in seconds of the access token.
        /// For example, the value "3600" denotes that the access token will expire in one hour from the time the response was generated.
        /// </summary>
        [JsonProperty("expires_in", Required = Required.Always)]
        public int ExpiresIn { get; set; }

        [JsonProperty("access_token", Required = Required.Always)]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token", Required = Required.Always)]
        public string RefreshToken { get; set; }

        [JsonProperty("id_token", Required = Required.Always)]
        public string IdToken { get; set; }
    }
}

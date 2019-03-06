using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OvChipApi.Exceptions;
using OvChipApi.Responses;
using OvChipApi.Responses.OAuth;

namespace OvChipApi
{
    public class OvClient : IDisposable
    {
        private readonly HttpClient _httpClient;

        public OvClient()
        {
            _httpClient = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip
            });

            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(Constants.UserAgent);
        }

        public TokenResponse OAuthTokens { get; private set; }

        public string AuthorizationToken { get; private set; }

        /// <summary>
        /// This is true when the <see cref="OvClient"/> is authenticated properly and is
        /// ready to make authenticated requests.
        /// </summary>
        public bool IsAuthenticated => OAuthTokens != null && !string.IsNullOrWhiteSpace(AuthorizationToken);

        /// <summary>
        /// Checks if an update is available for the android version used for this library.
        /// This may (and should) only be called before signing in.
        /// </summary>
        /// <returns>Returns a <see cref="UpdateResponse"/> object of 'OV-chipkaart.nl'.</returns>
        public async Task<UpdateResponse> CheckForUpdateAsync()
        {
            var request = new Dictionary<string, string>
            {
                { "api_key", Constants.Platform },
                { "currentversion", Constants.Version },   
            };

            var response = await SendRequestAsync<Response<UpdateResponse>>($"{Constants.ApiUrl}/update/checkforupdate", request);

            return response.Data;
        }

        /// <summary>
        /// Authenticates with OV-chipkaart and stores the authentication and authorization tokens
        /// so we can make authenticated requests. Which is required to fetch cards, transactions etc..
        /// </summary>
        /// <param name="username">An OV-chipkaart username.</param>
        /// <param name="password">An OV-chipkaart password that belongs to <see cref="username"/>.</param>
        /// <returns>Returns true if authentication was succesfull.</returns>
        public async Task<bool> LoginAsync(string username, string password)
        {
            // Step one: Receive OAuth 2.0 tokens
            // - Not using SendRequestAsync because we need to parse OAuth response based on status code.
            var request = new Dictionary<string, string>
            {
                { "scope", "openid" },
                { "client_secret", Constants.ClientSecret },
                { "password", password },
                { "client_id", Constants.ClientId },
                { "username", username },
                { "grant_type", "password" }
            };

            TokenResponse tokens;

            using (var oAuthResponse = await _httpClient.PostAsync(Constants.OAuthUrl, new FormUrlEncodedContent(request)))
            {
                var responseData = await oAuthResponse.Content.ReadAsStringAsync();

                if (!oAuthResponse.IsSuccessStatusCode)
                {
                    var tokenError = JsonConvert.DeserializeObject<TokenErrorResponse>(responseData);

                    throw new OvOAuthException($"OV-chipkaart OAuth gave an error: \"{tokenError.Error}: {tokenError.ErrorDescription}\". Your credentials may be incorrect.");
                }

                tokens = JsonConvert.DeserializeObject<TokenResponse>(responseData);
            }

            // Step two: Authorize tokens at the mobile gateway
            request = new Dictionary<string, string>
            {
                { "authenticationToken", tokens.IdToken }
            };

            var response = await SendRequestAsync<Response<string>>($"{Constants.ApiUrl}/api/authorize", request);
            if (response.StatusCode != 200) return false;

            OAuthTokens = tokens;
            AuthorizationToken = response.Data;

            return true;
        }

        /// <summary>
        /// Gets the ov cards
        /// </summary>
        /// <returns>The cards async.</returns>
        public async Task<Response<List<OvCardResponse>>> GetCardsAsync()
        {
            if (!IsAuthenticated)
            {
                throw new OvOAuthException("Client was not authenticated");
            }

            Dictionary<string, string> data = new Dictionary<string, string>
            {
                { "authorizationToken", this.AuthorizationToken }
            };

            return await SendRequestAsync<Response<List<OvCardResponse>>>($"{Constants.ApiUrl}{Constants.CardListEndpoint}", data);
        }

        public async Task<Response<TransactionResponse>> GetTransactionsAsync(String mediumId) 
        {
            if (!IsAuthenticated)
            {
                throw new OvOAuthException("Client was not authenticated");
            }

            Dictionary<string, string> data = new Dictionary<string, string>
            {
                { "authorizationToken", this.AuthorizationToken },
                { "mediumId" , mediumId }
            };

            return await SendRequestAsync<Response<TransactionResponse>>($"{Constants.ApiUrl}{Constants.TransactionsEndpoint}", data);
        }

        /// <summary>
        /// Sends a request and parses the json response.
        /// </summary>
        /// <typeparam name="T">The object you want to use for parsing the json response.</typeparam>
        /// <param name="url">The destination url.</param>
        /// <param name="data">The post data.</param>
        /// <returns>Returns the requested object <see cref="T"/>.</returns>
        private async Task<T> SendRequestAsync<T>(string url, Dictionary<string, string> data)
        {
            using (var response = await _httpClient.PostAsync(url, new FormUrlEncodedContent(data)))
            {
                var responseData = await response.Content.ReadAsStringAsync();
                
                return JsonConvert.DeserializeObject<T>(responseData);
            }
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}

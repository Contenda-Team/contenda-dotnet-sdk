using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Contenda.Sdk.Exceptions;
using Contenda.Sdk.Models.Request;
using Contenda.Sdk.Models.Result;
using Newtonsoft.Json;

namespace Contenda.Sdk.Auth
{
    /// <summary>
    /// API Key Authentication Provider
    /// </summary>
    public sealed class APIKeyAuthProvider : IAuthProvider
    {
        private readonly string _email;
        private readonly string _apiKey;

        private readonly DateTime _invalidDate = new(1970, 1, 1);

        private string? _apiToken;

        /// <summary>
        /// Constructor for an API key auth provider
        /// </summary>
        /// <param name="email">user's email</param>
        /// <param name="apiKey">api key</param>
        public APIKeyAuthProvider(string email, string apiKey)
        {
            _email = email;
            _apiKey = apiKey;
        }

        private DateTime ValidUntil()
        {
            if (_apiToken == null) return _invalidDate;
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(_apiToken);
                return token.ValidTo;
            }
            catch
            {
                return _invalidDate;
            }
        }

        /// <inheritdoc />
        public async Task<bool> TryAuthenticate(string apiBaseUri)
        {
            if (EnsureValid()) return true;

            var client = PoorMansHttpClientFactory.Instance.Client;

            var uri = $"{apiBaseUri}{Constants.AuthVersion1Prefix}{Constants.AuthV1.Token}";
            var body = new AuthV1Token
            {
                api_key = _apiKey,
                user_email = _email
            };
            var bodyString = JsonConvert.SerializeObject(body);

            try
            {
                var response = await client.PostAsync(new Uri(uri), new StringContent(bodyString, Encoding.UTF8, Constants.JsonMimeType)).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                var tokenResponse = await response.Content.ReadAsStringAsync();
                var tokenResult = JsonConvert.DeserializeObject<TokenResult>(tokenResponse);
                _apiToken = tokenResult!.access_token;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool EnsureValid() => DateTime.UtcNow < ValidUntil();

        /// <inheritdoc />
        public void ModifyHeadersCallback(HttpClient httpClient)
        {
            if (!EnsureValid()) throw new AuthenticationException("You have to be authenticated to call APIs with authentication.");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiToken);
        }

        /// <inheritdoc />
        public string ModifyQueryCallback(string currentQuery)
        {
            if (!EnsureValid()) throw new AuthenticationException("You have to be authenticated to call APIs with authentication.");

            // nothing to do here

            return currentQuery;
        }

    }
}

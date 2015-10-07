using LonoNet.Authenticators;
using LonoNet.Exceptions;
using LonoNet.Helpers;
using LonoNet.Models;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using System;
using System.Net;
using OAuth2Authenticator = LonoNet.Authenticators.OAuth2Authenticator;

namespace LonoNet.Client
{
    public partial class LonoNetClient : ILonoNetClient
    {
        private const string ApiBaseUrl = "http://make.lono.io";
        private const string Version = "v1";

        private UserLogin _userLogin;

        public UserLogin UserLogin
        {
            get { return _userLogin; }
            set
            {
                _userLogin = value;
                SetAuthProviders();
            }
        }

        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _deviceId;

        private RestClient _restClient;
        private RequestHelper _requestHelper;

        /// <summary>
        /// Default Constructor for the LonoNetClient
        /// </summary>
        /// <param name="clientId">The developer client ID to use for the Lono requests</param>
        /// <param name="clientSecret">The developer client secret to use for the Lono requests</param>
        /// <param name="deviceId">The device identifier given to your Lono controller</param>
        public LonoNetClient(string clientId, string clientSecret, string deviceId)
        {
            _deviceId = deviceId;
            LoadClient();
            _clientId = clientId;
            _clientSecret = clientId;
            UserLogin = null;
        }

        /// <summary>
        /// Creates an instance of the LonoNetClient given a developer client ID, client secret and an OAuth2 access token
        /// </summary>
        /// <param name="clientId">The developer client ID to use for the Lono requests</param>
        /// <param name="clientSecret">The developer client secret to use for the Lono requests</param>
        /// <param name="accessToken">The OAuth2 access token</param>
        /// <param name="deviceId">The device identifier given to your Lono controller</param>
        public LonoNetClient(string clientId, string clientSecret, string accessToken, string deviceId)
            : this(clientId, clientSecret, deviceId)
        {
            UserLogin = new UserLogin { Token = accessToken };
        }

        private void LoadClient()
        {
            _restClient = new RestClient(ApiBaseUrl);

            _restClient.ClearHandlers();
            _restClient.AddHandler("*", new JsonDeserializer());

            _requestHelper = new RequestHelper(Version, _deviceId);
        }

        public string BuildAuthorizeUrl(OAuth2AuthorizationFlow oAuth2AuthorizationFlow, string redirectUri, string scope = "write")
        {
            if (string.IsNullOrWhiteSpace(redirectUri))
            {
                throw new ArgumentNullException("redirectUri");
            }
            RestRequest request = _requestHelper.BuildOAuth2AuthorizeUrl(oAuth2AuthorizationFlow, _clientId, redirectUri, scope);
            return _restClient.BuildUri(request).ToString();
        }

        private T ExecuteSync<T>(IRestRequest request) where T : new()
        {
            IRestResponse<T> response;
            response = _restClient.Execute<T>(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new LonoRestException(response, HttpStatusCode.OK);
            }

            return response.Data;
        }

        private IRestResponse ExecuteSync(IRestRequest request)
        {
            IRestResponse response;
            response = _restClient.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new LonoRestException(response, HttpStatusCode.OK);
            }

            return response;
        }

        private void SetAuthProviders()
        {
            _restClient.Authenticator = GetAuthenticator(_restClient.BaseUrl.ToString());
        }

        private IAuthenticator GetAuthenticator(string baseUrl)
        {
            var userToken = UserLogin == null ? null : UserLogin.Token;
            //var userSecret = UserLogin == null ? null : UserLogin.Secret;
            return new OAuth2Authenticator(userToken);
        }
    }
}

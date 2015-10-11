using LonoNet.Exceptions;
using LonoNet.Helpers;
using LonoNet.Models;
using RestSharp;
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

        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _deviceId;

        private RestClient _restClient;
        private RequestHelper _requestHelper;
        private UserLogin _userLogin;

        public UserLogin UserLogin
        {
            get { return _userLogin; }
            set
            {
                _userLogin = value;
                SetAuthProvider();
            }
        }

        /// <summary>
        /// Creates an instance of the LonoNetClient
        /// </summary>
        /// <param name="clientId">The developer client ID to use for the Lono requests</param>
        /// <param name="clientSecret">The developer client secret to use for the Lono requests</param>
        /// <param name="deviceId">The device identifier given to your Lono controller</param>
        public LonoNetClient(string clientId, string clientSecret, string deviceId)
        {
            _deviceId = deviceId;
            LoadClient();
            _clientId = clientId;
            _clientSecret = clientSecret;
            UserLogin = null;
        }

        /// <summary>
        /// Creates an instance of the LonoNetClient
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

        /// <summary>
        /// First step of authorizing your application. This builds an OAuth 2.0 URL that you'll need to send users to in order for them to authorize your application. It is recommended that you
        /// launch the resulting URI in the user's default browser. Once they approve, they will be redirected to the URL specified by the 'redirectUri' parameter.
        /// </summary>
        /// <param name="redirectUri">Where to redirect the user after authorization has been granted.</param>
        /// <param name="scope">The scope requested for this session. "write" gives full access.</param>
        /// <returns>A URL to which your app should redirect the user for authorization. After the user authorizes your app, they will be sent to your redirect URI.</returns>
        public string BuildAuthorizationUrl(string redirectUri, string scope = "write")
        {
            if (string.IsNullOrWhiteSpace(redirectUri))
            {
                throw new ArgumentNullException("redirectUri");
            }

            RestRequest request = _requestHelper.BuildAuthorizationUrl(_clientId, redirectUri, scope);
            return _restClient.BuildUri(request).ToString();
        }

        private void LoadClient()
        {
            _restClient = new RestClient(ApiBaseUrl);

            _restClient.ClearHandlers();
            _restClient.AddHandler("*", new JsonDeserializer());

            _requestHelper = new RequestHelper(Version, _deviceId);
        }

        private void SetAuthProvider()
        {
            var userToken = UserLogin == null ? null : UserLogin.Token;

            _restClient.Authenticator = new OAuth2Authenticator(userToken);
        }

        private T Execute<T>(IRestRequest request) where T : new()
        {
            IRestResponse<T> response;
            response = _restClient.Execute<T>(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new LonoRestException(response, HttpStatusCode.OK);
            }

            return response.Data;
        }
    }
}

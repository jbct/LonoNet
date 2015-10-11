using LonoNet.Models;
using RestSharp;

namespace LonoNet.Client
{
    public partial class LonoNetClient
    {
        /// <summary>
        /// Last step in the authorization flow. It retrieves an OAuth2 access token using the code provided by Lono.
        /// </summary>
        /// <param name="code">The code provided by Lono when the user was redirected back to the calling site.</param>
        public UserLogin GetAccessToken(string code)
        {
            RestRequest request = _requestHelper.CreateAccessTokenRequest(code, _clientId, _clientSecret);
            var response = Execute<AccessToken>(request);
            var userLogin = new UserLogin { Token = response.Access_Token };
            UserLogin = userLogin;
            return userLogin;
        }
    }
}

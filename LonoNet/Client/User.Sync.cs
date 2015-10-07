﻿using LonoNet.Models;
using RestSharp;

namespace LonoNet.Client
{
    public partial class LonoNetClient
    {
        public UserLogin GetAccessToken(string code, string redirectUri)
        {
            RestRequest request = _requestHelper.CreateOAuth2AccessTokenRequest(code, redirectUri, _clientId, _clientSecret);
            var response = ExecuteSync<OAuth2AccessToken>(request);
            var userLogin = new UserLogin { Token = response.Access_Token };
            UserLogin = userLogin;
            return userLogin;
        }
    }
}
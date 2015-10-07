using LonoNet.Authenticators;
using RestSharp;
using System;

namespace LonoNet.Helpers
{
    /// <summary>
    /// Helper class for creating LonoNet RestSharp requests
    /// </summary>
    public class RequestHelper
    {
        private readonly string _version;
        private readonly string _deviceId;

        public RequestHelper(string version, string deviceId)
        {
            _version = version;
            _deviceId = deviceId;
        }

        public RestRequest TurnZoneOn(int zoneId)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "api/{version}/devices/{deviceId}/zones/{zoneId}/on";
            request.AddParameter("version", _version, ParameterType.UrlSegment);
            request.AddParameter("deviceId", _deviceId, ParameterType.UrlSegment);
            request.AddParameter("zoneId", zoneId, ParameterType.UrlSegment);

            return request;
        }

        public RestRequest TurnZoneOff(int zoneId)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "api/{version}/devices/{deviceId}/zones/{zoneId}/off";
            request.AddParameter("version", _version, ParameterType.UrlSegment);
            request.AddParameter("deviceId", _deviceId, ParameterType.UrlSegment);
            request.AddParameter("zoneId", zoneId, ParameterType.UrlSegment);

            return request;
        }

        public RestRequest GetZoneState()
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "api/{version}/devices/{deviceId}/zones/state";
            request.AddParameter("version", _version, ParameterType.UrlSegment);
            request.AddParameter("deviceId", _deviceId, ParameterType.UrlSegment);

            return request;
        }

        public RestRequest GetZoneInfo()
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "api/{version}/devices/{deviceId}/zones";
            request.AddParameter("version", _version, ParameterType.UrlSegment);
            request.AddParameter("deviceId", _deviceId, ParameterType.UrlSegment);

            return request;
        }

        public RestRequest UpdateZones()
        {
            throw new NotImplementedException();
        }

        public RestRequest DetectZones()
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "api/{version}/devices/{deviceId}/zones/detect";
            request.AddParameter("version", _version, ParameterType.UrlSegment);
            request.AddParameter("deviceId", _deviceId, ParameterType.UrlSegment);

            return request;
        }

        public RestRequest GetLonoInfo()
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "api/{version}/devices/{deviceId}";
            request.AddParameter("version", _version, ParameterType.UrlSegment);
            request.AddParameter("deviceId", _deviceId, ParameterType.UrlSegment);

            return request;
        }

        /// <summary>
        /// This is the first step the OAuth 2.0 authorization flow. This isn't an API call—it's the web page that lets the user sign in to Lono's developer portal and authorize your app. The user must be redirected to the page over HTTPS and it should be presented to the user through their web browser. After the user decides whether or not to authorize your app, they will be redirected to the URL specified by redirect_uri.
        /// </summary>
        /// <param name="oAuth2AuthorizationFlow">The type of authorization flow to use.  See AuthorizationFlow documentation for descriptions of each.</param>
        /// <param name="clientId"></param>
        /// <param name="redirectUri">Where to redirect the user after authorization has completed. This must be the exact URI registered in the app console, though localhost and 127.0.0.1 are always accepted. A redirect URI is required for a token flow, but optional for code. If the redirect URI is omitted, the code will be presented directly to the user and they will be invited to enter the information in your app.</param>
        /// <param name="scope">Arbitrary data that will be passed back to your redirect URI. This parameter can be used to track a user through the authorization flow.</param>
        /// <returns>A URL to which you should redirect the user.  Because /dialog/authorize is a website, there is no direct return value. However, after the user authorizes your app, they will be sent to your redirect URI. The type of response varies based on the response_type.</returns>
        public RestRequest BuildOAuth2AuthorizeUrl(OAuth2AuthorizationFlow oAuth2AuthorizationFlow, string clientId, string redirectUri, string scope)
        {
            var request = new RestRequest("dialog/authorize", Method.GET);
            request.AddParameter("response_type", Enum.GetName(typeof(OAuth2AuthorizationFlow), oAuth2AuthorizationFlow).ToLower());
            request.AddParameter("client_id", clientId);
            request.AddParameter("redirect_uri", redirectUri);
            request.AddParameter("scope", scope);
            return request;
        }

        /// <summary>
        /// This is the second and final step in the OAuth 2.0 'code' authorization flow.  It retrieves an OAuth2 access token using the code provided by Lono.
        /// </summary>
        /// <param name="code">The code provided by Lono when the user was redirected back to the calling site.</param>
        /// <param name="redirectUri">The calling site's host name, for verification only</param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <returns></returns>
        public RestRequest CreateOAuth2AccessTokenRequest(string code, string redirectUri, string clientId, string clientSecret)
        {
            var request = new RestRequest("oauth/token", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("client_id", clientId);
            request.AddParameter("client_secret", clientSecret);
            request.AddParameter("code", code);
            request.AddParameter("redirect_uri", redirectUri);
            return request;
        }
    }
}

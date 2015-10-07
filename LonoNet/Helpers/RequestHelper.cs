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

        /// <summary>
        /// Turns a zone off or on. When specifing a zone, keep in
        /// mind they are zero based, so to turn on zone 1 you'd want
        /// to specify 0 for the zoneId.
        /// </summary>
        /// <param name="zoneId">The zone to turn on or off.</param>
        /// <param name="isEnabled">true to turn the zone on, false otherwise.</param>
        /// <returns></returns>
        public RestRequest SetZone(int zoneId, bool isEnabled)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "api/{version}/devices/{deviceId}/zones/{zoneId}/{state}";
            request.AddParameter("version", _version, ParameterType.UrlSegment);
            request.AddParameter("deviceId", _deviceId, ParameterType.UrlSegment);
            request.AddParameter("zoneId", zoneId, ParameterType.UrlSegment);
            request.AddParameter("state", isEnabled ? "on" : "off", ParameterType.UrlSegment);

            return request;
        }

        /// <summary>
        /// Get the current zone that is enabled on Lono (No zone on will be null).
        /// </summary>
        /// <returns></returns>
        public RestRequest GetZoneState()
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "api/{version}/devices/{deviceId}/zones/state";
            request.AddParameter("version", _version, ParameterType.UrlSegment);
            request.AddParameter("deviceId", _deviceId, ParameterType.UrlSegment);

            return request;
        }

        /// <summary>
        /// Get a bunch of metadata that is stored internally with each Zone, like
        /// cycle time and soil type.
        /// </summary>
        /// <returns></returns>
        public RestRequest GetZoneInfo()
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "api/{version}/devices/{deviceId}/zones";
            request.AddParameter("version", _version, ParameterType.UrlSegment);
            request.AddParameter("deviceId", _deviceId, ParameterType.UrlSegment);

            return request;
        }

        /// <summary>
        /// Update the zones zonnected to a Lono with the zones specified. Zones is
        /// an array of zone objects (like what you'd receive from GetZoneInfo).
        /// </summary>
        /// <returns></returns>
        public RestRequest UpdateZones()
        {
            throw new NotImplementedException();

            //var request = new RestRequest(Method.PUT);
            //request.Resource = "api/{version}/devices/{deviceId}/zones";
            //request.AddParameter("version", _version, ParameterType.UrlSegment);
            //request.AddParameter("deviceId", _deviceId, ParameterType.UrlSegment);

            //// body contains zones array

            //return request;
        }

        /// <summary>
        /// Run a zone detect sequence to discover which zones have been attached to
        /// Lono.
        /// </summary>
        /// <returns></returns>
        public RestRequest DetectZones()
        {
            var request = new RestRequest(Method.POST);
            request.Resource = "api/{version}/devices/{deviceId}/zones/detect";
            request.AddParameter("version", _version, ParameterType.UrlSegment);
            request.AddParameter("deviceId", _deviceId, ParameterType.UrlSegment);

            return request;
        }

        /// <summary>
        /// Get a bunch of metadata that is stored internally with Lono, like
        /// hardware revision information and basic scheduling options.
        /// </summary>
        /// <returns></returns>
        public RestRequest GetLonoInfo()
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "api/{version}/devices/{deviceId}";
            request.AddParameter("version", _version, ParameterType.UrlSegment);
            request.AddParameter("deviceId", _deviceId, ParameterType.UrlSegment);

            return request;
        }

        /// <summary>
        /// Set the Lono's internal LED Color.
        /// </summary>
        /// <returns></returns>
        public RestRequest SetLed()
        {
            throw new NotImplementedException();

            //var request = new RestRequest(Method.POST);
            //request.Resource = "api/{version}/devices/{deviceId}/state";
            //request.AddParameter("version", _version, ParameterType.UrlSegment);
            //request.AddParameter("deviceId", _deviceId, ParameterType.UrlSegment);

            //// color
            //// mode
            //// brightness
            //// interval
            //// times

            //return request;
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

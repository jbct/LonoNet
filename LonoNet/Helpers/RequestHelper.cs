using LonoNet.Models;
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
            var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
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

            //var request = new RestRequest(Method.PUT) { RequestFormat = DataFormat.Json };
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
            var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
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
        public RestRequest SetLed(int desiredColor, LedMode desiredMode)
        {
            var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };
            request.Resource = "api/{version}/devices/{deviceId}/state";
            request.AddParameter("version", _version, ParameterType.UrlSegment);
            request.AddParameter("deviceId", _deviceId, ParameterType.UrlSegment);
            request.AddBody(new SetLedOptions() { color = desiredColor, mode = desiredMode, brightness = 255, interval = 5, times = 15 });

            return request;
        }

        /// <summary>
        /// First step of authorizing your application.  This builds an OAuth 2.0 URL that you'll need to send users to in order for them to authorize your application.  It is recommended that you
        /// launch the resulting URI in the user's default browser.  Once they approve, they will be redirected to the URL specified by the 'redirectUri' parameter.
        /// </summary>
        /// <param name="clientId">The developer client ID to use for the Lono requests</param>
        /// <param name="redirectUri">Where to redirect the user after authorization has been granted.</param>
        /// <param name="scope">The scope requested for this session. "write" gives full access.</param>
        /// <returns>A URL to which your app should redirect the user for authorization. After the user authorizes your app, they will be sent to your redirect URI.</returns>
        public RestRequest BuildAuthorizationUrl(string clientId, string redirectUri, string scope)
        {
            var request = new RestRequest("dialog/authorize", Method.GET);
            request.AddParameter("response_type", "code");
            request.AddParameter("client_id", clientId);
            request.AddParameter("redirect_uri", redirectUri);
            request.AddParameter("scope", scope);
            return request;
        }

        /// <summary>
        /// Last step in the authorization flow. It retrieves an OAuth2 access token using the code provided by Lono.
        /// </summary>
        /// <param name="code">The code provided by Lono when the user was redirected back to the calling site.</param>
        /// <param name="clientId">The developer client ID to use for the Lono requests</param>
        /// <param name="clientSecret">The developer client secret to use for the Lono requests</param>
        /// <returns></returns>
        public RestRequest CreateAccessTokenRequest(string code, string clientId, string clientSecret)
        {
            var request = new RestRequest("oauth/token", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("client_id", clientId);
            request.AddParameter("client_secret", clientSecret);
            request.AddParameter("code", code);
            return request;
        }
    }
}

using LonoNet.Authenticators;

namespace LonoNet.Client
{
    public interface ILonoNetClient
    {
        /// <summary>
        /// This starts the OAuth 2.0 authorization flow. This isn't an API call—it's the web page that lets the user sign in to Lono's developer portal and authorize your app. The user must be redirected to the page over HTTPS and it should be presented to the user through their web browser. After the user decides whether or not to authorize your app, they will be redirected to the URL specified by the 'redirectUri'.
        /// </summary>
        /// <param name="oAuth2AuthorizationFlow">The type of authorization flow to use.  See the OAuth2AuthorizationFlow enum documentation for more information.</param>
        /// <param name="redirectUri">Where to redirect the user after authorization has completed. A redirect URI is required for a token flow, but optional for code. If the redirect URI is omitted, the code will be presented directly to the user and they will be invited to enter the information in your app.</param>
        /// <param name="scope">The scope requested for this session.  "write" gives full access.</param>
        /// <returns>A URL to which your app should redirect the user for authorization.  After the user authorizes your app, they will be sent to your redirect URI. The type of response varies based on the 'oauth2AuthorizationFlow' argument.</returns>
        string BuildAuthorizeUrl(OAuth2AuthorizationFlow oAuth2AuthorizationFlow, string redirectUri, string scope = "write");
    }
}

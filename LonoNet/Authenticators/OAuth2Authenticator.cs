using RestSharp;

namespace LonoNet.Authenticators
{
    public class OAuth2Authenticator : RestSharp.Authenticators.OAuth2Authenticator
    {
        public OAuth2Authenticator(string accessToken) : base(accessToken)
        {
        }

        public override void Authenticate(IRestClient client, IRestRequest request)
        {
            if (!string.IsNullOrWhiteSpace(AccessToken))
            {
                request.AddHeader("authorization", string.Format("bearer {0}", AccessToken));
            }
        }
    }
}

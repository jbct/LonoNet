namespace LonoNet.Client
{
    public interface ILonoNetClient
    {
        string BuildAuthorizationUrl(string redirectUri, string scope = "write");
    }
}

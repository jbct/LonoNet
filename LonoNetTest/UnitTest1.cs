using LonoNet.Client;
using LonoNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace LonoNetTest
{
    /// <summary>
    /// Early stage.  At this point, this is more of sample code to get you started.  Will work on actual unit tests soon.
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public readonly string _clientId = "CLIENT KEY";
        public readonly string _clientSecret = "CLIENT SECRET";
        public readonly string _authToken = "AUTH TOKEN";
        public readonly string _deviceId = "DEVICE ID";

        public readonly string _authCode = "AUTH CODE";

        [TestMethod]
        public void TestMethod1()
        {
            LonoNetClient client = new LonoNetClient(_clientId, _clientSecret, _authToken, _deviceId);
            var login = client.GetAccessToken(_authCode, "http://localhost");

            ZoneState zs = client.GetActiveZone();
            ZoneInfo zi = client.GetAllZones();
            DeviceInfo di = client.GetDeviceInfo();
            client.SetZone(1, true);
            Thread.Sleep(10);
            client.SetZone(1, false);
        }

        [TestMethod]
        public void TestMethod2()
        {
            LonoNetClient client = new LonoNetClient(_clientId, _clientSecret, _deviceId);
            string url = client.BuildAuthorizeUrl(LonoNet.Authenticators.OAuth2AuthorizationFlow.Code, "http://localhost", "write");
            // Launch url
            // set _authCode to value returned in url
            var login = client.GetAccessToken(_authCode, "http://localhost");

            ZoneState zs = client.GetActiveZone();
            ZoneInfo zi = client.GetAllZones();
            DeviceInfo di = client.GetDeviceInfo();
            client.SetZone(1, true);
            Thread.Sleep(10);
            client.SetZone(1, false);
        }
    }
}

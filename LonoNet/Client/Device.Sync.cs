using LonoNet.Models;

namespace LonoNet.Client
{
    public partial class LonoNetClient
    {
        public ZoneState GetActiveZone()
        {
            var request = _requestHelper.GetZoneState();
            return ExecuteSync<ZoneState>(request);
        }

        public ZoneInfo GetAllZones()
        {
            var request = _requestHelper.GetZoneInfo();
            return ExecuteSync<ZoneInfo>(request);
        }

        public DeviceInfo GetDeviceInfo()
        {
            var request = _requestHelper.GetLonoInfo();
            return ExecuteSync<DeviceInfo>(request);
        }

        public GenericResult SetZoneOn(int zoneId)
        {
            var request = _requestHelper.TurnZoneOn(zoneId);
            return ExecuteSync<GenericResult>(request);
        }

        public GenericResult SetZoneOff(int zoneId)
        {
            var request = _requestHelper.TurnZoneOff(zoneId);
            return ExecuteSync<GenericResult>(request);
        }
    }
}

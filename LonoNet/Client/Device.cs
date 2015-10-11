using LonoNet.Models;

namespace LonoNet.Client
{
    public partial class LonoNetClient
    {
        /// <summary>
        /// Get the current zone that is enabled on Lono (null if no zones are on).
        /// </summary>
        /// <returns>Object containing the result</returns>
        public ZoneState GetActiveZone()
        {
            var request = _requestHelper.GetZoneState();
            return Execute<ZoneState>(request);
        }

        /// <summary>
        /// Get a bunch of metadata that is stored internally with each zone, like
        /// cycle time and soil type.
        /// </summary>
        /// <returns>Object containing zone information</returns>
        public ZoneInfo GetAllZones()
        {
            var request = _requestHelper.GetZoneInfo();
            return Execute<ZoneInfo>(request);
        }

        /// <summary>
        /// Get a bunch of metadata that is stored internally with Lono, like
        /// hardware revision information and basic scheduling options.
        /// </summary>
        /// <returns>Object containing device information</returns>
        public DeviceInfo GetDeviceInfo()
        {
            var request = _requestHelper.GetLonoInfo();
            return Execute<DeviceInfo>(request);
        }

        /// <summary>
        /// Turns a zone on or off.
        /// </summary>
        /// <param name="zoneId">The zone to turn on or off.</param>
        /// <param name="isEnabled">true to turn the zone on, false otherwise.</param>
        /// <returns>Object containing message returned</returns>
        public GenericResult SetZone(int zoneId, bool isEnabled)
        {
            var request = _requestHelper.SetZone(zoneId, isEnabled);
            return Execute<GenericResult>(request);
        }

        /// <summary>
        /// Detects the zones connected to the Lono device.
        /// </summary>
        /// <returns>Object containing detected zones</returns>
        public DetectZoneInfo DetectZones()
        {
            var request = _requestHelper.DetectZones();
            return Execute<DetectZoneInfo>(request);
        }

        //public GenericResult SetLed(int desiredColor, LedMode desiredMode)
        //{
        //    var request = _requestHelper.SetLed(desiredColor, desiredMode);
        //    return Execute<GenericResult>(request);
        //}
    }
}

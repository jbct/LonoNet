using System.Collections.Generic;

namespace LonoNet.Models
{
    public class DeviceInfo
    {
        public string name { get; set; }
        public DeviceInfoData data { get; set; }
    }

    public class DeviceInfoData
    {
        public int firmware_version { get; set; }
        public int hardware_revision { get; set; }
        public int id { get; set; }
        public float lat { get; set; }
        public float lng { get; set; }
        public string lono_id { get; set; }
        public string model_string { get; set; }
        public DeviceInfoDataTimes times { get; set; }
    }

    public class DeviceInfoDataTimes
    {
        public DeviceInfoDataTimesGlobal global { get; set; }
    }

    public class DeviceInfoDataTimesGlobal
    {
        public List<DeviceInfoDataTimesGlobalAllowed> allowed { get; set; }
        public List<DeviceInfoDataTimesGlobalBlackouts> blackouts { get; set; }
        public DeviceInfoDataTimesGlobalFrequency frequency { get; set; }
    }

    public class DeviceInfoDataTimesGlobalAllowed
    {
        public int day { get; set; }
        public int enabled { get; set; }

    }

    public class DeviceInfoDataTimesGlobalBlackouts
    {

    }

    public class DeviceInfoDataTimesGlobalFrequency
    {
        public DeviceInfoDataTimesGlobalFrequencyDaily daily { get; set; }
    }

    public class DeviceInfoDataTimesGlobalFrequencyDaily
    {
        public bool even { get; set; }
        public int every { get; set; }
        public bool odd { get; set; }
    }
}

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
    }
}

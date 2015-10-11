using System.Collections.Generic;

namespace LonoNet.Models
{
    public class DetectZoneInfo
    {
        public string name { get; set; }
        public List<DetectZoneInfoData> data { get; set; }
    }

    public class DetectZoneInfoData
    {
        public int zone { get; set; }
    }
}

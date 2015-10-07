using System.Collections.Generic;

namespace LonoNet.Models
{
    public class ZoneInfo
    {
        public string name { get; set; }
        public List<ZoneInfoData> data { get; set; }
    }

    public class ZoneInfoData
    {
        public int cycle_time { get; set; }
        public int cycles { get; set; }
        public int enabled { get; set; }
        public int has_custom_time_schedule { get; set; }
        public int high_evapo_environment { get; set; }
        public int is_master { get; set; }
        public int master_start_offset { get; set; }
        public string name { get; set; }
        public int needs_master { get; set; }
        public int number { get; set; }
        public int order { get; set; }
        public string plant_type { get; set; }
        public int runs_with_others { get; set; }
        public string slope_type { get; set; }
        public int soak_time { get; set; }
        public string soil_type { get; set; }
        public string sprinkler_type { get; set; }
        public string sunlight_exposure_type { get; set; }
        public string wind_exposure_level { get; set; }
    }
}

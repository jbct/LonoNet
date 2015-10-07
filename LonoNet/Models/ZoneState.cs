namespace LonoNet.Models
{
    public class ZoneState
    {
        public string name { get; set; }
        public ZoneStateData data { get; set; }
    }

    public class ZoneStateData
    {
        public string zone { get; set; }
    }
}

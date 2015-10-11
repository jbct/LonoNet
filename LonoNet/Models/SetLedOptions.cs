namespace LonoNet.Models
{
    public class SetLedOptions
    {
        public int color { get; set; }
        public LedMode mode { get; set; }
        public uint brightness { get; set; }
        public uint interval { get; set; }
        public uint times { get; set; }
    }

    public enum LedMode
    {
        Off = 0,
        Solid = 1,
        SlowBreath = 2,
        FastBreath = 3,
        Blink = 4,
        Flash = 5,
        Rainbow = 6
    }
}

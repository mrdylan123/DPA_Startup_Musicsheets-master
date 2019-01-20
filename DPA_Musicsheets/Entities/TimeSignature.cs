namespace DPA_Musicsheets.Entities
{
    public class TimeSignature : MusicalSymbol
    {
        public uint BeatsPerBar { get; set; }
        public uint BeatUnit { get; set; }

        public TimeSignature(uint beatUnit, uint beatsPerBar)
        {
            BeatsPerBar = beatsPerBar;
            BeatUnit = beatUnit;
        }
    }
}

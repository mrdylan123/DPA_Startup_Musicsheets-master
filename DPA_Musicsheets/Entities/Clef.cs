namespace DPA_Musicsheets.Entities
{
    public class Clef : MusicalSymbol
    {
        public ClefType ClefType { get; set; }

        public Clef(ClefType clefType)
        {
            ClefType = clefType;
        }
    }
}

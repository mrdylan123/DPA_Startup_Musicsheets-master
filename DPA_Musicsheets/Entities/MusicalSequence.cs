using System.Collections.Generic;

namespace DPA_Musicsheets.Entities
{
    public class MusicalSequence
    {
        public int BeatsPerMinute { get; set; }
        public List<IMusicalSymbol> Symbols { get; set; }

        public MusicalSequence()
        {
            Symbols = new List<IMusicalSymbol>();
        }
    }
}

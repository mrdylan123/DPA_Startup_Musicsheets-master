using PSAMControlLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Entities
{
    public class MusicalNote
    {
        char Pitch { get; set; }
        //int NumberofDots { get; set; }
        int Duration { get; set; } // De duur van een noot

        Boolean hasCross { get; set; } // C => C#
        Boolean hasMinor { get; set; } // C => Cb
        Boolean hasPoint { get; set; } // true => helft langer in duur
    }
}

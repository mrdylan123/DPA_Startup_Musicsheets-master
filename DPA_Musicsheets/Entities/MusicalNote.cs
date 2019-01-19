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
        public char Pitch { get; set; }
        //int NumberofDots { get; set; }
        public int Duration { get; set; } // De duur van een noot

        public Boolean hasCross { get; set; } // C => C#
        public Boolean hasMinor { get; set; } // C => Cb
        public Boolean hasPoint { get; set; } // true => helft langer in duur

        public MusicalNote() { }
    }
}

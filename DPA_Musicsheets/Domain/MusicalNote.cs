using PSAMControlLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Models
{
    public class MusicalNote : MusicalSymbol
    {
        public int Length { get; set; }
        public int Octave { get; set; }
        public char Letter { get; set; }
        public bool HasKruis { get; set; } // TODO: Change name
        public bool HasMol { get; set; } // TODO: Change name
    }
}
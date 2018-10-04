using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Entities
{
    public class TimeSignature
    {
        int Count { get; set; } // Tellen in een maat
        SymbolDuration Duration { get; set; } // Welke nootsoort is 1 tel
    }
}

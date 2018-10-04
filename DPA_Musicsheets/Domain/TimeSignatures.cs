using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Models
{
    public class TimeSignatures
    {
        // Example: 6/8 where there's 6 counts in a beat and an eigth note is a count.
        public int TotalSeconds { get; set; } // Number of counts in a beat
        public int TypeBeat { get; set; } // The note that represents 1 count
    }
}

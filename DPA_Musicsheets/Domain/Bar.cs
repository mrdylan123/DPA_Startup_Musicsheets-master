using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Models
{
    public class Bar
    {
        public static int Lines = 5;
        public int LineNumber { get; set; }

        List<Bar> bars = new List<Bar>();

    }
}

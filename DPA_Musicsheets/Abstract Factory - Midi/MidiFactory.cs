using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Abstract_Factory___Midi
{
    public interface MidiFactory
    {
        void isTimeSignature();
        void isTempo();
        void isEndOfTrack();
    }
}

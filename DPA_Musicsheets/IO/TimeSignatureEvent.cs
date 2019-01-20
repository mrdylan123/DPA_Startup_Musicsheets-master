using DPA_Musicsheets.Entities;

namespace DPA_Musicsheets.IO
{
    public interface ITimeSignatureEvent
    {
        TimeSignature TimeSignature { get; }
    }
}

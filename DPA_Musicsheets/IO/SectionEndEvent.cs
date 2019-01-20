using DPA_Musicsheets.Entities;

namespace DPA_Musicsheets.IO
{
    interface ISectionEndEvent
    {
        Barline Barline { get; }
    }
}

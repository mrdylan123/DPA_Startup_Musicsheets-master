using DPA_Musicsheets.Entities;

namespace DPA_Musicsheets.IO
{
    public interface ISectionStartEvent
    {
        Barline Barline { get; }
    }
}

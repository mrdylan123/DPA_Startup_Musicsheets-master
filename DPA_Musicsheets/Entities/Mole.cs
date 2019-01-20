namespace DPA_Musicsheets.Entities
{
    public class Mole : NoteDecoration
    {
        public override int NoteAlteration => Note.NoteAlteration - 1;
    }
}

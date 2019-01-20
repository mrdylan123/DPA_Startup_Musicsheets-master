namespace DPA_Musicsheets.Entities
{
    public class Cross : NoteDecoration
    {
        public override int NoteAlteration => Note.NoteAlteration + 1;
    }
}

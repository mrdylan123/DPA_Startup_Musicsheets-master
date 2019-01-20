namespace DPA_Musicsheets.Entities
{
    public class Dot : NoteDecoration
    {
        public override int Dots => Note.Dots + 1;
    }
}

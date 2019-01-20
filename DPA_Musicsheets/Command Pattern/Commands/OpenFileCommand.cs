using DPA_Musicsheets.ViewModels;

namespace DPA_Musicsheets.Command_Pattern.Commands
{
    public class OpenFileCommand : IEditorCommand
    {
        public string Pattern => "openFile";
        public void Execute(MainViewModel editor)
        {
            editor.OpenFileCommand.Execute(null);
        }
    }
}

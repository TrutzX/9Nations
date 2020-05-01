using Help;
using UI;

namespace LoadSave
{
    public class LoadWindow
    {
        public static void Show()
        {
            WindowBuilderSplit w = WindowBuilderSplit.Create("Load the game", "Load");
            
            //add files
            foreach (LoadSaveInfo info in LoadSaveMgmt.GetAllSaves())
            { 
                w.Add(new LoadWindowSplitElement(info, w));
            }
            
            w.Finish();
        }
    }
}
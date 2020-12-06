using Game;
using Help;
using UI;

namespace LoadSave
{
    public class SaveWindow
    {
        public static void Show()
        {
            WindowBuilderSplit w = WindowBuilderSplit.Create("Save the game", "Overwrite");
            UIHelper.CreateButton("Create new save", w.buttonPanel.transform, () =>
            {
                w.Add(new SaveWindowSplitElement(LoadSaveMgmt.CreateNewSave(), w));
                
            });
            
            //add files
            foreach (LoadSaveInfo info in LoadSaveMgmt.GetAllSaves())
            { 
                w.Add(new SaveWindowSplitElement(info, w));
            }
            
            w.Finish();
        }
    }
}
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
                w.AddElement(new SaveWindowSplitElement(LoadSaveMgmt.CreateNewSave(), w));
                
            });
            
            //add files
            foreach (LoadSaveInfo info in LoadSaveMgmt.GetAllSaves())
            { 
                w.AddElement(new SaveWindowSplitElement(info, w));
            }
            
            w.Finish();
        }
    }

    public class SaveWindowSplitElement : LoadWindowSplitElement
    {

        public SaveWindowSplitElement(LoadSaveInfo info, WindowBuilderSplit w) : base(info, w)
        {
        }

        public override void Perform()
        {
            info = LoadSaveMgmt.UpdateSave(info.file,GameMgmt.Get().data.name);
        }
    }
}
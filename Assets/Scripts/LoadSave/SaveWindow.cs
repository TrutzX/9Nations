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
                w.AddElement(new SaveWindowSplitElement(LoadSaveMgmt.CreateNewSave()));
                
            });
            
            //add files
            foreach (LoadSaveInfo info in LoadSaveMgmt.GetAllSaves())
            { 
                w.AddElement(new SaveWindowSplitElement(info));
            }
            
            w.Finish();
        }

        class SaveWindowSplitElement : WindowBuilderSplit.SplitElement
        {
            private LoadSaveInfo info;

            public SaveWindowSplitElement(LoadSaveInfo info) : base(info.name, "ui:file")
            {
                this.info = info;
            }

            public override void ShowDetail(PanelBuilder panel)
            {
                info.ShowInfo(panel);
            }

            public override void Perform()
            {
                info = LoadSaveMgmt.UpdateSave(info.file);
            }
        }
    }
}
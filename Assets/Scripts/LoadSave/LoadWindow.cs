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
                w.AddElement(new LoadWindowSplitElement(info));
            }
            
            w.Finish();
        }

        class LoadWindowSplitElement : SplitElement
        {
            private LoadSaveInfo info;

            public LoadWindowSplitElement(LoadSaveInfo info) : base(info.name, "ui:file")
            {
                this.info = info;
            }

            public override void ShowDetail(PanelBuilder panel)
            {
                info.ShowInfo(panel);
            }

            public override void Perform()
            {
                LoadSaveMgmt.LoadSave(info.file);
            }
        }
    }
}
using Help;
using UI;
using UI.Show;

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
                w.AddElement(new LoadWindowSplitElement(info, w));
            }
            
            w.Finish();
        }
    }

    public class LoadWindowSplitElement : SplitElement
    {
        protected LoadSaveInfo info;
        protected WindowBuilderSplit w;

        public LoadWindowSplitElement(LoadSaveInfo info, WindowBuilderSplit w) : base(info.name, "ui:file")
        {
            this.info = info;
            this.w = w;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            info.ShowInfo(panel,w);
        }

        public override void Perform()
        {
            LoadSaveMgmt.LoadSave(info.file);
        }
    }
}
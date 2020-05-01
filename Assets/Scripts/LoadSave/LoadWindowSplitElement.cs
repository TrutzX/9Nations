using Game;
using UI;
using UI.Show;

namespace LoadSave
{
    public class LoadWindowSplitElement : SplitElement
    {
        protected LoadSaveInfo info;
        protected WindowBuilderSplit w;

        public LoadWindowSplitElement(LoadSaveInfo info, WindowBuilderSplit w) : base(info.name, "file")
        {
            this.info = info;
            this.w = w;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            info.ShowInfo(panel);
            panel.AddImageTextButton($"Delete {info.name}", "no", () => { LoadSaveMgmt.DeleteFile(info.file); w.CloseWindow();});

        }

        public override void Perform()
        {
            LoadSaveMgmt.LoadSave(info.file);
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
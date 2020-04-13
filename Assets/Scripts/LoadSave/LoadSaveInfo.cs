using System;
using Game;
using UI;

namespace LoadSave
{
    [Serializable]
    public class LoadSaveInfo
    {
        public string version;
        public string round;
        public DateTime date;
        public string name;
        public string file;
        
        
        public void ShowInfo(PanelBuilder panel, WindowBuilderSplit wbs)
        {
            panel.AddHeaderLabel(name);
            panel.AddSubLabel("Round",round, "round");
            panel.AddSubLabel("Date",date.ToLongDateString(),"Icons/magic:date");
            panel.AddSubLabel("Game version",version,"logo");
            if (S.Debug())
                panel.AddSubLabel("File",file, "ui:file");
            panel.AddImageTextButton($"Delete {name}", SpriteHelper.Load("no"), () => { ES3.DeleteFile(file); wbs.CloseWindow();});
        }
    }
}
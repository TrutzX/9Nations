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
        
        
        public void ShowInfo(PanelBuilder panel)
        {
            panel.AddHeaderLabel(name);
            panel.AddSubLabel("Round",round, "round");
            panel.AddSubLabel("Date",date.ToLongDateString(),"!Icons/fugue:date");
            panel.AddSubLabel("Game",version,"logo");
            if (S.Debug())
                panel.AddSubLabel("File",file, "file");
        }
    }
}
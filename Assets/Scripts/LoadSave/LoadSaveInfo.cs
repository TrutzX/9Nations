using System;

namespace Help
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
            panel.AddLabel($"Round: {round}");
            panel.AddLabel($"Date: {date}");
            panel.AddLabel($"Game version: {version}");
        }
    }
}
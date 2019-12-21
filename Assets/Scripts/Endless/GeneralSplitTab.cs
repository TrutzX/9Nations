using System.Collections.Generic;
using Game;
using Libraries;
using Maps;
using UI;
using UI.Show;

namespace Endless
{
    
    public class GeneralSplitTab : SplitElementTab
    {
        public GeneralSplitTab(Dictionary<string, string> startConfig) : base("General", "logo", "Play & Set")
        {
            Add(new GeneralSplitElement(startConfig));
        }
    }
    
    internal class GeneralSplitElement : SplitElement
    {
        private Dictionary<string, string> startConfig;
        private int _id;
        
        public GeneralSplitElement(Dictionary<string, string> startConfig) : base("General", SpriteHelper.Load("base:map"))
        {
            this.startConfig = startConfig;
            _id = 0;
            disabled = "Add a player first";
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddButton("Add a new player", () =>
            {
                startConfig[_id + "name"] = "Player";
                startConfig[_id + "nation"] = "north";
                startConfig[_id + "winGold"] = "true";
                startConfig[_id + "loseKing"] = "true";
                PlayerSelectSplitElement psse = new PlayerSelectSplitElement(startConfig, _id++);
                (tab as GeneralSplitTab).Add(psse);
                
                disabled = null;
                //UpdateButtonText();
            });
        }

        public override void Perform()
        {
            GameMgmt.StartConfig = startConfig;
            GameMgmt.StartConfig["name"] = "endless game";
            GameMgmt.StartConfig["type"] = "endless";
            
            GameMgmt.Init();
        }
    }
}
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
    
    public class GeneralSplitElement : SplitElement
    {
        protected Dictionary<string, string> startConfig;
        protected int id;
        
        public GeneralSplitElement(Dictionary<string, string> startConfig, string name = "General", string icon = "map") : base(name, icon)
        {
            this.startConfig = startConfig;
            id = 0;
            disabled = "Add a player first";
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddButton("Add a new player", () =>
            {
                startConfig[id + "name"] = id==0?System.Environment.UserName:"Player";
                startConfig[id + "nation"] = "north";
                startConfig[id + "winGold"] = "true";
                startConfig[id + "loseKing"] = "true";
                PlayerSelectSplitElement psse = new PlayerSelectSplitElement(startConfig, id++);
                (tab as GeneralSplitTab).Add(psse);
                
                disabled = null;
                //UpdateButtonText();
            });

            panel.AddHeaderLabel("Options");
            L.b.gameOptions.GetAllByCategory("general").ForEach(o => o.AddOption(panel));
        }

        public override void Perform()
        {
            if (!startConfig.ContainsKey("map"))
            {
                UIHelper.ShowOk("No map","You need to select a map to play.");
                return;
            }
            
            GameMgmt.StartConfig = startConfig;
            GameMgmt.StartConfig["name"] = "endless game";
            GameMgmt.StartConfig["type"] = "endless";
            
            GameMgmt.Init();
        }
    }
}
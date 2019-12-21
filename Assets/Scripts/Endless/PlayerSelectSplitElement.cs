using System;
using System.Collections.Generic;
using Game;
using Libraries;
using Nations;
using UI;

namespace Endless
{
    internal class PlayerSelectSplitElement : SplitElement
    {
        Dictionary<string, string> startConfig;
        private int id;
        
        public PlayerSelectSplitElement(Dictionary<string, string> startConfig, int id) : base("Player", SpriteHelper.Load("base:map"))
        {
            this.startConfig = startConfig;
            this.id = id;
            this.icon = L.b.nations[startConfig[id + "nation"]].Sprite();
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddHeaderLabel("Name");
            panel.AddInput("Name",startConfig[id + "name"], s => { 
                startConfig[id + "name"] = s;
                UIHelper.UpdateButtonText(button,s);
            });

            List<string> ids = new List<string>();
            List<string> titles = new List<string>();
            foreach (Nation nation in L.b.nations.Values())
            {
                if (nation.Hidden)
                    continue;
                
                ids.Add(nation.Id);
                titles.Add(nation.Name);
            }
            
            panel.AddHeaderLabel("Nation");
            panel.AddDropdown(ids.ToArray(), startConfig[id + "nation"], titles.ToArray(),
                s => { 
                    startConfig[id + "nation"] = s;
                    UIHelper.UpdateButtonImage(button,L.b.nations[s].Sprite());
                });

            
            panel.AddHeaderLabel("How to win");
            panel.AddCheckbox(Boolean.Parse(startConfig[id +"winGold"]), "Collect 1000 Gold", b =>
            {
                startConfig[id +"winGold"] = b.ToString();
            });
            
            panel.AddHeaderLabel("How to lose");
            panel.AddCheckbox(Boolean.Parse(startConfig[id +"loseKing"]), "Lose your king", b =>
            {
                startConfig[id +"loseKing"] = b.ToString();
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
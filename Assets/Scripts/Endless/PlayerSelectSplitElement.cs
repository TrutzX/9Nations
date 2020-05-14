using System;
using System.Collections.Generic;
using System.Linq;
using Classes;
using Game;
using Libraries;
using Libraries.Coats;
using Libraries.Nations;
using UI;
using UI.Show;

namespace Endless
{
    internal class PlayerSelectSplitElement : GeneralSplitElement
    {
        
        public PlayerSelectSplitElement(Dictionary<string, string> startConfig, int id) : base(startConfig, "Player")
        {
            this.id = id;
            icon = L.b.coats[startConfig[id + "coat"]].Sprite();
            disabled = null;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddHeaderLabelT("name");
            panel.AddInputRandom("Name",startConfig[id + "name"], s => { 
                startConfig[id + "name"] = s;
                UIHelper.UpdateButtonText(button,s);
            }, () => LClass.s.NameGenerator("unit"));

            panel.AddHeaderLabelT("nation");
            panel.AddDropdown(L.b.nations.Values().ToList(), startConfig[id + "nation"],
                s => { 
                    startConfig[id + "nation"] = s;
                });
            
            panel.AddHeaderLabelT("coat");
            panel.AddDropdown(L.b.coats.GetAllByCategory("kingdom"), startConfig[id + "coat"], 
                s => { 
                    startConfig[id + "coat"] = s;
                    UIHelper.UpdateButtonImage(button,L.b.coats[s].Sprite());
                });

            panel.AddHeaderLabelT("endlessWin");
            panel.AddCheckbox(Boolean.Parse(startConfig[id +"winGold"]), S.T("endlessWinGold"), b =>
            {
                startConfig[id +"winGold"] = b.ToString();
            });
            
            panel.AddHeaderLabelT("endlessLose");
            panel.AddCheckbox(Boolean.Parse(startConfig[id +"loseKing"]), S.T("endlessLoseUnit"), b =>
            {
                startConfig[id +"loseKing"] = b.ToString();
            });
        }
    }
}
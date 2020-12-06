using System;
using System.Linq;
using Audio;
using Game;
using Libraries;
using Libraries.FActions;
using Libraries.Spells;
using Players;
using Tools;
using UI;
using Units;
using UnityEngine;

namespace Classes.Actions
{
    public class ActionMagicSelect : BaseActiveAction
    {
        public ActionMagicSelect() : base("magicSelect") { }

        public override void PreRun()
        {
            //load spell
            Spell spell = L.b.spells[action.data["waiting"]];
            
            //calc size
            var points = CircleGenerator.Gen(initPos, mapElementInfo.CalcVisibleRange(initPos));
            
            //collect move
            foreach (var pos in points)
            {
                //check them
                if (!player.fog.Visible(pos))
                {
                    continue;
                }
                
                if (!spell.reqTarget.Check(mapElementInfo,pos))
                    continue;
                
                Color(pos,0);
            }
            
        }

        protected override void RemoveAfter()
        {
        }

        public override string PanelMessage()
        {
            //load spell
            Spell spell = L.b.spells[action.data["waiting"]];
            
            return S.T("spellTarget", spell.Name());
        }

        protected override void ClickFirst()
        {
            //load spell
            Spell spell = L.b.spells[action.data["waiting"]];

            String desc;
            var mei = S.MapElement(LastClickPos);
            desc = mei != null ? mei.data.name : S.Game().newMap.Terrain(LastClickPos).Name();
            
            OnMapUI.Get().unitUI.ShowPanelMessage(S.T("spellField", spell.Name(), desc));
        }

        protected override void ClickFirstCancel()
        {
            OnMapUI.Get().SetActiveAction(null,false);
        }

        protected override void ClickSecond()
        {
            L.b.spells[action.data["waiting"]].Perform(player, mapElementInfo, LastClickPos, true);
        }
    }
}
using System;
using Audio;
using Buildings;
using Classes.Actions;
using Game;
using Libraries.Animations;
using Libraries.FActions;
using MapElements;
using MapElements.Units;
using Players;
using Players.Infos;
using reqs;
using Tools;
using UI;
using Units;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Libraries.Spells
{

    [Serializable]
    public class Spell : BaseData
    {
        public ActionHolders action;
        public ActionHolders actionFail;
        public int difficult;
        public int cost;
        public ReqHolder reqTarget;
        public string animation;

        public Spell()
        {
            action = new ActionHolders();
            actionFail = new ActionHolders();
            reqTarget = new ReqHolder();
        }
        
        public override void ShowLexicon(PanelBuilder panel)
        {
            base.ShowLexicon(panel);
            panel.AddImageLabel(S.T("spellCost", cost), "ap");
            req.BuildPanel(panel);
            reqTarget.BuildPanel(panel);
            
            var ah = new ActionDisplaySettings(panel, null);
            action.BuildPanelT(ah,"spellPerformSuccess");
            actionFail.BuildPanelT(ah, "spellPerformFail");
        }
        
        public override void ShowLexicon(PanelBuilder panel, MapElementInfo info, NVector pos)
        {
            base.ShowLexicon(panel);
            panel.AddImageLabel(S.T("spellCostUnit", cost, info.data.ap), "ap");
            panel.AddLabel(S.T("spellChance",info.data.spells.CalcChance(id)));
            req.BuildPanel(panel, info, info.Pos());
            reqTarget.BuildPanel(panel);

            var ah = new ActionDisplaySettings(panel, S.ActPlayer(), info, info.Pos(), null);
            action.BuildPanelT(ah,"spellPerformSuccess");
            actionFail.BuildPanelT(ah, "spellPerformFail");
        }

        public void Perform(Player player, MapElementInfo mapElementInfo, NVector pos, bool ani)
        {
            //mapElementInfo.data.ap -= cost;
            var target = S.MapElement(pos);
            string msg = null;
            int ap = 0;
            int chance = mapElementInfo.data.spells.CalcChance(id);
            mapElementInfo.data.spells.Cast(id);
            target = (target != null) ? target : mapElementInfo;
            //can perform?
            if (chance > Random.Range(0, 100))
            {
                //calc cost
                foreach (var ah in action.actions)
                {
                    ap += ah.DataAction().cost;
                }
                
                mapElementInfo.data.ap += ap; //tmp for spell using
                msg = action.Performs(ActionEvent.All, player, target, pos);
                
                //success?
                if (String.IsNullOrEmpty(msg))
                {
                    if (ani && !String.IsNullOrEmpty(animation))
                    {
                        L.b.animations.Create(animation, pos);
                        NAudio.Play(Sound);
                        if (!mapElementInfo.IsBuilding())
                            ((UnitInfo)mapElementInfo).UnitAnimator().PlayIdleAnimation(UnitAnimatorType.Cast);
                    }
                    
                    mapElementInfo.AddNoti(S.T("spellSuccess", Name()), Icon);
                    
                    return;
                }
            }

            //falure
            
            //calc cost
            ap = 0;
            foreach (var ah in action.actions)
            {
                ap += ah.DataAction().cost;
            }
                
            mapElementInfo.data.ap += ap; //tmp for spell using
            var nmsg = actionFail.Performs(ActionEvent.All, player, target, pos);
            
            if (ani) 
            { 
                L.b.animations.Create("broken", pos);
                mapElementInfo.UI().ShowPanelMessageError(S.T("spellError",Name()));
            }

            msg = $"{msg} {nmsg}";
            
            mapElementInfo.AddNoti(S.T("spellError",Name(),msg), Icon);

        }
    }
}
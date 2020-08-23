using System;
using Buildings;
using Game;
using Libraries;
using Libraries.FActions;
using Libraries.FActions.General;
using Players;
using Tools;
using Towns;
using Units;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Classes.Actions
{
    public class ActionProduce : BasePerformAction
    {
        public ActionProduce() : base("produce"){}
        
        public ActionProduce(string id) : base(id){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            foreach (var data in holder.data)
            {
                if (!data.Key.StartsWith("res-")) continue;

                string res = data.Key.Substring(4);
                
                
                if (!ProduceOneRes(evt, info, pos, ConvertHelper.Int(data.Value), res)) return;
            }
        }

        protected bool ProduceOneRes(ActionEvent evt, MapElementInfo info, NVector pos, int val, string res)
        {
            //Debug.Log(res+" "+val+" "+GameMgmt.Get().data.map.ResGenContains(pos, res));
            
            //remove?
            if (val > 0 && GameMgmt.Get().data.map.ResGenContains(pos, res))
            {
                int rval = GameMgmt.Get().data.map.ResGen(pos, res);
                if (rval < 200 && Random.Range(0, 200) > rval)
                {
                    val = 1;
                }

                if (rval <= 0)
                {
                    info.SetLastInfo($"The deposit of {res} is empty.");
                    return false;
                }

                if (rval < 20)
                {
                    info.SetLastInfo($"The deposit of {res} is nearly empty.");
                }

                GameMgmt.Get().data.map.ResGenAdd(pos, res, -val);
            }

            ResType r = evt == ActionEvent.NextRound ? ResType.Produce :
                evt == ActionEvent.FinishConstruct ? ResType.Construction : ResType.Gift;

            double v = val;
            //add modi?
            if (r == ResType.Produce && val > 0)
            {
                v = L.b.modifiers[C.ProduceModi].CalcModi(v, info.Player(), info.Pos());
            }
            
            info.Town().AddRes(res, v, r);
            return true;
        }

        protected override void Perform(ActionEvent evt, Player player, ActionHolder holder)
        {
            foreach (var data in holder.data)
            {
                if (!data.Key.StartsWith("res-")) continue;

                ResType r = evt == ActionEvent.NextRound ? ResType.Produce :
                    evt == ActionEvent.FinishConstruct ? ResType.Construction : ResType.Gift;
                player.AddResTotal(data.Key.Substring(4),ConvertHelper.Int(data.Value),r);
            }
        }

        public override void BuildPanel(ActionDisplaySettings sett)
        {
            if (sett.compact)
            {
                sett.panel.AddHeaderLabel(sett.holder.trigger == ActionEvent.FinishConstruct
                    ? "Produce resources after construction":"Produce resources every turn");
                foreach (var data in sett.holder.data)
                {
                    if (!data.Key.StartsWith("res-")) continue;

                    L.b.res[data.Key.Substring(4)].AddImageLabel(sett.panel, ConvertHelper.Int(data.Value));
                }
                
                return;
            }
            
            sett.addReq = false;
            base.BuildPanel(sett);
            foreach (var data in sett.holder.data)
            {
                if (!data.Key.StartsWith("res-")) continue;

                L.b.res[data.Key.Substring(4)].AddImageLabel(sett.panel, ConvertHelper.Int(data.Value));
            }
            if (sett.mapElement == null)
                sett.holder.req.BuildPanel(sett.panel, "Requirement");
            else
                sett.holder.req.BuildPanel(sett.panel, "Requirement", sett.mapElement, sett.pos);
        }

        public override ActionHolder Create(string setting)
        {
            ActionHolder conf = base.Create(setting);

            //add res
            string[] type = SplitHelper.Separator(setting);
            if (type[0] == "once")
            {
                conf.trigger = ActionEvent.FinishConstruct;
            }
            else if (type[0] == "turn")
            {
                conf.trigger = ActionEvent.NextRound;
            }

            for (int i = 1; i < type.Length; i++)
            {
                var d = SplitHelper.SplitInt(type[i]);
                if (d.value < 0)
                {
                    conf.req.Add("res",$">{d.value*-1}:{d.key}");
                }
                conf.data.Add("res-"+d.key,d.value.ToString());
            }

            return conf;
        }
    }
}
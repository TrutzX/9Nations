using System;
using Buildings;
using Game;
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

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            foreach (var data in holder.data)
            {
                if (!data.Key.StartsWith("res-")) continue;

                ResType r = evt == ActionEvent.NextRound ? ResType.Produce :
                    evt == ActionEvent.FinishConstruct ? ResType.Construction : ResType.Gift;

                string res = data.Key.Substring(4);
                int val = ConvertHelper.Int(data.Value);
                
                //Debug.Log(res+" "+val+" "+GameMgmt.Get().data.map.ResGenContains(pos, res));
                
                //remove?
                if (val > 0 && GameMgmt.Get().data.map.ResGenContains(pos, res))
                {
                    int rval = GameMgmt.Get().data.map.ResGen(pos, res);
                    if (rval < 200 && Random.Range(0,200) > rval)
                    {
                        val = 1;
                    }
                    
                    if (rval <= 0)
                    {
                        info.SetLastInfo($"The deposit of {res} is empty.");
                        return;
                    }
                    
                    if (rval < 20)
                    {
                        info.SetLastInfo($"The deposit of {res} is nearly empty.");
                    }
                    GameMgmt.Get().data.map.ResGenAdd(pos, res, -val);
                }
                
                info.Town().AddRes(res, val, r);
            }
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

                    sett.panel.AddRes(data.Key.Substring(4), data.Value);
                }
                
                return;
            }
            
            sett.addReq = false;
            base.BuildPanel(sett);
            foreach (var data in sett.holder.data)
            {
                if (!data.Key.StartsWith("res-")) continue;

                sett.panel.AddRes(data.Key.Substring(4), data.Value);
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
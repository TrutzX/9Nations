using Buildings;
using Crafts;
using Game;
using Libraries;
using Libraries.Crafts;
using Libraries.FActions;
using Libraries.FActions.General;
using Players;
using Tools;
using UI;
using Units;
using UnityEngine;

namespace Classes.Actions
{
    public class ActionCraft : ActionProduce
    {
        public ActionCraft() : base("craft"){}

        public override bool Is(ActionHolder holder, ActionEvent type)
        {
            return type == ActionEvent.Direct || type == ActionEvent.NextRound;
        }
        
        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            
            // show it
            if (evt == ActionEvent.Direct)
            {
                WindowBuilderSplit wbs = WindowBuilderSplit.Create("Craft", null);
                wbs.Add(new CraftStatusSplitElement(info, holder));
                
                //build it
                foreach (var craft in L.b.crafts.GetAllByCategory(holder.data["category"]))
                {
                    if (craft.req.Check(player, info, pos, true))
                        wbs.Add(new CraftSplitElement(craft, info, holder));
                }

                wbs.Finish();
                return;
            }

            //produce
            if (evt == ActionEvent.NextRound)
            {
                //has it?
                if (!holder.data.ContainsKey("craft0"))
                {
                    info.SetLastInfo("Nothing to craft");
                    return;
                }

                var c = SplitHelper.SplitInt(holder.data["craft0"]);
                
                //produce?
                Craft craft = L.b.crafts[c.key];

                foreach (var data in craft.res)
                {
                    if (!ProduceOneRes(evt, info, pos, data.Value, data.Key)) return;
                }
                
                //found it?
                if (c.value > 0)
                {
                    c.value--;
                    if (c.value > 0)
                        holder.data["craft0"] = SplitHelper.BuildSplit(c.key, c.value);
                    else
                    {
                        //delete it?
                        CraftMgmt.RebuildAfter(0, holder.data);
                    }
                }
            }
            
            
        }

        protected override void Perform(ActionEvent evt, Player player, ActionHolder holder)
        {
            throw new System.NotImplementedException();
        }

        public override ActionHolder Create(string setting)
        {
            ActionHolder conf = base.Create(setting);
            conf.trigger = ActionEvent.Direct;
            
            conf.data.Add("category",setting);

            return conf;
        }
    }
}
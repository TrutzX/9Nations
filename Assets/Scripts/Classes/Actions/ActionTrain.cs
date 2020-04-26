using System.Linq;
using Buildings;
using Classes.Actions.Addons;
using Libraries;
using Libraries.Buildings;
using Libraries.Elements;
using Libraries.FActions;
using Libraries.FActions.General;
using Libraries.Units;
using Players;
using Tools;
using UI;
using UI.Show;
using Units;

namespace Classes.Actions
{
    public class ActionTrain : ActionBuild
    {
        public ActionTrain() : base("train"){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            if (BuildAllowed(player, info, pos, holder)) return;

            WindowTabBuilder wtb = WindowTabBuilder.Create(holder.DataAction().Desc);

            BuildLast(player, info, pos, holder, wtb, "lastTrain");

            foreach (string e in player.elements.elements)
            {
                Element ele = L.b.elements[e];
                SplitElementTab set = new SplitElementTab(ele.name, ele.Icon, holder.DataAction().name);

                var b = L.b.units.GetAllByCategory(ele.id).OrderBy(o=>o.name).ToList();
                foreach (DataUnit build in b)
                {
                    AddBuild(player, info, pos, build.id, set);
                }

                if (set.Count() > 0)
                {
                    wtb.Add(set);
                }
            }

            wtb.Finish();
        }

        protected override BaseDataBuildingUnit Get(string key)
        {
            return L.b.units[key];
        }
        
        protected override void AddBuild(Player player, MapElementInfo info, NVector pos, string key, ISplitManager set)
        {
            BaseDataBuildingUnit build = Get(key);
            
            if (build.req.Check(player, info, pos, true))
            {
                TrainSplitElement be = new TrainSplitElement(build, info, pos, set);
                be.disabled = build.req.Desc(player, info, pos);
                set.Add(be);
                //win.AddBuilding(build.id);
            }
        }
    }
}
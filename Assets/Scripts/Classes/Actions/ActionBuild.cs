using System.Linq;
using Buildings;
using Classes.Actions.Addons;
using Libraries;
using Libraries.Buildings;
using Libraries.Elements;
using Libraries.FActions;
using Libraries.FActions.General;
using Players;
using Tools;
using UI;
using UI.Show;
using Units;

namespace Classes.Actions
{
    public class ActionBuild : BasePerformAction
    {
        public ActionBuild() : this("build"){}
        public ActionBuild(string id) : base(id){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            if (BuildAllowed(player, info, pos, holder)) return;

            WindowTabBuilder wtb = WindowTabBuilder.Create(holder.DataAction().Desc);

            BuildLast(player, info, pos, holder, wtb, "lastBuild");

            foreach (string e in player.elements.elements)
            {
                Element ele = L.b.elements[e];
                SplitElementTab set = new SplitElementTab(ele.Name(), ele.Icon, holder.DataAction().Name());

                var b = L.b.buildings.GetAllByCategory(ele.id).OrderBy(o=>o.Name()).ToList();
                foreach (DataBuilding build in b)
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

        protected bool BuildAllowed(Player player, MapElementInfo info, NVector pos, ActionHolder holder)
        {
            if (holder.data.ContainsKey("allowed"))
            {
                //load buildings
                WindowBuilderSplit b = WindowBuilderSplit.Create(holder.DataAction().Desc, holder.DataAction().Name());

                foreach (string key in SplitHelper.Separator(holder.data["allowed"]))
                {
                    AddBuild(player, info, pos, key, b);
                }

                b.Finish();
                return true;
            }

            return false;
        }

        protected void BuildLast(Player player, MapElementInfo info, NVector pos, ActionHolder holder, WindowTabBuilder wtb, string id)
        {
            var last = L.b.playerOptions[id];
            //add last used?
            if (!string.IsNullOrEmpty(last.Value()))
            {
                SplitElementTab set = new SplitElementTab(last.Name(), last.Icon, holder.DataAction().Name());
                foreach (string key in SplitHelper.Separator(last.Value()))
                {
                    if (string.IsNullOrEmpty(key)) continue;
                    
                    AddBuild(player, info, pos, key, set);
                }

                wtb.Add(set);
            }
        }

        protected virtual BaseDataBuildingUnit Get(string key)
        {
            return L.b.buildings[key];
        }
        
        protected virtual void AddBuild(Player player, MapElementInfo info, NVector pos, string key, ISplitManager set)
        {
            BaseDataBuildingUnit build = Get(key);
            
            if (build.req.Check(player, info, pos, true))
            {
                BuildSplitElement be = new BuildSplitElement(build, info, pos, set);
                be.disabled = build.req.Desc(player, info, pos);
                set.Add(be);
                //win.AddBuilding(build.id);
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
            if (!string.IsNullOrEmpty(setting))
            {
                conf.data["allowed"] = setting;
            }

            return conf;
        }
        
        
    }
}
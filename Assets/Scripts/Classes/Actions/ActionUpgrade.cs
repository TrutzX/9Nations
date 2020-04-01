using System.Linq;
using Buildings;
using Game;
using Libraries;
using Libraries.Buildings;
using Libraries.FActions;
using Libraries.FActions.General;
using Players;
using Tools;
using UI;
using Units;

namespace Classes.Actions
{
    public class ActionUpgrade : BasePerformAction
    {
        public ActionUpgrade() : base("upgrade"){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            string[] keys;
            if (holder.data.ContainsKey("upgrade"))
            {
                keys = SplitHelper.Seperator(holder.data["upgrade"]);
            }
            else
            {
                keys = info.IsBuilding()?L.b.buildings.Keys().ToArray():L.b.units.Keys().ToArray();
            }
            
            //load buildings
            WindowBuilderSplit b = WindowBuilderSplit.Create(holder.DataAction().Desc,holder.DataAction().name);

            {
                foreach (string key in keys)
                {
                    BaseDataBuildingUnit build = info.IsBuilding()?(BaseDataBuildingUnit) L.b.buildings[key]:L.b.units[key];
                    if (build.req.Check(player, info, pos,true))
                    {
                        BuildUpgradeSplitElement be = new BuildUpgradeSplitElement(build, info, pos);
                        be.disabled = build.req.Desc(player, info, pos);
                        b.AddElement(be);
                        //win.AddBuilding(build.id);
                    }
                
                }
                
            }

            b.Finish();
        }

        protected override void Perform(ActionEvent evt, Player player, ActionHolder holder)
        {
            throw new System.NotImplementedException();
        }

        public override ActionHolder Create(string setting)
        {
            ActionHolder conf = base.Create(setting);
            CreateTrigger(conf, ActionEvent.Direct);
            if (!string.IsNullOrEmpty(setting))
            {
                conf.data["upgrade"] = setting;
                string[] keys = SplitHelper.Seperator(setting);
                
                //add req
                foreach (string key in keys)
                {
                    conf.req.Add("upgrade",key);
                }
            }

            return conf;
        }
        
        
    }
    
    public class BuildUpgradeSplitElement : BuildSplitElement
    {
        public BuildUpgradeSplitElement(BaseDataBuildingUnit build, Buildings.MapElementInfo go, NVector pos) : base(build, go, pos)
        {
        }

        public override void Perform()
        {
            go.Upgrade(build.id);
            OnMapUI.Get().UpdatePanel(pos);
        }
    }
}
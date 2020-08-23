using System.Linq;
using Buildings;
using Classes.Actions.Addons;
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
                keys = SplitHelper.Separator(holder.data["upgrade"]);
            }
            else
            {
                keys = info.IsBuilding()?L.b.buildings.Keys().ToArray():L.b.units.Keys().ToArray();
            }
            
            //load buildings
            WindowBuilderSplit b = WindowBuilderSplit.Create(holder.DataAction().Desc(),holder.DataAction().Name());

            {
                foreach (string key in keys)
                {
                    BaseDataBuildingUnit build = info.IsBuilding()?(BaseDataBuildingUnit) L.b.buildings[key]:L.b.units[key];
                    if (build.req.Check(player, info, pos,true))
                    {
                        BuildUpgradeSplitElement be = new BuildUpgradeSplitElement(build, info, pos, b);
                        be.disabled = build.req.Desc(player, info, pos);
                        b.Add(be);
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
            conf.trigger = ActionEvent.Direct;
            
            if (!string.IsNullOrEmpty(setting))
            {
                conf.data["upgrade"] = setting;
                string[] keys = SplitHelper.Separator(setting);
                
                //add req
                foreach (string key in keys)
                {
                    conf.req.Add("upgrade",key);
                }
            }

            return conf;
        }
        
        
    }
}
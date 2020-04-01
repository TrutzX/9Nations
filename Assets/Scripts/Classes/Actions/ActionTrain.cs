using System.Linq;
using Buildings;
using Game;
using Libraries;
using Libraries.Buildings;
using Libraries.FActions;
using Libraries.FActions.General;
using Libraries.Units;
using Players;
using Tools;
using UI;
using Units;

namespace Classes.Actions
{
    public class ActionTrain : BasePerformAction
    {
        public ActionTrain() : base("train"){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            string[] keys;
            if (holder.data.ContainsKey("unit"))
            {
                keys = SplitHelper.Seperator(holder.data["unit"]);
            }
            else
            {
                keys = L.b.units.Keys().ToArray();
            }
            
            //load buildings
            WindowBuilderSplit b = WindowBuilderSplit.Create(holder.DataAction().Desc,holder.DataAction().name);

            foreach (string key in keys)
            {
                DataUnit build = L.b.units[key];
                if (build.req.Check(player, info, pos,true))
                {
                    BuildSplitElement be = new TrainSplitElement(build, info, pos);
                    be.disabled = build.req.Desc(player, info, pos);
                    b.AddElement(be);
                    //win.AddBuilding(build.id);
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
                conf.data["unit"] = setting;
            }

            return conf;
        }
        
        
    }
    
    public class TrainSplitElement : BuildSplitElement
    {
        public TrainSplitElement(BaseDataBuildingUnit build, Buildings.MapElementInfo go, NVector pos) : base(build, go, pos)
        {
        }

        public override void Perform()
        {
            GameMgmt.Get().unit.Create(S.Towns().NearstTown(PlayerMgmt.ActPlayer(),pos,false).id, build.id, pos);
            OnMapUI.Get().UpdatePanel(pos);
        }
    }
}
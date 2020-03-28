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
using UI.Show;

namespace Classes.Actions
{
    public class ActionBuild : BasePerformAction
    {
        public ActionBuild() : base("build"){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            string[] keys;
            if (holder.data.ContainsKey("building"))
            {
                keys = SplitHelper.Seperator(holder.data["building"]);
            }
            else
            {
                keys = L.b.buildings.Keys().ToArray();
            }
            
            //load buildings
            WindowBuilderSplit b = WindowBuilderSplit.Create(holder.DataAction().Desc,holder.DataAction().name);

            foreach (string key in keys)
            {
                DataBuilding build = L.b.buildings[key];
                if (build.req.Check(player, info, pos,true))
                {
                    BuildSplitElement be = new BuildSplitElement(build, info, pos);
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
                conf.data["building"] = setting;
            }

            return conf;
        }
        
        
    }
    
    public class BuildSplitElement : SplitElement
    {
        protected BaseDataBuildingUnit build;
        protected MapElementInfo go;
        protected NVector pos;
        public BuildSplitElement(BaseDataBuildingUnit build, MapElementInfo go, NVector pos) : base(build.name, build.Sprite())
        {
            this.build = build;
            this.pos = pos;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            build.ShowBuild(panel, pos);
        }

        public override void Perform()
        {
            GameMgmt.Get().building.Create(S.Towns().NearstTown(PlayerMgmt.ActPlayer(),pos,false).id, build.id, pos);
            OnMapUI.Get().UpdatePanel(pos);
        }
    }
}
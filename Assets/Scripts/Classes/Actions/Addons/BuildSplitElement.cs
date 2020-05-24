using Buildings;
using Game;
using Libraries;
using Libraries.Buildings;
using Libraries.PlayerOptions;
using Players;
using Tools;
using UI;
using UI.Show;
using UnityEngine;

namespace Classes.Actions.Addons
{
    public class BuildSplitElement : SplitElement
    {
        protected ISplitManager ism;
        protected BaseDataBuildingUnit build;
        protected MapElementInfo go;
        protected NVector pos;
        public BuildSplitElement(BaseDataBuildingUnit build, MapElementInfo go, NVector pos, ISplitManager ism) : base(build.Name(), build.Sprite())
        {
            this.build = build;
            this.pos = pos;
            this.ism = ism;
            this.go = go;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            build.ShowBuild(panel, pos);
        }

        public override void Perform()
        {
            UpdatePref("lastBuild");
            ism.Close();
            GameMgmt.Get().building.Create(S.Towns().NearestTown(S.ActPlayer(),pos,false).id, build.id, pos);
            OnMapUI.Get().UpdatePanel(pos);
        }

        protected void UpdatePref(string key)
        {
            PlayerOption po = L.b.playerOptions[key];
            
            //remove old?
            string val = string.IsNullOrEmpty(po.Value())?"":po.Value().Replace(build.id + ";", "");
            
            val = build.id + ";" + val;
            
            //trim?
            int count = 0;
            foreach (char c in val) 
                if (c == ';') count++;

            if (count > 10)
            {
                val = val.Substring(0, val.LastIndexOf(';'));
            }
            
            po.SetValue(val);
        }
    }
}
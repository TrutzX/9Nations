using System.Linq;
using Buildings;
using Classes.Actions.Addons;
using Game;
using Libraries;
using Libraries.Buildings;
using Libraries.Elements;
using Libraries.FActions;
using Libraries.FActions.General;
using Libraries.Units;
using MapElements;
using MapElements.Material;
using Players;
using Tools;
using UI;
using UI.Show;
using Units;
using UnityEngine;

namespace Classes.Actions
{
    
    public class ActionTrain : BaseSelectElementAction<DataUnitMgmt,DataUnit>
    {
        public ActionTrain() : base("train"){}
        
        protected override DataUnitMgmt Objects()
        {
            return L.b.units;
        }

        protected override SplitElement CreateSplitElement(DataUnit build, MapElementInfo info, NVector pos, ISplitManager ism)
        {
            return new BaseSelectElementSplitElement<DataUnit>(id, build, info, pos, ism, (mei, position) => 
            {
                MaterialWindow.ShowBuildMaterialWindow(build, pos, cost =>
                {
                    GameMgmt.Get().unit.Create(S.ActPlayerID(), build.id, pos, cost);
                    OnMapUI.Get().UpdatePanel(pos);
                });
            });
        }
    }
}
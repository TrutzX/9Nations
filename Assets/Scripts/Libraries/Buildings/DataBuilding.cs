using System;
using Game;
using MapElements;
using Tools;
using UI;

namespace Libraries.Buildings
{
    [Serializable]
    public class DataBuilding : BaseDataBuildingUnit
    {
        public int width;
        public int height;
        public string connected;
        public string winter;
        public int worker;
        
        protected override void ShowWorker(PanelBuilder panel)
        {
            if (worker == 0) return;

            if (worker > 0)
            {
                L.b.res[C.Inhabitant].AddImageLabel(panel, worker);
            }
            else
            {
                L.b.res[C.Worker].AddImageLabel(panel, -worker);
            }
        }

        public override void ShowLexicon(PanelBuilder panel, MapElementInfo onMap, NVector pos)
        {
            base.ShowLexicon(panel, onMap!=null && !onMap.IsBuilding()?null:onMap, pos);
        }
    }

}

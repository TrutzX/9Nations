using System.Linq;
using Buildings;
using Classes;
using Players;
using UI;
using UI.Show;

namespace Units
{
    class UnitSplitInfo : SplitElement
    {
        private readonly UnitInfo _unit;
        
        public UnitSplitInfo(UnitInfo unit) : base(unit.gameObject.name,unit.dataUnit.Sprite())
        {
            this._unit = unit;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            //diff unit?
            if (!_unit.Owner(PlayerMgmt.ActPlayerID()))
            {
                _unit.dataUnit.AddImageLabel(panel);
                panel.AddHeaderLabel("Information");
                panel.AddImageLabel($"Owner: {_unit.Player().name}",_unit.Player().icon);
                panel.AddImageLabel($"HP: ??/{_unit.dataUnit.hp}","hp");
                panel.AddImageLabel($"AP: ??/{_unit.dataUnit.ap}","ap");
                return;
            }

            panel.AddInputRandom("name", _unit.name,
                val => _unit.name = val,
                () => LClass.s.nameGenerators["unit"].Gen()+" "+_unit.dataUnit.name);
            
            panel.AddHeaderLabel("Information");
            panel.AddImageLabel($"HP: {_unit.data.hp}/{_unit.data.hpMax}","hp");
            panel.AddImageLabel($"AP: {_unit.data.ap}/{_unit.data.apMax}","ap");
            
            Construction con = _unit.GetComponent<Construction>();
            if (con != null)
            {
                panel.AddRes("Under Construction",_unit.data.construction.ToDictionary(entry => entry.Key,entry => entry.Value));
                panel.AddLabel("Missing resources");
            }
        }

        public override void Perform()
        {
        }
    }
}
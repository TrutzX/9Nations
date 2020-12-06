using MapElements.Buildings;
using UI;
using UI.Show;

namespace Buildings
{
    class BuildingLexiconInfo : SplitElement
    {
        private readonly BuildingInfo _unit;
        
        public BuildingLexiconInfo(BuildingInfo unit) : base("Lexicon","lexicon")
        {
            this._unit = unit;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            _unit.baseData.ShowLexicon(panel, _unit, null);
            
        }

        public override void Perform()
        {
        }
    }
}
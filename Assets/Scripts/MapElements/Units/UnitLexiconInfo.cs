using MapElements.Units;
using UI;
using UI.Show;

namespace Units
{
    internal class UnitLexiconInfo : SplitElement
    {
        private readonly UnitInfo _unit;
        
        public UnitLexiconInfo(UnitInfo unit) : base("lexicon","lexicon")
        {
            this._unit = unit;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            _unit.dataUnit.ShowLexicon(panel, _unit, null);
        }

        public override void Perform()
        {
        }
    }
}
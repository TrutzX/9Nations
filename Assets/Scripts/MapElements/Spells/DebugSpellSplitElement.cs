using Buildings;
using Libraries;
using Libraries.Spells;
using UI;
using UI.Show;
using UnityEngine;

namespace MapElements.Spells
{
    public class DebugSpellSplitElement : SplitElement
    {
        private readonly MapElementInfo _mapElementInfo;
        public DebugSpellSplitElement(MapElementInfo mapElementInfo) : base("Spell","magic")
        {
            this._mapElementInfo = mapElementInfo;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddHeaderLabelT("general");
            panel.AddSubLabel("Total",_mapElementInfo.data.spells.total.ToString());
            panel.AddHeaderLabelT("spell");
            foreach (var s in _mapElementInfo.data.spells.known)
            {
                Spell spell = L.b.spells[s];
                panel.AddSubLabel(spell.Name(), _mapElementInfo.data.spells.count[s].ToString(), spell.Icon);
            }
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}
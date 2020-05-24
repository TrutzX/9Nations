using Game;
using Players;
using Tools;
using UI;
using UI.Show;
using UnityEngine;

namespace Libraries.Terrains
{
    public class TerrainSplitElement : SplitElement
    {
        private readonly DataTerrain _data;
        private readonly NVector _pos;
        
        public TerrainSplitElement(DataTerrain data, NVector pos) : base(data.Name(), data.Sprite())
        {
            _data = data;
            _pos = pos;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            _data.ShowField(panel, S.ActPlayer(), _pos);
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}
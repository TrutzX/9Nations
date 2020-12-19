using Game;
using Libraries;
using Libraries.Elements;
using Libraries.Terrains;
using Maps;
using Maps.GameMaps;
using Players;
using Tools;
using UI;
using UnityEngine;

namespace Classes.Elements
{
    public class FireElement : BaseElementRun
    {
        public FireElement() : base("fire") {}
        public override void Develop(Player player)
        {
            //get town
            var pos = S.Towns().GetByPlayer(player.id)[0].pos;
            var list = CircleGenerator.Gen(pos, 20);

            foreach (var p in list)
            {
                if (S.Unit().Free(p))
                {
                    GameMgmt.Get().unit.Create(player.id, "christkind", p).FinishConstruct();
                    return;
                }
            }
        }
        
        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddHeaderLabelT("elementFreeUnit");
            L.b.units["christkind"].AddImageLabel(panel);
        }
    }
}
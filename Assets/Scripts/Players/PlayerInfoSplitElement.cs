using Libraries.Rounds;
using Players.Infos;
using UI;
using UI.Show;
using UnityEngine;

namespace Players
{
    public class PlayerInfoSplitElement: SplitElement
    {
        private Player _player;
        
        //TODO ADD Icon
        public PlayerInfoSplitElement(Player player) : base(player.name, SpriteHelper.Load(player.icon))
        {
            _player = player;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddHeaderLabel("General");
            panel.AddSubLabel("Name",_player.name);
            panel.AddSubLabel("Nation",_player.Nation().name);
            panel.AddSubLabel("Points",_player.points.ToString());

        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}